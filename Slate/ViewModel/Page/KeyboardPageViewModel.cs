﻿using Glitonea.Mvvm;
using Slate.Infrastructure.Services;
using Slate.Model.Messaging;
using Slate.Model.Settings.Components;
using Slate.View;

namespace Slate.ViewModel.Page
{
    public class KeyboardPageViewModel : SingleInstanceViewModelBase
    {
        private ISettingsService _settingsService;

        public KeyboardSettings KeyboardSettings => _settingsService.ControlCenter!.Keyboard;

        public KeyboardPageViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public void ActivateKeyBindingsPage()
        {
            new NavigationRequestedMessage(Pages.KeyboardBindings)
                .Broadcast();
        }
    }
}