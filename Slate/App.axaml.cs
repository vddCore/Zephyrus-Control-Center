using System;
using System.Diagnostics;
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
        private readonly Timer _globalTimer = new(250);
        
        private ulong _globalTickCount;
        
        public override void Initialize()
        {
            GlitoneaCore.Initialize();
            
            Message.Subscribe<MainWindowTransitionFinishedMessage>(this, OnMainWindowTransitionFinished);
            
            AvaloniaXamlLoader.Load(this);

            _globalTimer.Elapsed += GlobalTime_Elapsed;
            
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
            
            TrayIcon.GetIcons(this)[0].IsVisible = true;
        }
        
        private void OnMainWindowTransitionFinished(MainWindowTransitionFinishedMessage msg)
        {
            if (msg.WasSlidingIn)
            {
                _globalTimer.Start();
            }
            else
            {
                _globalTimer.Stop();
            }
        }
        
        private void TrayIcon_Clicked(object? sender, EventArgs e)
        {
            Message.Broadcast<TrayIconClickedMessage>();
        }
        
        private void GlobalTime_Elapsed(object? sender, ElapsedEventArgs elapsedEventArgs)
        {
            new GlobalTickMessage(++_globalTickCount)
                .Broadcast();
        }
    }
}