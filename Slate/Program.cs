using Avalonia;
using System;
using Slate.Infrastructure.Asus.Acpi;

namespace Slate
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
#if ACPITESTING
            var proxy = new AsusAcpiProxy();
            Console.WriteLine(proxy.DSTS.ReadInt32(DstsMethod.IIA0_0x00110024));
#else
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
#endif
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder
                .Configure<App>()
                .UsePlatformDetect()
                .With(new Win32PlatformOptions
                {
                    UseWindowsUIComposition = true
                })
                .LogToTrace();
    }
}