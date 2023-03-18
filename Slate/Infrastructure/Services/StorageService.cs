using System;
using System.IO;

namespace Slate.Infrastructure.Services
{
    public class StorageService : IStorageService
    {
        private const string AppStorageDirectoryName = "ZephyrusControlCenter";
        
        public string BaseDirectory { get; }

        public StorageService()
        {
            BaseDirectory = GetOrCreateUserDataDirectory();
        }

        public FileStream CreateFile(string storageRelativePath)
        {
            var absolutePath = Path.Combine(BaseDirectory, storageRelativePath);
            var targetDirectory = Path.GetDirectoryName(absolutePath);

            Directory.CreateDirectory(targetDirectory!);

            return new FileStream(
                absolutePath,
                FileMode.Create,
                FileAccess.ReadWrite,
                FileShare.Read
            );
        }

        public FileStream OpenOrCreateFile(string storageRelativePath)
        {
            var absolutePath = Path.Combine(BaseDirectory, storageRelativePath);
            var targetDirectory = Path.GetDirectoryName(absolutePath);

            Directory.CreateDirectory(targetDirectory!);

            return new FileStream(
                absolutePath,
                FileMode.OpenOrCreate,
                FileAccess.ReadWrite,
                FileShare.Read
            );
        }

        private string GetOrCreateUserDataDirectory()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appSettingsPath = Path.Combine(appDataPath, AppStorageDirectoryName);

            Directory.CreateDirectory(appSettingsPath);

            return appSettingsPath;
        }
    }
}