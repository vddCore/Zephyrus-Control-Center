using System;
using System.Diagnostics;
using Glitonea.Mvvm;
using Slate.Infrastructure;
using Slate.Infrastructure.Services;

namespace Slate.ViewModel.Page
{
    public class DebugPageViewModel : SingleInstanceViewModelBase
    {
        private const string AcpiRegistersOutputFileName = "wmi_registers";
        private const string WmiRegDumpsDirectoryName = "wmi_regdumps";
        private const string AcpiTableDumpsDirectoryName = "acpi_tables";

        private IAsusHalService _asusHalService;
        private IStorageService _storageService;

        public DebugPageViewModel(
            IAsusHalService asusHalService,
            IStorageService storageService)
        {
            _asusHalService = asusHalService;
            _storageService = storageService;
        }

        public void DumpWmiRegisters()
        {
            var dt = DateTime.Now.ToString("dd-MM-yy_hh_mm_ss");

            using (var fs = _storageService.CreateFile($"{WmiRegDumpsDirectoryName}/{AcpiRegistersOutputFileName}_{dt}.txt"))
            {
                _asusHalService.DumpWmiRegisters(fs);
            }
        }

        public void DumpAcpiTables()
        {
            var dt = DateTime.Now.ToString("dd-MM-yy_hh_mm_ss");
            
            var firmwareAcpiTableList = _asusHalService.FetchFirmwareAcpiTableList();
            foreach (var table in firmwareAcpiTableList)
            {
                var tableName = table.ToFourCharacterCode();
                using (var fs = _storageService.CreateFile($"{AcpiTableDumpsDirectoryName}/{dt}/{tableName}.acpi"))
                {
                    _asusHalService.DumpFirmwareAcpiTable(table, fs);
                }
            }

            var registryAcpiTableList = _asusHalService.FetchRegistryAcpiTableList();
            foreach (var table in registryAcpiTableList)
            {
                var tableName = table.ToFourCharacterCode();
                using (var fs = _storageService.CreateFile($"{AcpiTableDumpsDirectoryName}/{dt}/{tableName}.acpi"))
                {
                    _asusHalService.DumpRegistryAcpiTable(table, fs);
                }
            }
        }

        public void OpenAppDataDirectory()
            => Process.Start("explorer.exe", _storageService.BaseDirectory);

        public void OpenAppBaseDirectory()
            => Process.Start("explorer.exe", AppContext.BaseDirectory);
    }
}