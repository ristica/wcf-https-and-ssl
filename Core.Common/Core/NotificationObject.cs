using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using Core.Common.Utils;

namespace Core.Common.Core
{
    public class NotificationObject : INotifyPropertyChanged
    {
        private event PropertyChangedEventHandler _PropertyChangedEvent;

        protected List<PropertyChangedEventHandler> _PropertyChangedSubscribers = new List<PropertyChangedEventHandler>();

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                if (_PropertyChangedSubscribers.Contains(value)) return;
                _PropertyChangedEvent += value;
                _PropertyChangedSubscribers.Add(value);
            }
            remove
            {
                _PropertyChangedEvent -= value;
                _PropertyChangedSubscribers.Remove(value);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (_PropertyChangedEvent != null)
            {
                _PropertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
            OnPropertyChanged(propertyName);
        }
    }
}
