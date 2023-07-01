using System.Runtime.InteropServices;
using Slate.Infrastructure.Native;
using Slate.Model;

namespace Slate.Infrastructure.Services.Implementations
{
    public class InputInjectionService : IInputInjectionService
    {
        public void SimulateMediaKeyPress(MediaKey mediaKey)
        {
            switch (mediaKey)
            {
                case MediaKey.Previous:
                    SendInput(User32.VK_MEDIA_PREV_TRACK);
                    break;

                case MediaKey.PlayPause:
                    SendInput(User32.VK_MEDIA_PLAY_PAUSE);
                    break;

                case MediaKey.Stop:
                    SendInput(User32.VK_MEDIA_STOP);
                    break;

                case MediaKey.Next:
                    SendInput(User32.VK_MEDIA_NEXT_TRACK);
                    break;
            }
        }

        private void SendInput(ushort vkCode)
        {
            var inputDown = new User32.INPUT
            {
                type = User32.INPUT_TYPE.KEYBOARD,
                union = new User32.INPUTUNION
                {
                    ki = new User32.KEYBDINPUT
                    {
                        wVk = vkCode,
                        dwFlags = 0
                    }
                }
            };

            var inputUp = new User32.INPUT
            {
                type = User32.INPUT_TYPE.KEYBOARD,
                union = new User32.INPUTUNION
                {
                    ki = new User32.KEYBDINPUT
                    {
                        wVk = vkCode,
                        dwFlags = User32.KEYEVENTF.KEYUP
                    }
                }
            };

            User32.SendInput(2, new[] { inputDown, inputUp }, Marshal.SizeOf<User32.INPUT>());
        }
    }
}