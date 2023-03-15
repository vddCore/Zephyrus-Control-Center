using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation.Easings;
using Avalonia.Platform;
using Avalonia.Threading;
using Glitonea.Mvvm.Messaging;
using PropertyChanged;
using Slate.Infrastructure;
using Slate.Infrastructure.Native;
using Slate.Model.Messaging;

namespace Slate.View.Window
{
    [DoNotNotify]
    public partial class MainWindow : AnimatedWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Message.Subscribe<TrayIconClickedMessage>(this, OnTrayClicked);
            Message.Subscribe<PageSwitchedMessage>(this, OnPageSwitched);
        }

        private void OnPageSwitched(PageSwitchedMessage msg)
        {
            if (msg.PageMarker == Pages.MainMenu)
            {
                BackButton.Classes.Set("OnMainPage", true);
            }
            else
            {
                BackButton.Classes.Set("OnMainPage", false);
            }

            ResizeTo(msg.PageMarker!.ViewHeight);
        }

        protected override void OnLoaded()
        {
            var hwnd =  PlatformImpl!.Handle.Handle;
            
            ConfigureAsToolWindow(hwnd);
            
            IsVisible = false;
            Position = HiddenDesktopPosition;

            base.OnLoaded();
            
            new MainWindowLoadedMessage().Broadcast();
        }

        private void ConfigureAsToolWindow(nint hwnd)
        {
            var gwl = User32.GetWindowLong(
                hwnd,
                User32.GWL_EX_STYLE
            );

            User32.SetWindowLong(
                hwnd,
                User32.GWL_EX_STYLE,
                (gwl | User32.WS_EX_TOOLWINDOW) & ~User32.WS_EX_APPWINDOW
            );
        }

        private void OnTrayClicked(TrayIconClickedMessage _)
        {
            if (Animating)
                return;

            if (IsVisible)
            {
                SlideOut();
            }
            else
            {
                Activate();
                SlideIn();
            }
        }

        private void Window_OnDeactivated(object? sender, EventArgs e)
        {
            if (Animating || !IsVisible)
                return;

#if !DEBUG
            SlideOut();
#endif
        }

    }
}