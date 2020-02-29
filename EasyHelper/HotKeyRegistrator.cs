using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EasyHelper
{
    internal class HotKeyRegistrator: Form
    {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        public void RegisterHotKeys()
        {
            foreach (var record in RecordsManager.Records)
            {
                RegisterHotKey(base.Handle, record.Id, (int)record.Modificator, (int)record.Key);
            }
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 0x0312) // hot key message
            {
                HotKey((int)m.WParam); // wparam has code assigned in register hot key
            }
            base.WndProc(ref m);
        }

        public void HotKey(int cmd)
        {
            ProcessStartInfo info = new ProcessStartInfo(RecordsManager.Records.Where(r => r.Id == cmd).FirstOrDefault().Command);
            info.Arguments = RecordsManager.Records.Where(r => r.Id == cmd).FirstOrDefault().Arguments;
            Process.Start(info);
        }
    }
}
