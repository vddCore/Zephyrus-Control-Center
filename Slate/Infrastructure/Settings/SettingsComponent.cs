using System;
using System.ComponentModel;
using Slate.Model.Messaging;

namespace Slate.Infrastructure.Settings
{
    public abstract class SettingsComponent : INotifyPropertyChanged
    {
        private bool _isEventSuppresed;
        
        public event PropertyChangedEventHandler? PropertyChanged;

        protected SettingsComponent()
        {
            PropertyChanged += RaiseSettingsModified;
        }

        protected void RaiseSettingsModified(object? sender, PropertyChangedEventArgs e)
        {
            if (_isEventSuppresed)
                return;
            
            OnSettingsModified(e.PropertyName);

            new SettingsModifiedMessage(this, e.PropertyName)
                .Broadcast();
        }

        protected virtual void OnSettingsModified(string? propertyName)
        {
        }

        protected void WithEventSuppressed(Action action)
        {
            _isEventSuppresed = true;

            try
            {
                action();
            }
            finally
            {
                _isEventSuppresed = false;
            }
        }
    }
}