using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework {
    public class Form : System.Windows.Forms.Form {
        private bool _CloseBox = true;
        private bool _Aero;
        private Padding _AeroMargin;
        private NativeMethods.MARGINS margins;

        public Form() : base() {
            base.DoubleBuffered = true;
            base.BackColor = SystemColors.Window;
        }

        #region Form properties

        /// <summary>
        /// Set if close button is enabled
        /// </summary>
        [Category("WindowStyle")]
        [DefaultValue(true)]
        [Description("Set if close button is enabled")]
        public bool CloseBox {
            get {
                return _CloseBox;
            }
            set {
                _CloseBox = value;
                EnableCloseButton(value);
            }
        }

        /// <summary>
        /// Set the aero glass to the form
        /// </summary>
        [Category("WindowStyle")]
        [DefaultValue(false)]
        [Description("Set the aero glass to the form")]
        public bool Aero {
            get {
                return _Aero;
            }
            set {
                if (_Aero != value) {
                    _Aero = value;
                    if (value && DoubleBuffered) {
                        DoubleBuffered = false;
                    }
                    RecreateHandle();
                }
            }
        }

        [Category("Design")]
        [DefaultValue(0)]
        [Description("The margins between form container and Aero glass")]
        public Padding AeroMargin {
            get {
                return _AeroMargin;
            }
            set {
                _AeroMargin = value;
                RecreateHandle();
            }
        }

        [DefaultValue(true)]
        protected override bool DoubleBuffered {
            get {
                return base.DoubleBuffered;
            }
            set {
                base.DoubleBuffered = value;
                if (value && Aero) {
                    Aero = false;
                }
            }
        }

        protected override CreateParams CreateParams {
            get {
                if (!CloseBox) {
                    CreateParams cp = base.CreateParams;
                    cp.ClassStyle |= NativeMethods.CS_NOCLOSE;
                    return cp;
                }
                return base.CreateParams;
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

        [Browsable(true)]
        public new virtual MainMenu Menu {
            get {
                return base.Menu;
            }
            set {
                base.Menu = value;
                if (value != null && MainMenuStrip != null) {
                    MainMenuStrip = null;
                }
            }
        }

        public new virtual MenuStrip MainMenuStrip {
            get {
                return base.MainMenuStrip;
            }
            set {
                base.MainMenuStrip = value;
                if (value != null && Menu != null) {
                    Menu = null;
                }
            }
        }

        [DefaultValue(typeof(Color), "Window")]
        public override Color BackColor {
            get {
                return base.BackColor;
            }

            set {
                base.BackColor = value;
            }
        }

        #endregion

        private void EnableCloseButton(bool enable) {
            IntPtr hMenu = NativeMethods.GetSystemMenu(Handle, false);
            if (hMenu != IntPtr.Zero) {
                NativeMethods.EnableMenuItem(hMenu, NativeMethods.SC_CLOSE, NativeMethods.MF_BYCOMMAND | (enable ? NativeMethods.MF_ENABLED : NativeMethods.MF_GRAYED));
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e) {
            if (Aero) {
                base.OnPaint(e);
                if (NativeMethods.DwmIsCompositionEnabled()) {
                    e.Graphics.Clear(Color.Black);
                    Rectangle clientArea = new Rectangle(
                            margins.leftWidth,
                            margins.topHeight,
                            this.ClientRectangle.Width - margins.leftWidth - margins.rightWidth,
                            this.ClientRectangle.Height - margins.topHeight - margins.bottomHeight
                        );
                    Brush b = new SolidBrush(this.BackColor);
                    e.Graphics.FillRectangle(b, clientArea);
                }
            } else {
                base.OnPaintBackground(e);
            }
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            if (Aero && NativeMethods.DwmIsCompositionEnabled()) {
                margins = new NativeMethods.MARGINS();
                margins.topHeight = _AeroMargin.Top;
                margins.bottomHeight = _AeroMargin.Bottom;
                margins.leftWidth = _AeroMargin.Left;
                margins.rightWidth = _AeroMargin.Right;
                NativeMethods.DwmExtendFrameIntoClientArea(Handle, ref margins);
            }
        }

    }
}
