using System.Threading.Tasks;
using Avalonia;
using Glitonea.Extensions;
using Glitonea.Mvvm;
using Glitonea.Mvvm.Messaging;
using Slate.Infrastructure;
using Slate.Infrastructure.Services;
using Slate.Model.Messaging;
using Slate.Model.Settings.Components;
using Slate.View.Window;
using Slate.ViewModel.Window;

namespace Slate.ViewModel.Page
{
    public class GraphicsAndDisplayPageViewModel : ViewModelBase
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