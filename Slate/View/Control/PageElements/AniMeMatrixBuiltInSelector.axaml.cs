using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using PropertyChanged;
using Starlight.Asus.AnimeMatrix;

namespace Slate.View.Control.PageElements
{
    [DoNotNotify]
    public partial class AniMeMatrixBuiltInSelector : UserControl
    {
        public static readonly StyledProperty<bool> IsRunningSelectorEnabledProperty =
            AvaloniaProperty.Register<AniMeMatrixBuiltInSelector, bool>(nameof(IsRunningSelectorEnabled));

        public static readonly StyledProperty<AnimeMatrixBuiltIn.Running> RunningAnimationProperty =
            AvaloniaProperty.Register<AniMeMatrixBuiltInSelector, AnimeMatrixBuiltIn.Running>(nameof(RunningAnimation));

        public static readonly StyledProperty<AnimeMatrixBuiltIn.Shutdown> ShutdownAnimationProperty =
            AvaloniaProperty.Register<AniMeMatrixBuiltInSelector, AnimeMatrixBuiltIn.Shutdown>(
                nameof(ShutdownAnimation));

        public static readonly StyledProperty<AnimeMatrixBuiltIn.Sleeping> SleepingAnimationProperty =
            AvaloniaProperty.Register<AniMeMatrixBuiltInSelector, AnimeMatrixBuiltIn.Sleeping>(
                nameof(SleepingAnimation));

        public static readonly StyledProperty<AnimeMatrixBuiltIn.Startup> StartupAnimationProperty =
            AvaloniaProperty.Register<AniMeMatrixBuiltInSelector, AnimeMatrixBuiltIn.Startup>(nameof(StartupAnimation));

        public bool IsRunningSelectorEnabled
        {
            get => GetValue(IsRunningSelectorEnabledProperty);
            set => SetValue(IsRunningSelectorEnabledProperty, value);
        }

        public AnimeMatrixBuiltIn.Running RunningAnimation
        {
            get => GetValue(RunningAnimationProperty);
            set => SetValue(RunningAnimationProperty, value);
        }

        public AnimeMatrixBuiltIn.Shutdown ShutdownAnimation
        {
            get => GetValue(ShutdownAnimationProperty);
            set => SetValue(ShutdownAnimationProperty, value);
        }

        public AnimeMatrixBuiltIn.Sleeping SleepingAnimation
        {
            get => GetValue(SleepingAnimationProperty);
            set => SetValue(SleepingAnimationProperty, value);
        }

        public AnimeMatrixBuiltIn.Startup StartupAnimation
        {
            get => GetValue(StartupAnimationProperty);
            set => SetValue(StartupAnimationProperty, value);
        }

        public AniMeMatrixBuiltInSelector()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);

            switch (change.Property.Name)
            {
                case nameof(RunningAnimation):
                case nameof(ShutdownAnimation):
                case nameof(SleepingAnimation):
                case nameof(StartupAnimation):
                    UpdateVisualState();
                    break;
            }
        }

        private void UpdateVisualState()
        {
            UpdateRunningButtonVisualState();
            UpdateShutdownButtonVisualState();
            UpdateSleepingButtonVisualState();
            UpdateStartupButtonVisualState();
        }

        private void UpdateRunningButtonVisualState()
        {
            RunningButtonA.IsChecked = RunningAnimation == AnimeMatrixBuiltIn.Running.BinaryBannerScroll;
            RunningButtonB.IsChecked = RunningAnimation == AnimeMatrixBuiltIn.Running.RogLogoGlitch;
        }

        private void UpdateShutdownButtonVisualState()
        {
            ShutdownButtonA.IsChecked = ShutdownAnimation == AnimeMatrixBuiltIn.Shutdown.GlitchOut;
            ShutdownButtonB.IsChecked = ShutdownAnimation == AnimeMatrixBuiltIn.Shutdown.SeeYa;
        }

        private void UpdateSleepingButtonVisualState()
        {
            SleepingButtonA.IsChecked = SleepingAnimation == AnimeMatrixBuiltIn.Sleeping.BannerSwipe;
            SleepingButtonB.IsChecked = SleepingAnimation == AnimeMatrixBuiltIn.Sleeping.Starfield;
        }

        private void UpdateStartupButtonVisualState()
        {
            StartupButtonA.IsChecked = StartupAnimation == AnimeMatrixBuiltIn.Startup.GlitchConstruction;
            StartupButtonB.IsChecked = StartupAnimation == AnimeMatrixBuiltIn.Startup.StaticEmergence;
        }

        private void RunningButtonA_OnClick(object? sender, RoutedEventArgs e)
            => RunningAnimation = AnimeMatrixBuiltIn.Running.BinaryBannerScroll;

        private void RunningButtonB_OnClick(object? sender, RoutedEventArgs e)
            => RunningAnimation = AnimeMatrixBuiltIn.Running.RogLogoGlitch;

        private void ShutdownButtonA_OnClick(object? sender, RoutedEventArgs e)
            => ShutdownAnimation = AnimeMatrixBuiltIn.Shutdown.GlitchOut;

        private void ShutdownButtonB_OnClick(object? sender, RoutedEventArgs e)
            => ShutdownAnimation = AnimeMatrixBuiltIn.Shutdown.SeeYa;

        private void SleepingButtonA_OnClick(object? sender, RoutedEventArgs e)
            => SleepingAnimation = AnimeMatrixBuiltIn.Sleeping.BannerSwipe;

        private void SleepingButtonB_OnClick(object? sender, RoutedEventArgs e)
            => SleepingAnimation = AnimeMatrixBuiltIn.Sleeping.Starfield;

        private void StartupButtonA_OnClick(object? sender, RoutedEventArgs e)
            => StartupAnimation = AnimeMatrixBuiltIn.Startup.GlitchConstruction;

        private void StartupButtonB_OnClick(object? sender, RoutedEventArgs e)
            => StartupAnimation = AnimeMatrixBuiltIn.Startup.StaticEmergence;
    }
}