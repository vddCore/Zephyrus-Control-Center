using System;
using System.Diagnostics;
using Glitonea.Mvvm.Messaging;
using Microsoft.Win32;
using Slate.Model.Messaging;
using Slate.Model.Settings.Components;

namespace Slate.Controller
{
    public partial class ApplicationController
    {
        private ApplicationSettings ApplicationSettings => ControlCenterSettings.Application;

        private void SubscribeToApplicationSettings()
        {
            Message.Subscribe<StartupLaunchChangedMessage>(this, OnStartupLaunchChanged);
        }

        private void OnStartupLaunchChanged(StartupLaunchChangedMessage msg)
        {
            using var regKey = Registry.CurrentUser.OpenSubKey(
                "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", 
                true
            );

            try
            {
                if (msg.Enabled)
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
                ApplicationSettings.RunOnStartup = false;
            }
        }
    }
}