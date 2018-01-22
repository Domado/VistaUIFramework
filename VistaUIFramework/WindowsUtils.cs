using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace MyAPKapp.VistaUIFramework {

    /// <summary>
    /// The Windows Utilities contains multiple common features like move the window to top, flicker the window and taskbar, shutdown computer, etc.
    /// </summary>
    public class WindowsUtils {

        /// <summary>
        /// Make the window flash and flicker to get user's attention
        /// </summary>
        /// <param name="Window">The form to be affected by flash</param>
        /// <returns>Window state (active or inactive)</returns>
        public static bool FlashWindow(System.Windows.Forms.Form Window) {
            return NativeMethods.FlashWindow(Window.Handle, true);
        }

        /// <summary>
        /// Make the window flash and flicker to get user's attention
        /// </summary>
        /// <param name="Window"></param>
        /// <param name="Count"></param>
        /// <param name="Timeout"></param>
        /// <returns>Window state (active or inactive)</returns>
        public static bool FlashWindow(System.Windows.Forms.Form Window, int Count, int Timeout) {
            NativeMethods.FLASHWINFO info = new NativeMethods.FLASHWINFO {
                cbSize = Marshal.SizeOf(typeof(NativeMethods.FLASHWINFO)),
                hwnd = Window.Handle,
                uCount = Count,
                dwTimeout = Timeout
            };
            return NativeMethods.FlashWindowEx(ref info);
        }

        /// <summary>
        /// Make the window flash and flicker to get user's attention
        /// </summary>
        /// <param name="Window"></param>
        /// <param name="Count"></param>
        /// <param name="Timeout"></param>
        /// <param name="Flags"></param>
        /// <returns>Window state (active or inactive)</returns>
        public static bool FlashWindow(System.Windows.Forms.Form Window, int Count, int Timeout, FlashFlags Flags) {
            NativeMethods.FLASHWINFO info = new NativeMethods.FLASHWINFO {
                cbSize = Marshal.SizeOf(typeof(NativeMethods.FLASHWINFO)),
                hwnd = Window.Handle,
                dwFlags = (int)Flags,
                uCount = Count,
                dwTimeout = Timeout
            };
            return NativeMethods.FlashWindowEx(ref info);
        }

        /// <summary>
        /// Force the window to show in front of everything
        /// </summary>
        /// <param name="Window"></param>
        /// <returns>If window was brought to the top</returns>
        public static bool MakeWindowTop(System.Windows.Forms.Form Window) {
            bool Result = NativeMethods.SetForegroundWindow(Window.Handle);
            Window.Focus();
            NativeMethods.SetActiveWindow(Window.Handle);
            return Result;
        }

        /// <summary>
        /// Lock the computer (with password in case of password-protected users) and current user has to unlock the computer (using password if necessary).
        /// </summary>
        /// <returns></returns>
        public static bool LockComputer() {
            return NativeMethods.LockWorkStation();
        }

        /// <summary>
        /// Get the current active window in a native way (using Handles).
        /// </summary>
        /// <returns>Native Handle (HWND)</returns>
        public static IntPtr GetActiveWindow() {
            return NativeMethods.GetActiveWindow();
        }

        /// <summary>
        /// Retrieve a built-in icon
        /// </summary>
        /// <param name="StockIcon">Icon to be extracted</param>
        /// <param name="Large">32z32 or 16x16</param>
        /// <returns>The icon according to the arguments</returns>
        public static Icon GetStockIcon(StockIcon StockIcon, bool Large) {
            NativeMethods.SHSTOCKICONINFO info = new NativeMethods.SHSTOCKICONINFO();
            info.cbSize = Marshal.SizeOf(info);
            NativeMethods.SHGSI Flags = NativeMethods.SHGSI.SHGSI_ICON;
            if (!Large) Flags |= NativeMethods.SHGSI.SHGSI_SMALLICON;
            int Result = NativeMethods.SHGetStockIconInfo(StockIcon, Flags, ref info);
            if (!NativeMethods.Succeeded(Result)) {
                Marshal.ThrowExceptionForHR(Result);
            }
            return Icon.FromHandle(info.hIcon);
        }

        /// <summary>
        /// The list of Flags used for flashing the window
        /// </summary>
        [Flags]
        public enum FlashFlags {
            Stop = 0,
            Caption = 1,
            Tray = 2,
            All = 1 | 2,
            Timer = 4,
            TimerUntilForeground = 5
        }

    }
}