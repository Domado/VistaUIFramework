using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace MyAPKapp.VistaUIFramework {
    public class Form : System.Windows.Forms.Form {
        private bool _CloseBox = true;
        private bool _Aero;
        private Padding _AeroMargin;

        public Form() : base() {
            base.DoubleBuffered = true;
            base.BackColor = SystemColors.Window;
            if (!DesignMode) {
                Activated += Form_Activated;
                Deactivate += Form_Deactivate;
            }
            this.SetStyle(ControlStyles.AllPaintingInWmPaint
              | ControlStyles.OptimizedDoubleBuffer
              | ControlStyles.ResizeRedraw
              | ControlStyles.UserPaint
              , true);
        }

        #region Inner events

        private void Form_Activated(object sender, EventArgs e) {
            Invalidate();
        }

        private void Form_Deactivate(object sender, EventArgs e) {
            Invalidate();
        }

        #endregion

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
                Invalidate();
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
            base.OnPaintBackground(e);
            if (Aero) {
                bool isDWMEnabled;
                int result = NativeMethods.DwmIsCompositionEnabled(out isDWMEnabled);
                if (NativeMethods.Succeeded(result) && isDWMEnabled) {
                    if (DesignMode) {
                        e.Graphics.Clear(SystemColors.GradientActiveCaption);
                    } else {
                        e.Graphics.Clear(Color.Transparent);
                    }
                    if (!IsAeroSheet) {
                        Rectangle clientArea = new Rectangle(
                            _AeroMargin.Left,
                            _AeroMargin.Top,
                            this.ClientRectangle.Width - _AeroMargin.Left - _AeroMargin.Right,
                            this.ClientRectangle.Height - _AeroMargin.Top - _AeroMargin.Bottom
                        );
                        Brush b = new SolidBrush(this.BackColor);
                        e.Graphics.FillRectangle(b, clientArea);
                    }
                } else {
                    if (DesignMode) {
                        e.Graphics.Clear(SystemColors.GradientActiveCaption);
                    } else {
                        if (ActiveForm == this) {
                            e.Graphics.Clear(SystemColors.GradientActiveCaption);
                        } else {
                            e.Graphics.Clear(SystemColors.GradientInactiveCaption);
                        }
                    }
                    if (!IsAeroSheet) {
                        Rectangle clientArea = new Rectangle(
                            _AeroMargin.Left,
                            _AeroMargin.Top,
                            this.ClientRectangle.Width - _AeroMargin.Left - _AeroMargin.Right,
                            this.ClientRectangle.Height - _AeroMargin.Top - _AeroMargin.Bottom
                        );
                        Brush b = new SolidBrush(this.BackColor);
                        e.Graphics.FillRectangle(b, clientArea);
                    }
                }
            }
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            bool isDWMEnabled;
            int result = NativeMethods.DwmIsCompositionEnabled(out isDWMEnabled);
            if (Aero && NativeMethods.Succeeded(result) && isDWMEnabled) {
                NativeMethods.MARGINS margins = new NativeMethods.MARGINS();
                margins.topHeight = _AeroMargin.Top;
                margins.bottomHeight = _AeroMargin.Bottom;
                margins.leftWidth = _AeroMargin.Left;
                margins.rightWidth = _AeroMargin.Right;
                NativeMethods.DwmExtendFrameIntoClientArea(Handle, ref margins);
            }
        }

        private bool IsAeroSheet {
            get {
                return _AeroMargin.Left == -1 && _AeroMargin.Right == -1 && _AeroMargin.Top == -1 && _AeroMargin.Bottom == -1;
            }
        }

    }
}
