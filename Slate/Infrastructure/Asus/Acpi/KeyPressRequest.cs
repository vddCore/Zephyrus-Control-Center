namespace Slate.Infrastructure.Asus.Acpi
{
    public enum KeyPressRequest : byte
    {
        DecreaseScreenBrightness = 0x10,
        IncreaseScreenBrightness = 0x20,
        EnterHardSleepMode = 0x35,       // S3
        TriggerM4 = 0x38,
        TriggerTouchpadToggleKey = 0x6B,            
        EnterLightSleepMode = 0x6C,      // S1
        TriggerM3 = 0x7C,
        Unk_0x9E = 0x9E,
        Unk_0x88__Q0B = 0x88,            // No-op on GA402
        Unk_0x8A__Q72 = 0x8A,
        Unk_0xA8 = 0xA8,
        Unk_0xA9 = 0xA9,
        Unk_0xAA = 0xAA,
        Unk_0xAB = 0xAB,
        TriggerFanModeCycleKey = 0xAE,
        TriggerAuraKey = 0xB3,
        IncreaseKeyboardBrightness = 0xC4,
        DecreaseKeyboardBrigthness = 0xC5,
    }
}