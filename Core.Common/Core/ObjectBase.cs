using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using Core.Common.Contracts;
using Core.Common.Extensions;
using Core.Common.Utils;
using FluentValidation;
using FluentValidation.Results;

namespace Core.Common.Core
{
    /// <summary>
    ///  just as the name says - it will be the base class
    /// for all entites
    /// </summary>
    public abstract class ObjectBase : 
        NotificationObject, 
        IDirtyCapable, 
        IExtensibleDataObject, 
        IDataErrorInfo
    {
        #region Fields

        protected bool _IsDirty;
        protected IValidator _Validator;  
        protected IEnumerable<ValidationFailure> _ValidationErrors;

        #endregion

        #region Properties

        public static CompositionContainer Container { get; set; }

        #endregion

        #region C-Tor

        public ObjectBase()
        {
            _Validator = GetValidator();
            Validate();
        }

        #endregion

        #region IExtensibleDataObject Members

        public ExtensionDataObject ExtensionData { get; set; }

        #endregion

        #region IDirtyCapable members

        [NotNavigable]
        public virtual bool IsDirty
        {
            get { return _IsDirty; }
            protected set
            {
                _IsDirty = value;
                OnPropertyChanged("IsDirty", false);
            }
        }

        public virtual bool IsAnythingDirty()
        {
            var isDirty = false;

            WalkObjectGraph(
            o =>
            {
                if (o.IsDirty)
                {
                    isDirty = true;
                    return true; // short circuit
                }
                return false;
            }, coll => { });

            return isDirty;
        }

        public List<IDirtyCapable> GetDirtyObjects()
        {
            var dirtyObjects = new List<IDirtyCapable>();

            WalkObjectGraph(
            o =>
            {
                if (o.IsDirty)
                    dirtyObjects.Add(o);

                return false;
            }, coll => { });

            return dirtyObjects;
        }

        public void CleanAll()
        {
            WalkObjectGraph(
            o =>
            {
                if (o.IsDirty)
                    o.IsDirty = false;
                return false;
            }, coll => { });
        }

        #endregion

        #region Protected methods

        protected void WalkObjectGraph(Func<ObjectBase, bool> snippetForObject,
                                       Action<IList> snippetForCollection,
                                       params string[] exemptProperties)
        {
            var visited = new List<ObjectBase>();
            Action<ObjectBase> walk = null;

            var exemptions = new List<string>();
            if (exemptProperties != null)
                exemptions = exemptProperties.ToList();

            walk = (o) =>
            {
                if (o == null || visited.Contains(o)) return;
                visited.Add(o);

                var exitWalk = snippetForObject.Invoke(o);

                if (exitWalk) return;
                var properties = o.GetBrowsableProperties();
                foreach (var property in properties)
                {
                    if (exemptions.Contains(property.Name)) continue;
                    if (property.PropertyType.IsSubclassOf(typeof(ObjectBase)))
                    {
                        var obj = (ObjectBase)(property.GetValue(o, null));
                        walk(obj);
                    }
                    else
                    {
                        var coll = property.GetValue(o, null) as IList;
                        if (coll == null) continue;
                        snippetForCollection.Invoke(coll);

                        foreach (var item in coll)
                        {
                            if (item is ObjectBase)
                            {
                                walk((ObjectBase)item);
                            }
                        }
                    }
                }
            };

            walk(this);
        }

        #endregion

        #region Property change notification

        protected override void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName, true);
        }

        protected void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression, bool makeDirty)
        {
            var propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
            OnPropertyChanged(propertyName, makeDirty);
        }

        protected void OnPropertyChanged(string propertyName, bool makeDirty)
        {
            base.OnPropertyChanged(propertyName);

            if (makeDirty)
            {
                IsDirty = true;
            }

            Validate();
        }

        #endregion

        #region Validation

        protected virtual IValidator GetValidator()
        {
            return null;
        }

        [NotNavigable]
        public IEnumerable<ValidationFailure> ValidationErrors
        {
            get { return _ValidationErrors; }
            set { }
        }

        public void Validate()
        {
            if (_Validator == null) return;
            var results = _Validator.Validate(this);
            _ValidationErrors = results.Errors;
        }

        [NotNavigable]
        public virtual bool IsValid
        {
            get {
                return _ValidationErrors == null || !_ValidationErrors.Any();
            }
        }

        #endregion

        #region IDataErrorInfo members

        string IDataErrorInfo.Error
        {
            get { return string.Empty; }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                var errors = new StringBuilder();

                if (_ValidationErrors != null && _ValidationErrors.Any())
                {
                    foreach (var validationError in _ValidationErrors)
                    {
                        if (validationError.PropertyName == columnName)
                        {
                            errors.AppendLine(validationError.ErrorMessage);
                        }
                    }
                }

                return errors.ToString();
            }
        }

        #endregion

    }
}
