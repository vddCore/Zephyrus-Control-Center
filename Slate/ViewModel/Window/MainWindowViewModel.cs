using Avalonia;
using Avalonia.Controls;
using Glitonea.Extensions;
using Glitonea.Mvvm;
using Glitonea.Mvvm.Messaging;
using Slate.Controller;
using Slate.Infrastructure.Services;
using Slate.Model.Messaging;
using Slate.View;
using Slate.View.Window;

namespace Slate.ViewModel.Window
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly ApplicationController _applicationController;

        public PageMarker? CurrentPage { get; private set; }

        public MainWindowViewModel(
            IAsusHalService asusHalService,
            ISettingsService settingsService)
        {
            _settingsService = settingsService;
            _applicationController = new ApplicationController(asusHalService, settingsService);

            Message.Subscribe<MainWindowLoadedMessage>(this, OnMainWindowLoaded);
            Message.Subscribe<MainWindowTransitionFinishedMessage>(this, OnMainWindowTransitionFinished);
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
        private void SetCurrentPage(PageMarker? page)
        {
            new PageSwitchedMessage(page)
                .Broadcast();
        }

        private void OnMainWindowLoaded(MainWindowLoadedMessage _)
        {
            SetCurrentPage(Pages.MainMenu);
        }
        
        private void OnMainWindowTransitionFinished(MainWindowTransitionFinishedMessage msg)
        {
            if (!msg.WasSlidingIn)
            {
                SetCurrentPage(Pages.MainMenu);
            }
        }

        private void OnPageSwitched(PageSwitchedMessage msg)
        {
            CurrentPage = msg.PageMarker;
        }

        private async void OnSettingsModified(SettingsModifiedMessage _)
        {
            await _settingsService.Save();
        }
    }
}