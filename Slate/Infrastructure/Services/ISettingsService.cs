using System.Threading.Tasks;
using Glitonea.Mvvm;
using Slate.Model.Settings;

namespace Slate.Infrastructure.Services
{
    public interface ISettingsService : IService
    {
        ControlCenterSettings? ControlCenter { get; }

        Task Save();
    }
}