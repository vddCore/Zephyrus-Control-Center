using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Slate.Model.Settings;

namespace Slate.Infrastructure.Services
{
    public class SettingsService : ISettingsService
    {
        private const string SettingsFileName = "zcc_settings.json";

        private readonly IStorageService _storageService;

        public ControlCenterSettings? ControlCenter { get; private set; }

        public SettingsService(IStorageService storageService)
        {
            _storageService = storageService;
            ReadOrCreateSettings();
        }

        public async Task Save()
        {
            using (var fs = _storageService.CreateFile(SettingsFileName))
            {
                await JsonSerializer.SerializeAsync(fs, ControlCenter, new JsonSerializerOptions
                {
                    WriteIndented = true,
                });
            }
        }

        private void ReadOrCreateSettings()
        {
            try
            {
                using (var fs = _storageService.OpenOrCreateFile(SettingsFileName))
                using (var sr = new StreamReader(fs))
                {
                    ControlCenter = JsonSerializer.Deserialize<ControlCenterSettings>(
                        sr.ReadToEnd()
                    );
                }
            }
            catch (Exception)
            {
                // todo show a message box, log a failure. anything.
                ControlCenter = new ControlCenterSettings();
                Save().Wait();
            }
        }
    }
}