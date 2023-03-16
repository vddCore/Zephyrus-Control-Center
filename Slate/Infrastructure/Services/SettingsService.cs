using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Slate.Model.Settings;

namespace Slate.Infrastructure.Services
{
    public class SettingsService : ISettingsService
    {
        private const string AppSettingsDirectoryName = "ZephyrusControlCenter";
        private const string SettingsFileName = "zcc_settings.json";

        public string BaseDirectory { get; }
        private string SettingsFilePath { get; }

        public ControlCenterSettings? ControlCenter { get; private set; }

        public SettingsService()
        {
            BaseDirectory = GetOrCreateUserDataDirectory();
            
            SettingsFilePath = Path.Combine(
                BaseDirectory,
                SettingsFileName
            );

            ReadOrCreateSettings();
        }

        public async Task Save()
        {
            if (File.Exists(SettingsFilePath))
            {
                File.Delete(SettingsFilePath);
            }
            
            using (var fs = new FileStream(SettingsFilePath, FileMode.Create))
            {
                await JsonSerializer.SerializeAsync(fs, ControlCenter, new JsonSerializerOptions
                {
                    WriteIndented = true,
                });
            }
        }

        private string GetOrCreateUserDataDirectory()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appSettingsPath = Path.Combine(appDataPath, AppSettingsDirectoryName);

            Directory.CreateDirectory(appSettingsPath);

            return appSettingsPath;
        }

        private void ReadOrCreateSettings()
        {
            try
            {
                using (var sr = new StreamReader(SettingsFilePath))
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