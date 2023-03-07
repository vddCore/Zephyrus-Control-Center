using Glitonea.Mvvm;
using Glitonea.Mvvm.Messaging;
using Slate.Model;
using Slate.Model.Messaging;

namespace Slate.ViewModel.SubView
{
    public class PresetSelectorViewModel : ViewModelBase
    {
        public void ActivatePreset(object parameter)
        {
            var preset = parameter as PerformancePreset?;
            
            if (preset == null)
                return;
            
            Message.Broadcast(new PerformancePresetChangedMessage(preset.Value));
        }
    }
}