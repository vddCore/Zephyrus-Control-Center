using System.Collections.Generic;
using Avalonia;
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

        private Stack<PageMarker> _navigationStack = new();

        public PageMarker? CurrentPage
        {
            get
            {
                if (_navigationStack.TryPeek(out var page))
                    return page;

                return null;
            }
        }

        public bool CanNavigateBack => _navigationStack.Count > 1;
        public bool BackButtonEnabled { get; private set; } = true;

        public MainWindowViewModel(
            IAsusHalService asusHalService,
            ISettingsService settingsService,
            IPowerManagementService powerManagementService,
            IDisplayManagementService displayManagementService,
            IAsusAuraService asusAuraService,
            IInputInjectionService inputInjectionService,
            IAsusAnimeMatrixService asusAnimeMatrixService)
        {
            _settingsService = settingsService;

            _applicationController = new ApplicationController(
                asusHalService,
                settingsService,
                powerManagementService,
                displayManagementService,
                asusAuraService,
                inputInjectionService,
                asusAnimeMatrixService
            );

            Message.Subscribe<MainWindowLoadedMessage>(this, OnMainWindowLoaded);
            Message.Subscribe<MainWindowTransitionFinishedMessage>(this, OnMainWindowTransitionFinished);
            Message.Subscribe<SettingsModifiedMessage>(this, OnSettingsModified);
            Message.Subscribe<NavigatedToPageMessage>(this, OnNavigatedToPage);
            Message.Subscribe<NavigatedBackMessage>(this, OnNavigatedBack);
            Message.Subscribe<NavigationRequestedMessage>(this, OnNavigationRequested);
            Message.Subscribe<EcoModeTransitionStartedMessage>(this, OnEcoModeTransitionStarted);
            Message.Subscribe<EcoModeTransitionFinishedMessage>(this, OnEcoModeTransitionFinished);
        }

        private void OnEcoModeTransitionStarted(EcoModeTransitionStartedMessage _)
            => BackButtonEnabled = false;

        private void OnEcoModeTransitionFinished(EcoModeTransitionFinishedMessage _)
            => BackButtonEnabled = true;

        public void NavigateBack()
        {
            if (CurrentPage == Pages.MainMenu)
            {
                var window = (Application.Current.GetDesktopLifetime().MainWindow as MainWindow)!;
                
                if (window.Elevated)
                {
                    NavigateTo(Pages.Debug);
                }
                else
                {
                    window.SlideOut();
                }
            }
            else
            {
                _navigationStack.Pop();

                new NavigatedBackMessage(CurrentPage!)
                    .Broadcast();
            }
        }

        private void NavigateTo(PageMarker pageMarker)
        {
            if (CurrentPage == pageMarker)
                return;
            
            _navigationStack.Push(pageMarker);

            new NavigatedToPageMessage(pageMarker)
                .Broadcast();
        }

        private void NavigateToTop()
        {
            _navigationStack.Clear();
            NavigateTo(Pages.MainMenu);
        }

        private void OnNavigationRequested(NavigationRequestedMessage msg)
            => NavigateTo(msg.PageMarker);
        
        private void OnNavigatedToPage(NavigatedToPageMessage msg) 
            => OnPropertyChanged(nameof(CurrentPage));

        private void OnNavigatedBack(NavigatedBackMessage msg) 
            => OnPropertyChanged(nameof(CurrentPage));

        private void OnMainWindowLoaded(MainWindowLoadedMessage _)
        {
            NavigateToTop();
        }

        private void OnMainWindowTransitionFinished(MainWindowTransitionFinishedMessage msg)
        {
            if (!msg.WasSlidingIn)
            {
                NavigateToTop();
            }
        }

        private async void OnSettingsModified(SettingsModifiedMessage _)
        {
            await _settingsService.Save();
        }
    }
}