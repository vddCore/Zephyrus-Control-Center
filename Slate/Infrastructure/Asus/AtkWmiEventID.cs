namespace Slate.Infrastructure.Asus
{
    public enum AtkWmiEventID
    {
        KeyPressM4 = 56,
        PowerAdapterUnplugged = 87,
        PowerAdapterPlugged = 88,

        /**
         * Happens when USB-C or barrel plug
         * is connected to or disconnected
         * from the laptop.
         **/
        ExternalPowerStateChanged = 123,
        KeyPressM3 = 124,
        KeyPressFnF10 = 107,
        KeyPressFnF5 = 174,
        KeyPressFnF4 = 179,

        /**
         * Happens when a HDMI cable is slotted
         * into the built-in socket or plugged-out
         * of it.
         */
        HdmiConnectionStateChanged = 192,
        KeyPressFnF3 = 196,
        KeyPressFnF2 = 197,

        /**
         * Happens with 88 only in this particular order.
         *
         * 1. Barrel connector state change detected. (207)
         * 2. External power plugged-in. (88)
         * 3. Notify power state has changed. (123)
         */
        BarrelConnectorPluggedIn = 207
    }
}