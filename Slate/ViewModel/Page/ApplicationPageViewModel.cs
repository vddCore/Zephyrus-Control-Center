using System.Diagnostics;
using System.Reflection;
using Glitonea.Mvvm;
using Slate.Infrastructure.Services;
using Slate.Model.Settings.Components;

namespace Slate.ViewModel.Page
{
    public class ApplicationPageViewModel : SingleInstanceViewModelBase
    {
        private readonly ISettingsService _settingsService;

        public ApplicationSettings ApplicationSettings => _settingsService.ControlCenter!.Application;

        public string VersionString
        {
            get
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;

                if (version == null)
                    return "W.T.F?";

                return $"{version.Major}.{version.Minor}.{version.Build}";
            }
        }

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