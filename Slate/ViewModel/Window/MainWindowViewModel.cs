using Avalonia;
using Avalonia.Controls;
using Glitonea.Extensions;
using Glitonea.Mvvm;
using Glitonea.Mvvm.Messaging;
using Slate.Infrastructure.Services;
using Slate.Model.Messaging;
using Slate.View;
using Slate.View.Window;

namespace Slate.ViewModel.Window
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;

        public UserControl? CurrentPage { get; private set; }

        public MainWindowViewModel(
            IAsusHalService asusHalService,
            ISettingsService settingsService)
        {
            if (!asusHalService.IsAcpiSessionOpen)
                asusHalService.OpenAcpiSession();
            
            _settingsService = settingsService;

            Message.Subscribe<MainWindowLoadedMessage>(this, OnMainWindowLoaded);
            Message.Subscribe<SettingsModifiedMessage>(this, OnSettingsModified);
            Message.Subscribe<PageSwitchedMessage>(this, OnPageSwitched);
        }

        public void NavigateBack()
        {
            if (CurrentPage == Pages.MainMenu)
            {
                (Application.Current.GetDesktopLifetime().MainWindow as MainWindow)!.SlideOut();
            }
            else
            {
                SetCurrentPage(Pages.MainMenu);
            }
        }
        private void SetCurrentPage(UserControl? page)
        {
            new PageSwitchedMessage(page)
                .Broadcast();
        }

        private void OnMainWindowLoaded(MainWindowLoadedMessage obj)
        {
            SetCurrentPage(Pages.MainMenu);
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