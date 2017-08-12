﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework {
    [ToolboxBitmap(typeof(System.Windows.Forms.TextBox))]
    public class TextBox : System.Windows.Forms.TextBox {

        private string _Hint;

        public TextBox() : base() {}

        /// <summary>
        /// Set the TextBox's gray text when TextBox is empty
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(null)]
        [Description("Set the TextBox's gray text when TextBox is empty")]
        public string Hint {
            get {
                return _Hint;
            }
            set {
                _Hint = value;
                NativeMethods.SendMessage(Handle, NativeMethods.EM_SETCUEBANNER, IntPtr.Zero, value);
            }
        }

        [Browsable(true)]
        public override ContextMenu ContextMenu {
            get {
                return base.ContextMenu;
            }

            set {
                base.ContextMenu = value;
                if (value != null && ContextMenuStrip != null) {
                    ContextMenuStrip = null;
                }
            }
        }

        public override ContextMenuStrip ContextMenuStrip {
            get {
                return base.ContextMenuStrip;
            }

            set {
                base.ContextMenuStrip = value;
                if (value != null && ContextMenu != null) {
                    ContextMenu = null;
                }
            }
        }

    }
}
