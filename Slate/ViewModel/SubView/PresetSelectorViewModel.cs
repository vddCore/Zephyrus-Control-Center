using Glitonea.Mvvm;
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

            new PerformancePresetChangedMessage(preset.Value)
                .Broadcast();
        }
    }
}