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
        private IStorageService _storageService;

        public DebugPageViewModel(
            IAsusHalService asusHalService,
            IStorageService storageService)
        {
            _asusHalService = asusHalService;
            _storageService = storageService;
        }

        public void DumpAcpiRegisters()
        {
            var dt = DateTime.Now.ToString("dd-MM-yy hh_mm_ss");

            using (var fs = _storageService.CreateFile($"acpi_dumps/{AcpiRegistersOutputFileName}_{dt}.txt"))
            {
                _asusHalService.DumpAcpiRegisters(fs);
            }
        }
        
        public void OpenAppDataDirectory() 
            => Process.Start("explorer.exe", _storageService.BaseDirectory);

        public void OpenAppBaseDirectory() 
            => Process.Start("explorer.exe", AppContext.BaseDirectory);
    }
}