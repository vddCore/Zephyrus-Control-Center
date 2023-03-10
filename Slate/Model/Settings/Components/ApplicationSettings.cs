using System;
using System.Diagnostics;
using Slate.Infrastructure.Settings;
using Microsoft.Win32;

namespace Slate.Model.Settings.Components
{
    public class ApplicationSettings : SettingsComponent
    {
        private const string ApplicationName = "Zephyrus Control Center";
        
        public bool RunOnStartup { get; set; }

        protected override void OnSettingsModified(string? propertyName)
        {
            switch (propertyName)
            {
                case nameof(RunOnStartup):
                {
                    TryUpdateRegistryKey();
                    break;
                }
            }
        }

        private void TryUpdateRegistryKey()
        {
            using var regKey = Registry.CurrentUser.OpenSubKey(
                "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", 
                true
            );

            try
            {
                if (RunOnStartup)
                {
                    regKey?.SetValue(
                        ApplicationName,
                        Process.GetCurrentProcess().MainModule!.FileName
                    );
                }
                else
                {
                    regKey?.DeleteValue(ApplicationName, false);
                }
            }
            catch (Exception)
            {
                // todo log or something. idk.
                
                WithEventSuppressed(() =>
                {
                    RunOnStartup = false;
                });
            }
        }
    }
}