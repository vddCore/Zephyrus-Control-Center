using System.Diagnostics;
using Glitonea.Mvvm;
using Slate.Infrastructure.Services;
using Slate.Model.Settings.Components;

namespace Slate.ViewModel.Page
{
    public class ApplicationPageViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;

        public ApplicationSettings ApplicationSettings => _settingsService.ControlCenter!.Application;

        public ApplicationPageViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }
        
        public void OpenPage(object? parameter)
        {
            if (parameter is not string link)
                return;
            
            Process.Start(new ProcessStartInfo(link)
            {
                UseShellExecute = true
            });
        }
    }
}