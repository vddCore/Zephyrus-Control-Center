using Glitonea.Mvvm;
using Glitonea.Mvvm.Messaging;
using Slate.Model.Messaging;
using Slate.View;

namespace Slate.ViewModel.SubView
{
    public class SettingsPaletteViewModel : ViewModelBase
    {
        public void ActivateProcessorPage()
        {
            Message.Broadcast(new PageSwitchedMessage(Pages.Processor));
        }

        public void ActivateGraphicsPage()
        {
        }

        public void ActivateKeyboardPage()
        {
        }

        public void ActivateAniMeMatrixPage()
        {
        }

        public void ActivateDisplayPage()
        {
        }
    }
}