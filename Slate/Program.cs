using Avalonia;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Glitonea.Mvvm.Messaging;
using Slate.Infrastructure.Native;
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

            var names = Enum.GetNames(typeof(DstsMethod));

            using (var sw = new StreamWriter("acpivars.txt"))
            {
                foreach (var name in names)
                {
                    try
                    {
                        sw.WriteLine(
                            $"{name}: {proxy.DSTS.ReadInt32((DstsMethod)Enum.Parse(typeof(DstsMethod), name)):X8}"
                        );
                    }
                    catch
                    {
                        // ignore
                    }
                }
            }

            Debugger.Break();
#else
            // var hook = User32.SetWindowsHookEx(User32.WH_KEYBOARD_LL, KeyboardProc, 0, 0);
            
            var app = BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
#endif
        }
        //
        // private static nint KeyboardProc(int nCode, nint wParam, nint lParam)
        // {
        //     var str = Marshal.PtrToStructure<User32.KBDLLHOOKSTRUCT>(lParam);
        //
        //     if (wParam == User32.WM_KEYDOWN || wParam == User32.WM_SYSKEYDOWN || wParam == User32.WM_KEYUP || wParam == User32.WM_SYSKEYUP)
        //     {
        //         Debug.WriteLine($"{nCode:X8} : {str.vkCode}");
        //     }
        //
        //     return User32.CallNextHookEx(0, nCode, wParam, lParam);
        // }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder
                .Configure<App>()
                .UsePlatformDetect()
                .With(new Win32PlatformOptions
                {
                    CompositionMode = new[] { Win32CompositionMode.WinUIComposition }
                })
                .LogToTrace()
                .AfterSetup((_) => Message.Broadcast<MainWindowLoadedMessage>());
    }
}