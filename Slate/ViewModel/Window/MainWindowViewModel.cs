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
        private readonly IAsusHalService _asusHalService;
        
        public UserControl CurrentPage { get; private set; } = Pages.MainMenu;
        public string BackButtonClasses { get; private set; } = string.Empty;

        public MainWindowViewModel(IAsusHalService asusHalService)
        {
            _asusHalService = asusHalService;

            if (!_asusHalService.IsAcpiSessionOpen)
            {
                _asusHalService.OpenAcpiSession();
            }
            
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
                Message.Broadcast(new PageSwitchedMessage(Pages.MainMenu));
            }
        }

        private void OnPageSwitched(PageSwitchedMessage msg)
        {
            CurrentPage = msg.Page;
        }
    }
}