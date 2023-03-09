using Glitonea.Mvvm.Messaging;
using Slate.Infrastructure.Settings;

namespace Slate.Model.Messaging
{
    public sealed class SettingsModifiedMessage : Message
    {
        public SettingsComponent Owner { get; }
        public string? PropertyName { get; }

        public SettingsModifiedMessage(SettingsComponent owner, string? propertyName)
        {
            Owner = owner;
            PropertyName = propertyName;
        }
    }
}