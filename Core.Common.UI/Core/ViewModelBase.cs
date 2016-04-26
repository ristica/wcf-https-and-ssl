using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using Core.Common.Core;
using Demo.Business.Contracts;
using Demo.Client.Proxies.Service_Procies;
using FluentValidation.Results;

namespace Core.Common.UI.Core
{
    public class ViewModelBase : ObjectBase
    {
        #region Fields

        private bool _errorsVisible;
        private List<ObjectBase> _models;

        #endregion

        #region Properties

        public object ViewLoaded
        {
            get
            {
                OnViewLoaded();
                return null;
            }
        }

        #endregion

        #region Commands

        public DelegateCommand<object> ToggleErrorsCommand { get; protected set; }

        #endregion

        #region C-Tor

        protected ViewModelBase()
        {
            ToggleErrorsCommand = new DelegateCommand<object>(OnToggleErrorsCommandExecute, OnToggleErrorsCommandCanExecute);
        }

        #endregion

        #region Overrides

        public virtual string ViewTitle => string.Empty;

        protected virtual void OnViewLoaded() { }

        protected virtual void AddModels(List<ObjectBase> models) { }

        public virtual bool ValidationHeaderVisible => this.ValidationErrors != null && this.ValidationErrors.Any();

        public virtual bool ErrorsVisible
        {
            get { return this._errorsVisible; }
            set
            {
                if (this._errorsVisible == value) return;

                this._errorsVisible = value;
                OnPropertyChanged(() => this.ErrorsVisible, false);
            }
        }

        protected virtual void OnToggleErrorsCommandExecute(object arg)
        {
            this.ErrorsVisible = !this.ErrorsVisible;
        }

        protected virtual bool OnToggleErrorsCommandCanExecute(object arg)
        {
            return !this.IsValid;
        }

        public virtual string ValidationHeaderText
        {
            get
            {
                var ret = string.Empty;

                if (this.ValidationErrors == null) return ret;
                var verb = this.ValidationErrors.Count() == 1 ? "is" : "are";
                var suffix = this.ValidationErrors.Count() == 1 ? "" : "s";

                if (!IsValid)
                {
                    ret = $"There {verb} {ValidationErrors.Count()} validation error{suffix}.";
                }

                return ret;
            }
        }

        #endregion

        #region Methods

        protected void WithClient<T>(T proxy, Action<T> codeToExecute)
        {
            codeToExecute.Invoke(proxy);

            var disposableClient = proxy as IDisposable;
            disposableClient?.Dispose();
        }

        protected void ValidateModel()
        {
            if (this._models == null)
            {
                this._models = new List<ObjectBase>();
                AddModels(this._models);
            }

            this._ValidationErrors = new List<ValidationFailure>();

            if (this._models.Count <= 0) return;
            foreach (var modelObject in this._models)
            {
                modelObject?.Validate();
                this._ValidationErrors = this._ValidationErrors.Union(modelObject.ValidationErrors).ToList();
            }

            OnPropertyChanged(() => this.ValidationErrors, false);
            OnPropertyChanged(() => this.ValidationHeaderText, false);
            OnPropertyChanged(() => this.ValidationHeaderVisible, false);
        }

        #endregion
    }
}
