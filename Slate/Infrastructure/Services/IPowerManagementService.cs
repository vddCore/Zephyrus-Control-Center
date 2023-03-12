using Glitonea.Mvvm;
using Slate.Infrastructure.PowerManagement;

namespace Slate.Infrastructure.Services
{
    public interface IPowerManagementService  : IService
    {
        ProcessorBoostLevel ReadProcessorBoostState(PowerMode powerMode);
        void WriteProcessorBoostState(PowerMode powerMode, ProcessorBoostLevel boostLevel);

        void CommitCurrentPowerSchemeChanges();
    }
}