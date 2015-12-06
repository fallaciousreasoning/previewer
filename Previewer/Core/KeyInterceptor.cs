using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Previewer
{
    public static class KeyInterceptor
    {
        public static void Wait(Key key, Action<KeyStates> a)
        {
            var ao = AsyncOperationManager.CreateOperation(null);
            SendOrPostCallback cb = state => a((KeyStates)state);
            ThreadPool.QueueUserWorkItem(state =>
            {
                var vk = KeyInterop.VirtualKeyFromKey(key);  // WindowsBase.dll, v4.0.30319
                var start = ((GetAsyncKeyState(vk) & 0x8000) == 0x8000);
                var prev = start;
                while (true)
                {
                    var res = ((GetAsyncKeyState(vk) & 0x8000) == 0x8000);
                    if (res != prev && res != start)
                    {
                        ao.Post(cb, res ? KeyStates.Down : KeyStates.None);
                    }
                    prev = res;
                }
            });
        }

        public static Task WaitForKey(Key key)
        {
            return Task.Run(() =>
            {
                var vk = KeyInterop.VirtualKeyFromKey(key);  // WindowsBase.dll, v4.0.30319
                var prev = ((GetAsyncKeyState(vk) & 0x8000) == 0x8000);

                while (true)
                {
                    var res = ((GetAsyncKeyState(vk) & 0x8000) == 0x8000);
                    if (res != prev) return;
                }
            });
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        static extern short GetAsyncKeyState(int vkey);
    }
}