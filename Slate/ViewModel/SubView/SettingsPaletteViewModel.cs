using Avalonia.Controls;
using Glitonea.Mvvm;
using Slate.Model.Messaging;
using Slate.View;

namespace Slate.ViewModel.SubView
{
    public class SettingsPaletteViewModel : ViewModelBase
    {
        public void ActivatePage(UserControl page)
        {
            new PageSwitchedMessage(page)
                .Broadcast();
        }

        public void ActivateProcessorPage()
            => ActivatePage(Pages.Processor);

        public void ActivateGraphicsAndDisplayPage()
            => ActivatePage(Pages.GrapicsAndDisplay);
        
        public void ActivatePowerManagementPage()
            => ActivatePage(Pages.PowerManagement);
        
        public void ActivateKeyboardPage()
            => ActivatePage(Pages.Keyboard);

        public void ActivateAniMeMatrixPage()
            => ActivatePage(Pages.AniMeMatrix);
        
        public void ActivateApplicationPage()
            => ActivatePage(Pages.Application);

    }
}