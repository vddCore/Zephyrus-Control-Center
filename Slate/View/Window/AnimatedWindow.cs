using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation.Easings;
using Avalonia.Threading;
using PropertyChanged;
using Slate.Infrastructure;
using Slate.Model.Messaging;

namespace Slate.View.Window
{
    [DoNotNotify]
    public class AnimatedWindow : Avalonia.Controls.Window
    {
        private readonly Easing _slideOutEasing = Easing.Parse("CubicEaseIn");
        private readonly Easing _slideInEasing = Easing.Parse("CubicEaseOut");
        private readonly Easing _resizeEasing = Easing.Parse("CubicEaseOut");

        private const int SideMargin = 12;

        private double _slideInStep = 0.038;
        private double _slideOutStep = 0.038;
        private double _resizeStep = 0.038;

        protected bool Animating { get; private set; }

        public WindowSlideOrigin SlideOrigin { get; private set; } = WindowSlideOrigin.Bottom;

        public PixelPoint VisibleDesktopPosition
        {
            get
            {
                var workAreaSize = SystemParameters.WorkingAreaSize;

                return SlideOrigin switch
                {
                    WindowSlideOrigin.Bottom => new(
                        workAreaSize.Width - (int)Width - SideMargin,
                        workAreaSize.Height - (int)Height - SideMargin
                    ),

                    _ => throw new NotImplementedException(
                        "Not implemented yet. Want it NOW? Do it yourself. No handouts <3"
                    )
                };
            }
        }

        public PixelPoint HiddenDesktopPosition
        {
            get
            {
                var workAreaSize = SystemParameters.PrimaryScreenSize;

                return SlideOrigin switch
                {
                    WindowSlideOrigin.Bottom => new(
                        workAreaSize.Width - (int)Width - SideMargin,
                        workAreaSize.Height + SideMargin
                    ),

                    _ => throw new NotImplementedException(
                        "Not implemented yet. Want it NOW? Do it yourself. No handouts <3"
                    )
                };
            }
        }

        internal void ResizeTo(double targetHeight)
        {
            var target = Math.Abs(Height - targetHeight);
            var sizingDown = Height > targetHeight;
            var amount = 0.0;

            var sh = Height;
            var sy = Position.Y;

            Animate(
                (progress) =>
                {
                    Dispatcher.UIThread.Post(() =>
                    {
                        if (sizingDown)
                        {
                            Height = sh - amount;
                            Position = new PixelPoint(Position.X, (int)(sy + amount));
                        }
                        else
                        {
                            Height = sh + amount;
                            Position = new PixelPoint(Position.X, (int)(sy - amount));
                        }
                        amount = target * _resizeEasing.Ease(progress);
                    });

                    return _resizeStep;
                },
                after: () =>
                {
                    if (sizingDown)
                    {
                        Height = sh - target;
                        Position = new PixelPoint(Position.X, (int)(sy + target));
                    }
                    else
                    {
                        Height = sh + target;
                        Position = new PixelPoint(Position.X, (int)(sy - target));
                    }
                }
            );
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
            var target = Math.Abs(Position.Y - HiddenDesktopPosition.Y);
            var amount = 0.0;
            var sy = Position.Y;
            var tx = VisibleDesktopPosition.X;

            Animate(
                (progress) =>
                {
                    Position = new PixelPoint(tx, (int)(sy + amount));

                    amount = target * _slideOutEasing.Ease(progress);
                    return _slideOutStep;
                },
                () => Topmost = true,
                () =>
                {
                    IsVisible = false;
                    Topmost = false;

                    new MainWindowTransitionFinishedMessage(false)
                        .Broadcast();
                }
            );
        }

        internal void SlideIn()
        {
            var target = Math.Abs(Position.Y - VisibleDesktopPosition.Y);
            var amount = 0.0;

            var sy = Position.Y;
            var tx = VisibleDesktopPosition.X;

            Animate(
                (progress) =>
                {
                    Position = new PixelPoint(tx, (int)(sy - amount));

                    amount = target * _slideInEasing.Ease(progress);
                    return _slideInStep;
                },
                () =>
                {
                    Topmost = true;
                    IsVisible = true;
                },
                () =>
                {
                    new MainWindowTransitionFinishedMessage(true)
                        .Broadcast();
                }
            );
        }

        private void Animate(Func<double, double> action, Action? before = null, Action? after = null)
        {
            Task.Run(async () =>
            {
                Dispatcher.UIThread.Post(() => { before?.Invoke(); });

                Animating = true;
                {
                    double progress = 0;

                    while (progress < 1.0)
                    {
                        if (progress > 1.0)
                            progress = 1.0;

                        progress += action(progress);
                        await Task.Delay(1);
                    }
                }
                Animating = false;

                Dispatcher.UIThread.Post(() => { after?.Invoke(); });
            });
        }
    }
}