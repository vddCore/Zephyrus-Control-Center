using Glitonea.Mvvm.Messaging;
using Slate.Model.Messaging;
using Slate.Model.Settings.Components;

namespace Slate.Controller
{
    public partial class ApplicationController
    {
        private GraphicsAndDisplaySettings GraphicsAndDisplaySettings =>
            _settingsService.ControlCenter!.GraphicsAndDisplay;

        private void SubscribeToGraphicsAndDisplaySettings()
        {
        }
    }
}