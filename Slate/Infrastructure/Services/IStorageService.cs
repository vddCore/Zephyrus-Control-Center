using System.IO;
using Glitonea.Mvvm;

namespace Slate.Infrastructure.Services
{
    public interface IStorageService : IService
    {
        string BaseDirectory { get; }

        FileStream CreateFile(string storageRelativePath);
        FileStream OpenOrCreateFile(string storageRelativePath);
    }
}