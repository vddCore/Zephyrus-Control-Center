using Slate.Infrastructure.Settings;
using Slate.Model.Messaging;

namespace Slate.Model.Settings.Components
{
    public class ApplicationSettings : SettingsComponent
    {        
        public bool RunOnStartup { get; set; }
        public bool CheckForUpdates { get; set; }

        protected override void OnSettingsModified(string? propertyName)
        {
            switch (propertyName)
            {
                case nameof(RunOnStartup):
                {
                    WithEventSuppressed(() =>
                    {
                        new StartupLaunchChangedMessage(RunOnStartup)
                            .Broadcast();
                    });

                    break;
                }
            }
        }
    }
}