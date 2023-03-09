using Avalonia;
using Avalonia.Controls;
using Glitonea.Extensions;
using Glitonea.Mvvm;
using Glitonea.Mvvm.Messaging;
using Slate.Infrastructure.Services;
using Slate.Model.Messaging;
using Slate.Model.Settings.Components;
using Slate.View;
using Slate.View.Window;

namespace Slate.ViewModel.Window
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IAsusHalService _asusHalService;
        private readonly ISettingsService _settingsService;

        public UserControl CurrentPage { get; private set; } = Pages.MainMenu;

        public ApplicationSettings ApplicationSettings => _settingsService.ControlCenter!.Application;

        public MainWindowViewModel(
            IAsusHalService asusHalService,
            ISettingsService settingsService)
        {
            _asusHalService = asusHalService;
            _settingsService = settingsService;

            Message.Subscribe<SettingsModifiedMessage>(this, OnSettingsModified);
            Message.Subscribe<PageSwitchedMessage>(this, OnPageSwitched);
            
            if (!_asusHalService.IsAcpiSessionOpen)
            {
                _asusHalService.OpenAcpiSession();
            }
        }

        public void NavigateBack()
        {
            if (CurrentPage == Pages.MainMenu)
            {
                (Application.Current.GetDesktopLifetime().MainWindow as MainWindow)!.SlideOut();
            }
            else
            {
                Message.Broadcast(new PageSwitchedMessage(Pages.MainMenu));
            }
        }

        private void OnPageSwitched(PageSwitchedMessage msg)
        {
            CurrentPage = msg.Page;
        }

        private async void OnSettingsModified(SettingsModifiedMessage _)
        {
            await _settingsService.Save();
        }
    }
}