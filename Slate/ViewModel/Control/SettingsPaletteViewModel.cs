using Glitonea.Mvvm;
using Slate.Model.Messaging;
using Slate.View;

namespace Slate.ViewModel.Control
{
    public class SettingsPaletteViewModel : ViewModelBase
    {
        public void ActivatePage(PageMarker page)
        {
            new NavigationRequestedMessage(page)
                .Broadcast();
        }

        public void ActivateFansPage()
            => ActivatePage(Pages.Fans);

        public void ActivateGraphicsAndDisplayPage()
            => ActivatePage(Pages.GraphicsAndDisplay);

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