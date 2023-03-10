using Glitonea.Mvvm;
using Slate.Model.Messaging;
using Slate.View;

namespace Slate.ViewModel.SubView
{
    public class SettingsPaletteViewModel : ViewModelBase
    {
        public void ActivateProcessorPage()
        {
            new PageSwitchedMessage(Pages.Processor)
                .Broadcast();
        }

        public void ActivateGraphicsAndDisplayPage()
        {
            new PageSwitchedMessage(Pages.GrapicsAndDisplay)
                .Broadcast();
        }
        
        public void ActivatePowerManagementPage()
        {
            new PageSwitchedMessage(Pages.PowerManagement)
                .Broadcast();
        }

        public void ActivateKeyboardPage()
        {
            new PageSwitchedMessage(Pages.Keyboard)
                .Broadcast();
        }

        public void ActivateAniMeMatrixPage()
        {
            new PageSwitchedMessage(Pages.AniMeMatrix)
                .Broadcast();
        }
        
        public void ActivateApplicationPage()
        {
            new PageSwitchedMessage(Pages.Application)
                .Broadcast();
        }
    }
}