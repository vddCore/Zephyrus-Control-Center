using System;
using System.Timers;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Glitonea;
using Glitonea.Mvvm.Messaging;
using PropertyChanged;
using Slate.Model.Messaging;
using Slate.View.Window;

namespace Slate
{
    [DoNotNotify]
    public class App : Application
    {
        private readonly Timer _globalTimer = new(1000);
        private ulong _globalTickCount;
        
        public override void Initialize()
        {
            GlitoneaCore.Initialize();
            
            Message.Subscribe<MainWindowLoadedMessage>(this, OnMainWindowLoaded);
            
            AvaloniaXamlLoader.Load(this);

            _globalTimer.Elapsed += (_, _) =>
            {
                Message.Broadcast(new GlobalTickMessage(++_globalTickCount));
            };
            
            _globalTimer.Start();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void OnTrayIconClicked(object? sender, EventArgs e)
        {
            Message.Broadcast<TrayIconClickedMessage>();
        }
        
        private void OnMainWindowLoaded(MainWindowLoadedMessage obj)
        {
            TrayIcon.GetIcons(this)[0].IsVisible = true;
        }
    }
}