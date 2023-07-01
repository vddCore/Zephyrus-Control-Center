using System;
using Slate.Infrastructure.Native;
using Slate.Infrastructure.PowerManagement;

namespace Slate.Infrastructure.Services.Implementations
{
    public class PowerManagementService : IPowerManagementService
    {
        public ProcessorBoostLevel ReadProcessorBoostState(PowerMode powerMode)
        {
            PowrProf.PowerGetActiveScheme(0, out var activeSchemeGuid);
            
            if (powerMode == PowerMode.AC)
            {
                PowrProf.PowerReadACValueIndex(
                    nint.Zero,
                    activeSchemeGuid,
                    PowrProf.GUID_PROCESSOR_SETTINGS_SUBGROUP,
                    PowrProf.GUID_PROCESSOR_PERFORMANCE_BOOST_SETTING,
                    out int value
                );
                
                return (ProcessorBoostLevel)value;
            }
            else if (powerMode == PowerMode.DC)
            {
                PowrProf.PowerReadDCValueIndex(
                    nint.Zero,
                    activeSchemeGuid,
                    PowrProf.GUID_PROCESSOR_SETTINGS_SUBGROUP,
                    PowrProf.GUID_PROCESSOR_PERFORMANCE_BOOST_SETTING,
                    out int value
                );
                
                return (ProcessorBoostLevel)value;
            }
            
            /**
             * Boo-hoo it would have been avoided by design if only you used a boolean.
             * Shut it. It's open-source and I'm having fun here.
             */
            throw new ArgumentException($"Invalid power mode provided UwU: {powerMode}.");
        }

        public void WriteProcessorBoostState(PowerMode powerMode, ProcessorBoostLevel boostLevel)
        {
            PowrProf.PowerGetActiveScheme(0, out var activeSchemeGuid);
            
            if (powerMode == PowerMode.AC)
            {
                PowrProf.PowerWriteACValueIndex(
                    nint.Zero,
                    activeSchemeGuid,
                    PowrProf.GUID_PROCESSOR_SETTINGS_SUBGROUP,
                    PowrProf.GUID_PROCESSOR_PERFORMANCE_BOOST_SETTING,
                    (int)boostLevel
                );
            }
            else if (powerMode == PowerMode.DC)
            {
                PowrProf.PowerWriteDCValueIndex(
                    nint.Zero,
                    activeSchemeGuid,
                    PowrProf.GUID_PROCESSOR_SETTINGS_SUBGROUP,
                    PowrProf.GUID_PROCESSOR_PERFORMANCE_BOOST_SETTING,
                    (int)boostLevel
                );

            }
            else
            {
                throw new ArgumentException($"Invalid power mode provided UwU: {powerMode}.");
            }
        }

        public void CommitCurrentPowerSchemeChanges()
        {
            PowrProf.PowerGetActiveScheme(0, out var activeSchemeGuid);
            PowrProf.PowerSetActiveScheme(0, activeSchemeGuid);
        }
    }
}