using Avalonia;
using System;
using Glitonea.Mvvm.Messaging;
using Slate.Model.Messaging;

#if ACPITESTING
    using System.Diagnostics;
    using Slate.Infrastructure.Asus.Acpi;
#endif

namespace Slate
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
#if ACPITESTING
                var proxy = new AsusAcpiProxy();           
                Debugger.Break();
#else
            var app = BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
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
                .LogToTrace()
                .AfterSetup((_) => Message.Broadcast<MainWindowLoadedMessage>());
    }
}