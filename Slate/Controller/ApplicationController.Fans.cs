﻿using Glitonea.Mvvm.Messaging;
using Slate.Model.Messaging;
using Slate.Model.Settings.Components;

namespace Slate.Controller
{
    public partial class ApplicationController
    {
        private FansSettings FansSettings => ControlCenterSettings.Fans;

        private void SubscribeToFansSettings()
        {
            Message.Subscribe<CpuFanCurveUpdatedMessage>(this, OnCpuFanCurveUpdated);
            Message.Subscribe<GpuFanCurveUpdatedMessage>(this, OnGpuFanCurveUpdated);
            Message.Subscribe<ManualFanOverrideUpdatedMessage>(this, OnManualFanOverrideUpdated);
        }

        private void OnManualFanOverrideUpdated(ManualFanOverrideUpdatedMessage msg)
        {
            _asusHalService.SetPerformancePreset(FansSettings.PerformancePreset);
            
            if (msg.IsOverrideEnabled)
            {
                _asusHalService.SetCpuFanDutyCycle(msg.CpuDutyCycle);
                _asusHalService.SetGpuFanDutyCycle(msg.GpuDutyCycle);
            }
            else
            {
                _asusHalService.WriteCpuFanCurve(FansSettings.CpuFanCurve!);
                _asusHalService.WriteGpuFanCurve(FansSettings.GpuFanCurve!);
            }
        }

        private void OnCpuFanCurveUpdated(CpuFanCurveUpdatedMessage msg)
        {
            _asusHalService.WriteCpuFanCurve(msg.Curve);
        }

        private void OnGpuFanCurveUpdated(GpuFanCurveUpdatedMessage msg)
        {
            _asusHalService.WriteGpuFanCurve(msg.Curve);
        }
    }
}