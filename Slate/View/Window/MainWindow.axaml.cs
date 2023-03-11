using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation.Easings;
using Avalonia.Threading;
using Glitonea.Mvvm.Messaging;
using PropertyChanged;
using Slate.Infrastructure;
using Slate.Infrastructure.Native;
using Slate.Model.Messaging;

namespace Slate.View.Window
{
    [DoNotNotify]
    public partial class MainWindow : Avalonia.Controls.Window
    {
        private readonly Easing _slideOutEasing = Easing.Parse("CubicEaseIn");
        private readonly Easing _slideInEasing = Easing.Parse("CubicEaseOut");

        private const int ScreenSideMargin = 12; // Notification center margin.
        private const int ScreenBottomMargin = 48 + ScreenSideMargin; // Taskbar height.

        private double _slideInStep = 0.038;
        private double _slideOutStep = 0.038;
        
        private bool _animating;

        public PixelPoint VisibleDesktopPosition
        {
            get
            {
                var screenSize = SystemParameters.PrimaryScreenSize;

                return new(
                    screenSize.Width - (int)Width - ScreenSideMargin,
                    screenSize.Height - (int)Height - ScreenBottomMargin
                );
            }
        }

        public PixelPoint HiddenDesktopPosition
        {
            get
            {
                var screenSize = SystemParameters.PrimaryScreenSize;

                return new(
                    screenSize.Width - (int)Width - ScreenSideMargin,
                    screenSize.Height + ScreenBottomMargin
                );
            }
        }

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
        }

        protected override void OnLoaded()
        {
            var hwnd =  PlatformImpl!.Handle.Handle;
            
            ConfigureAsToolWindow(hwnd);
            
            IsVisible = false;
            Position = HiddenDesktopPosition;

            Message.Broadcast<MainWindowLoadedMessage>();
            base.OnLoaded();
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
            if (_animating)
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
            if (_animating || !IsVisible)
                return;

#if !DEBUG
            SlideOut();
#endif
        }

        /**
         * This is atrocious and I WISH AVALONIA COULD ANIMATE WINDOW POSITIONS FROM XAML.
         * "oooh just look at the samples!" my ass.
         *
         * Fuck you! Look what "looking at the samples" made me come up with.
         *
         * It works.
         *
         * I am so D O N E.
         *
         * Don't even talk to me about DPI-awareness in here.
         * Do it yourself if you need it. I don't give a shit.
         **/
        internal void SlideOut()
        {
            var vy = VisibleDesktopPosition.Y;
            var tx = HiddenDesktopPosition.X;
            var ty = HiddenDesktopPosition.Y;
            
            Task.Run(async () =>
            {
                Dispatcher.UIThread.Post(() =>
                {
                    Topmost = true;
                });
                
                _animating = true;
                {
                    double progress = 0;

                    while (progress < 1.0)
                    {
                        if (progress > 1.0)
                            progress = 1.0;

                        int y = (int)(vy + ty * _slideOutEasing.Ease(progress));
                        progress += _slideOutStep;

                        Position = new PixelPoint(tx, y);
                        await Task.Delay(1);
                    }
                }
                _animating = false;

                Dispatcher.UIThread.Post(() =>
                {
                    IsVisible = false;
                    Topmost = false;
                    
                    new MainWindowTransitionFinishedMessage(false)
                        .Broadcast();
                });
            });
        }

        internal void SlideIn()
        {
            var vy = HiddenDesktopPosition.Y;
            var tx = VisibleDesktopPosition.X;
            var ty = VisibleDesktopPosition.Y;

            Topmost = true;
            IsVisible = true;

            Task.Run(async () =>
            {
                _animating = true;
                {
                    double progress = 0;

                    while (progress < 1.0)
                    {
                        if (progress > 1.0)
                            progress = 1.0;
                        
                        int y = (int)((vy + (ScreenSideMargin * 2) /* wtf? */) - ty * _slideInEasing.Ease(progress));
                        progress += _slideInStep;
                        
                        Position = new PixelPoint(tx, y);
                        await Task.Delay(1);
                    }
                }
                _animating = false;

                Dispatcher.UIThread.Post(() =>
                {
                    new MainWindowTransitionFinishedMessage(true)
                        .Broadcast();
                });
            });
        }
    }
}