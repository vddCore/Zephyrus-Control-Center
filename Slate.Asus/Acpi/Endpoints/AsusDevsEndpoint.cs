﻿using Slate.Asus.Acpi.FunctionSets;

namespace Slate.Asus.Acpi.Endpoints
{
    public class AsusDevsEndpoint : AsusAcpiEndpoint<DevsMethod>
    {
        public AsusDevsEndpoint(AsusAcpiProxy proxy)
            : base(proxy, WmnbFunction.DEVS)
        {
        }

        public void SimulateKeyPress(KeyPressRequest request)
        {
            _ = ReadInt32(
                DevsMethod.SimulateKeyPress,
                (byte)request
            );
        }

        public void SetPerformancePreset(PerformancePreset preset)
        {
            _ = ReadInt32(
                DevsMethod.SetPerformancePreset,
                (byte)preset
            );
        }

        public void SetCpuFanDutyCycle(byte dutyCycle)
        {
            _ = ReadInt32(
                DevsMethod.SetCpuFanSpeedDirect, 
                dutyCycle
            );
        }

        public void SetGpuFanDutyCycle(byte dutyCycle)
        {
            _ = ReadInt32(
                DevsMethod.SetGpuFanSpeedDirect,
                dutyCycle
            );
        }

        public void SetCpuFanCurve(FanCurve curve)
        {
            _ = ReadInt32(
                DevsMethod.SetCpuFanCurve,
                curve.RawData
            );
        }

        public void SetGpuFanCurve(FanCurve curve)
        {
            _ = ReadInt32(
                DevsMethod.SetGpuFanCurve,
                curve.RawData
            );
        }

        public void SetMuxSwitch(MuxSwitchMode mode)
        {
            _ = ReadInt32(
                DevsMethod.SetMuxSwitch,
                (byte)mode
            );
        }

        /**
         * Calling this will crash your system if MUX switch is set 
         * to Discrete. I mean - you *will* have to power-cycle it.
         *
         * Manually. Oh, also, you'll have to re-enable the discrete
         * GPU in Device Manager. Likewise - manually.
         * 
         * But hey, this is a low-level API. Who am I to judge.
         **/
        public void SetEcoMode(bool enable)
        {
            _ = ReadInt32(
                DevsMethod.SetEcoMode,
                enable ? (byte)1 : (byte)0
            );
        }

        public void SetDisplayOverdrive(bool enable)
        {
            _ = ReadInt32(
                DevsMethod.SetDisplayOverdrive,
                enable ? (byte)1 : (byte)0
            );
        }

        public void SetBatteryChargeTarget(byte value)
        {
            _ = ReadInt32(
                DevsMethod.SetBatteryChargeTarget,
                value
            );
        }

        /**
         * These are probably the most dangerous ACPI calls.
         *
         * I'm not sure how much you can fuck your system up
         * by providing invalid data, and I don't trust ASUS
         * to obey any hard limits after seeing some of the
         * driver disassembly.
         *
         * Once again - this is a low-level API. Provide bogus
         * data at your own risk, then suffer the consequences.
         **/
        public void SetTotalPPT(byte totalPpt)
        {
            _ = ReadInt32(
                DevsMethod.SetTotalPPT,
                totalPpt
            );
        }
        
        public void SetCpuPPT(byte cpuPpt)
        {
            _ = ReadInt32(
                DevsMethod.SetCpuPPT,
                cpuPpt
            );
        }
    }
}