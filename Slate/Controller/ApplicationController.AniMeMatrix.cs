using Glitonea.Mvvm.Messaging;
using Slate.Model.Messaging;
using Slate.Model.Settings.Components;

namespace Slate.Controller
{
    public partial class ApplicationController
    {
        private AniMeMatrixSettings AniMeMatrixSettings => ControlCenterSettings.AniMeMatrix;

        private void SubscribeToAniMeMatrixSettings()
        {
            Message.Subscribe<AniMeMatrixBrightnessChangedMessage>(this, OnAniMeMatrixBrightnessChanged);
            Message.Subscribe<AniMeMatrixBuiltInsChangedMessage>(this, OnAniMeMatrixBuiltInsChanged);
        }

        private void OnAniMeMatrixBuiltInsChanged(AniMeMatrixBuiltInsChangedMessage msg)
        {
            _asusAnimeMatrixService.SetBuiltInAnimation(
                msg.PreferPowerSavingAnimation,
                msg.BuiltInConfiguration
            );
        }

        private void OnAniMeMatrixBrightnessChanged(AniMeMatrixBrightnessChangedMessage msg)
        {
            _asusAnimeMatrixService.SetBrightness(msg.BrightnessLevel);
        }
    }
}