using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework {
    [ToolboxBitmap(typeof(System.Windows.Forms.TextBox))]
    public class TextBox : System.Windows.Forms.TextBox {

        private string _Hint;
        private bool _HideHintOnFocus;

        public TextBox() : base() {
            _HideHintOnFocus = true;
        }

        /// <summary>
        /// Set the TextBox's gray text when TextBox is empty
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(null)]
        [Localizable(true)]
        [Description("Set the TextBox's gray text when TextBox is empty")]
        public string Hint {
            get {
                return _Hint;
            }
            set {
                _Hint = value;
                NativeMethods.SendMessage(Handle, NativeMethods.EM_SETCUEBANNER, NativeMethods.BoolToNative(!_HideHintOnFocus), value);
            }
        }

        /// <summary>
        /// Set if TextBox's hint is hidden on focus, even if TextBox was empty
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("Set if TextBox's hint is hidden on focus, even if TextBox was empty")]
        public bool HideHintOnFocus {
            get {
                return _HideHintOnFocus;
            }
            set {
                _HideHintOnFocus = value;
                NativeMethods.SendMessage(Handle, NativeMethods.EM_SETCUEBANNER, NativeMethods.BoolToNative(!value), _Hint);
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

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            if (_Hint != null) Hint = _Hint;
        }

    }
}
