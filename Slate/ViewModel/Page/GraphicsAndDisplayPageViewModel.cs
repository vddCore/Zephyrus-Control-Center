using System.Threading.Tasks;
using Avalonia;
using Glitonea.Extensions;
using Glitonea.Mvvm;
using Glitonea.Mvvm.Messaging;
using Slate.Infrastructure.Asus;
using Slate.Infrastructure.Services;
using Slate.Model.Messaging;
using Slate.Model.Settings.Components;
using Slate.View.Window;
using Slate.ViewModel.Window;

namespace Slate.ViewModel.Page
{
    public class GraphicsAndDisplayPageViewModel : SingleInstanceViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly IShutdownService _shutdownService;

        private MuxSwitchMode _attemptedTargetMode;

        public bool IsDeciding { get; set; }
        public double DecisionBoxOpacity { get; set; }

        public bool IsDiscreteExclusiveMode
        {
            get => GraphicsAndDisplaySettings.MuxSwitchMode == MuxSwitchMode.Discrete;

            set
            {
                GraphicsAndDisplaySettings.MuxSwitchMode = value
                    ? MuxSwitchMode.Discrete
                    : MuxSwitchMode.Switched;

                OnPropertyChanged(nameof(IsDiscreteExclusiveMode));
            }
        }

        public bool IsEcoModeEnabled => GraphicsAndDisplaySettings.IsEcoModeEnabled!.Value;
        public bool IsSwitchingEcoModes { get; private set; }
        public string EcoModeSwitchText { get; private set; } = "Prioritize power saving";

        public bool Is60HzSelected => GraphicsAndDisplaySettings.DisplayRefreshRate == 60;
        public bool Is75HzSelected => GraphicsAndDisplaySettings.DisplayRefreshRate == 75;
        public bool Is100HzSelected => GraphicsAndDisplaySettings.DisplayRefreshRate == 100;
        public bool Is120HzSelected => GraphicsAndDisplaySettings.DisplayRefreshRate == 120;
        public bool Is144HzSelected => GraphicsAndDisplaySettings.DisplayRefreshRate == 144;

        public GraphicsAndDisplaySettings GraphicsAndDisplaySettings =>
            _settingsService.ControlCenter!.GraphicsAndDisplay;

        public GraphicsAndDisplayPageViewModel(
            ISettingsService settingsService,
            IShutdownService shutdownService)
        {
            _settingsService = settingsService;
            _shutdownService = shutdownService;
        }

        public void TryEnableSwitchedGraphicsMode()
        {
            if (!IsDiscreteExclusiveMode)
                return;

            _attemptedTargetMode = MuxSwitchMode.Switched;
            ShowDecisionBox();
        }

        public void TryEnableDiscreteGraphicsMode()
        {
            if (IsDiscreteExclusiveMode)
                return;

            _attemptedTargetMode = MuxSwitchMode.Discrete;
            ShowDecisionBox();
        }

        /**
         * Transition to eco-mode takes some time for the EC
         * to facilitate, so we run this asynchronously.
         **/
        public async Task BeginEcoModeTransition()
        {
            await Task.Run(() =>
            {
                Message.Broadcast<EcoModeTransitionStartedMessage>();
                EcoModeSwitchText = "Waiting for embedded controller...";
                IsSwitchingEcoModes = true;
                GraphicsAndDisplaySettings.IsEcoModeEnabled = !GraphicsAndDisplaySettings.IsEcoModeEnabled;
                OnPropertyChanged(nameof(IsEcoModeEnabled));
                IsSwitchingEcoModes = false;
                EcoModeSwitchText = "Prioritize power saving";
                Message.Broadcast<EcoModeTransitionFinishedMessage>();
            });
        }

        public void ProceedWithModeSwitch()
        {
            GraphicsAndDisplaySettings.MuxSwitchMode = _attemptedTargetMode;
            HideDecisionBox();

            /* Ah yes, special cases. Don't we love them. <3 */
            ((Application.Current.GetMainWindow() as MainWindow)!.DataContext as MainWindowViewModel)!.NavigateBack();
            ((Application.Current.GetMainWindow() as MainWindow)!.DataContext as MainWindowViewModel)!.NavigateBack();
#if !DEBUG
            _shutdownService.RequestSystemReboot("Zephyrus Control Center: graphics mode switch.");
#endif
        }

        public void SetDisplayRefreshRate(object? parameter)
        {
            if (parameter is string str && uint.TryParse(str, out var refreshRate))
            {
                GraphicsAndDisplaySettings.DisplayRefreshRate = refreshRate;
            }
            
            OnPropertyChanged(nameof(Is60HzSelected));
            OnPropertyChanged(nameof(Is75HzSelected));
            OnPropertyChanged(nameof(Is100HzSelected));
            OnPropertyChanged(nameof(Is120HzSelected));
            OnPropertyChanged(nameof(Is144HzSelected));
        }

        public void AbandonModeSwitch()
        {
            HideDecisionBox();
        }

        private void ShowDecisionBox()
        {
            IsDeciding = true;
            DecisionBoxOpacity = 1.0;
        }

        private void HideDecisionBox()
        {
            IsDeciding = false;
            DecisionBoxOpacity = 0;
        }
    }
}