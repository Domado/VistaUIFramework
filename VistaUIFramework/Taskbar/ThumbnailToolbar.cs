using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework.Taskbar {
    /// <summary>
    /// <code>ThumbnailToolbar</code> is a nativewindow, the purpose is return a WndProc'd Handle
    /// </summary>
    internal class ThumbnailToolbar : NativeWindow {

        private ThumbnailButton[] buttons;

        public ThumbnailToolbar(IntPtr Handle, ThumbnailButton[] buttons) {
            this.buttons = buttons;
            AssignHandle(Handle);
        }

        protected override void WndProc(ref Message m) {
            if (m.Msg == NativeMethods.WM_COMMAND && NativeMethods.GetHiWord(m.WParam.ToInt64(), 16) == NativeMethods.THUMBBUTTON.Clicked) {
                int buttonId = NativeMethods.GetLoWord(m.WParam.ToInt64());
                foreach (ThumbnailButton button in buttons) {
                    if (button.Id == buttonId) {
                        button.FireClickEvent();
                    }
                }
            }
            base.WndProc(ref m);
        }

    }
}
