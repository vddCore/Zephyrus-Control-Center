using System;
using System.Diagnostics;
using System.IO;
using Glitonea.Mvvm;
using Slate.Infrastructure.Services;

namespace Slate.ViewModel.Page
{
    public class DebugPageViewModel : SingleInstanceViewModelBase
    {
        private const string AcpiRegistersOutputFileName = "acpi_registers";

        private IAsusHalService _asusHalService;
        private ISettingsService _settingsService;

        public DebugPageViewModel(
            IAsusHalService asusHalService,
            ISettingsService settingsService)
        {
            _asusHalService = asusHalService;
            _settingsService = settingsService;
        }

        public void DumpAcpiRegisters()
        {
            var dt = DateTime.Now.ToString("dd-MM-yy hh_mm_ss");

            using (var fs = new FileStream($"{AcpiRegistersOutputFileName}_{dt}.txt",FileMode.Create))
            {
                _asusHalService.DumpAcpiRegisters(fs);
            }
        }
        
        public void OpenAppDataDirectory() 
            => Process.Start("explorer.exe", _settingsService.BaseDirectory);

        public void OpenAppBaseDirectory() 
            => Process.Start("explorer.exe", AppContext.BaseDirectory);
    }
}