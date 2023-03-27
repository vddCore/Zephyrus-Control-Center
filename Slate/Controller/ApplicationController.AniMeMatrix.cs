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
        }

        private void OnAniMeMatrixBrightnessChanged(AniMeMatrixBrightnessChangedMessage msg)
        {
            _asusAnimeMatrixService.SetBrightness(msg.BrightnessLevel);
        }
    }
}