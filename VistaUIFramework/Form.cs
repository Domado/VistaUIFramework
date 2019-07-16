//--------------------------------------------------------------------
// <copyright file="Form.cs" company="MyAPKapp">
//     Copyright (c) MyAPKapp. All rights reserved.
// </copyright>                                                                
//--------------------------------------------------------------------
// This open-source project is licensed under Apache License 2.0
//--------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework {

    public class Form : System.Windows.Forms.Form {

        private bool _CloseBox = true;
        private bool _Aero;
        private Padding _AeroMargin;
        private bool _AeroDrag;
        private bool _AeroKey;
        private bool _AeroBlur;

        public Form() : base() {
            base.DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            AutoScaleMode = AutoScaleMode.Dpi;
        }

        #region Form events

        /// <summary>
        /// Fires when DWM composition changes (e.g.: Aero is enabled/disabled)
        /// </summary>
        /// <remarks>
        /// As of Windows 8, DWM composition is always enabled, so this event is not fired regardless of video mode changes.
        /// </remarks>
        [PropertyChangedCategory]
        [Description("Fires when DWM composition changes")]
        public event AeroCompChangedEventHandler AeroCompChanged;

        /// <summary>
        /// Fires when Aero glass color changes
        /// </summary>
        /// <remarks>
        /// As of Windows 8, DWM composition is always enabled, so this event is not fired regardless of video mode changes.
        /// </remarks>
        [PropertyChangedCategory]
        [Description("Fires when Aero glass color changes")]
        public event AeroColorChangedEventHandler AeroColorChanged;

        #endregion

        #region Overrided methods

        protected override void OnActivated(EventArgs e) {
            base.OnActivated(e);
            Invalidate();
        }

        protected override void OnDeactivate(EventArgs e) {
            base.OnDeactivate(e);
            Invalidate();
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnAeroCompChanged(AeroCompChangedEventArgs e) {
            AeroCompChanged?.Invoke(this, e);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnAeroColorChanged(AeroColorChangedEventArgs e) {
            AeroColorChanged?.Invoke(this, e);
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
                if (Environment.OSVersion.Version.Major < 6) {
                    throw new UnsupportedWindowsException("Windows Vista");
                }
                return _Aero;
            }
            set {
                if (Environment.OSVersion.Version.Major < 6) {
                    throw new UnsupportedWindowsException("Windows Vista");
                }
                if (_Aero != value) {
                    _Aero = value;
                    if (!value && AeroDrag) AeroDrag = false;
                    if (!value && AeroKey) AeroKey = false;
                    if (!value && AeroBlur) AeroBlur = false;
                    if (value && DoubleBuffered) {
                        DoubleBuffered = false;
                    }
                    RecreateHandle();
                }
            }
        }

        /// <summary>
        /// The margins between form container and Aero glass
        /// </summary>
        /// <remarks>
        /// If all margins are set to -1, the glass will cover the whole form
        /// </remarks>
        [Category("Design")]
        [DefaultValue(0)]
        [Description("The margins between form container and Aero glass")]
        public Padding AeroMargin {
            get {
                return _AeroMargin;
            }
            set {
                if (_AeroMargin != value) {
                    _AeroMargin = value;
                    Invalidate();
                    RecreateHandle();
                }
            }
        }

        /// <summary>
        /// Gets or sets if mouse is able to move the window by dragging the Aero glass
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Gets or sets if mouse is able to move the window by dragging the Aero glass")]
        public bool AeroDrag {
            get {
                if (Environment.OSVersion.Version.Major < 6) {
                    throw new UnsupportedWindowsException("Windows Vista");
                }
                return _AeroDrag;
            }
            set {
                if (Environment.OSVersion.Version.Major < 6) {
                    throw new UnsupportedWindowsException("Windows Vista");
                }
                if (_AeroDrag != value) {
                    _AeroDrag = value;
                    if (value && !Aero) Aero = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets if Aero glass is <code>TransparencyKey</code> compliant
        /// </summary>
        /// <remarks>
        /// It's not recommended to use same-value RGB (eg. 255,255,255) or any shade of green, otherwise, you'd interacting with other windows behind your windows (possibly because window behaves like transparent). Modifying the transparency key can cause some issues with alpha channel (neither 0% nor 100%).
        /// </remarks>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Gets or sets if Aero glass is TransparencyKey compliant")]
        public bool AeroKey {
            get {
                if (Environment.OSVersion.Version.Major < 6) {
                    throw new UnsupportedWindowsException("Windows Vista");
                }
                return _AeroKey;
            }
            set {
                if (Environment.OSVersion.Version.Major < 6) {
                    throw new UnsupportedWindowsException("Windows Vista");
                }
                if (_AeroKey != value) {
                    _AeroKey = value;
                    if (value && !Aero) Aero = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets if background color is Aero blur (different from Aero glass)
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("Gets or sets if background color is Aero blur (different from Aero glass)")]
        public bool AeroBlur {
            get {
                if (Environment.OSVersion.Version.Major < 6) {
                    throw new UnsupportedWindowsException("Windows Vista");
                }
                return _AeroBlur;
            }
            set {
                if (Environment.OSVersion.Version.Major < 6) {
                    throw new UnsupportedWindowsException("Windows Vista");
                }
                if (_AeroBlur != value) {
                    _AeroBlur = value;
                    if (value && !Aero) Aero = true;
                }
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
        [EditorBrowsable(EditorBrowsableState.Always)]
        public new event EventHandler ContextMenuChanged { add => base.ContextMenuChanged += value; remove => base.ContextMenuChanged -= value; }

        [Browsable(true)]
        public new MainMenu Menu {
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

        public new MenuStrip MainMenuStrip {
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

        /// <summary>
        /// Gets or sets if Aero glass DWM Composition is enabled
        /// </summary>
        [Browsable(false)]
        public bool CompositionEnabled {
            get {
                if (Environment.OSVersion.Version.Major < 6) {
                    throw new UnsupportedWindowsException("Windows Vista");
                }
                int result = NativeMethods.DwmIsCompositionEnabled(out bool enabled);
                return NativeMethods.Succeeded(result) && enabled;
            }
            set {
                if (Environment.OSVersion.Version.Major < 6) {
                    throw new UnsupportedWindowsException("Windows Vista");
                }
                int result = NativeMethods.DwmEnableComposition(NativeMethods.BoolToNative(value));
                if (NativeMethods.Failed(result)) {
                    Marshal.ThrowExceptionForHR(result);
                }
            }
        }

        [Browsable(false)]
        public GlassColor GlassColor {
            get {
                if (Environment.OSVersion.Version.Major < 6) {
                    throw new UnsupportedWindowsException("Windows Vista");
                }
                int result = NativeMethods.DwmGetColorizationColor(out int ColorizationColor, out bool ColorizationOpaqueBlend);
                if (NativeMethods.Failed(result)) {
                    Marshal.ThrowExceptionForHR(result);
                }
                return new GlassColor(Color.FromArgb(ColorizationColor), ColorizationOpaqueBlend);
            }
        }

        #endregion

        #region Private methods

        private void EnableCloseButton(bool enable) {
            IntPtr hMenu = NativeMethods.GetSystemMenu(Handle, false);
            if (hMenu != IntPtr.Zero) {
                NativeMethods.EnableMenuItem(hMenu, NativeMethods.SC_CLOSE, NativeMethods.MF_BYCOMMAND | (enable ? NativeMethods.MF_ENABLED : (NativeMethods.MF_DISABLED | NativeMethods.MF_GRAYED)));
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e) {
            base.OnPaintBackground(e);
            if (Aero) {
                if (CompositionEnabled) {
                    if (DesignMode) {
                        e.Graphics.Clear(ActiveCaption);
                    } else {
                        if (AeroKey) {
                            e.Graphics.Clear(TransparencyKey);
                        } else {
                            e.Graphics.Clear(Color.Black);
                        }
                    }
                    if (!IsAeroSheet) {
                        Rectangle clientArea = new Rectangle(
                                _AeroMargin.Left,
                                _AeroMargin.Top,
                                ClientRectangle.Width - _AeroMargin.Left - _AeroMargin.Right,
                                ClientRectangle.Height - _AeroMargin.Top - _AeroMargin.Bottom
                            );
                        if (AeroBlur) {
                            if (!DesignMode) {
                                GraphicsPath path = new GraphicsPath();
                                path.AddRectangle(clientArea);
                                Region Reg = new Region(path);
                                IntPtr hRgn = Reg.GetHrgn(Graphics.FromHwnd(Handle));
                                NativeMethods.DWM_BLURBEHIND blur = new NativeMethods.DWM_BLURBEHIND {
                                    dwFlags = NativeMethods.DWM_BB.Enable | NativeMethods.DWM_BB.BlurRegion | NativeMethods.DWM_BB.TransitionMaximized,
                                    fEnable = true,
                                    hRgnBlur = hRgn
                                };
                                NativeMethods.DwmEnableBlurBehindWindow(Handle, ref blur);
                            }
                        } else {
                            Brush b = new SolidBrush(BackColor);
                            e.Graphics.FillRectangle(b, clientArea);
                        }
                    }
                } else {
                    if (DesignMode) {
                        e.Graphics.Clear(ActiveCaption);
                    } else {
                        if (ActiveForm == this) {
                            e.Graphics.Clear(ActiveCaption);
                        } else {
                            e.Graphics.Clear(InactiveCaption);
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
            int result = NativeMethods.DwmIsCompositionEnabled(out bool isDWMEnabled);
            if (Aero && NativeMethods.Succeeded(result) && isDWMEnabled) {
                NativeMethods.MARGINS margins = new NativeMethods.MARGINS {
                    topHeight = _AeroMargin.Top,
                    bottomHeight = _AeroMargin.Bottom,
                    leftWidth = _AeroMargin.Left,
                    rightWidth = _AeroMargin.Right
                };
                NativeMethods.DwmExtendFrameIntoClientArea(Handle, ref margins);
            }
        }

        protected override void WndProc(ref Message m) {
            base.WndProc(ref m);
            if (_Aero && _AeroDrag && m.Msg == NativeMethods.WM_NCHITTEST && m.Result.ToInt32() == NativeMethods.HTCLIENT) {
                if (IsAeroSheet) {
                    m.Result = new IntPtr(NativeMethods.HTCAPTION);
                } else {
                    int x = NativeMethods.GetLoWord(m.LParam.ToInt64());
                    int y = NativeMethods.GetHiWord(m.LParam.ToInt64(), 16);
                    Point ClientPoint = PointToClient(new Point(x, y));
                    if (IsOutside(_AeroMargin, ClientPoint, ClientSize)) {
                        m.Result = new IntPtr(NativeMethods.HTCAPTION);
                    }
                }
            } else if (m.Msg == NativeMethods.WM_DWMCOMPOSITIONCHANGED || m.Msg == NativeMethods.WM_DWMNCRENDERINGCHANGED) {
                OnAeroCompChanged(new AeroCompChangedEventArgs(CompositionEnabled));
            } else if (m.Msg == NativeMethods.WM_DWMCOLORIZATIONCOLORCHANGED) {
                int NativeColor = (IntPtr.Size == 8) ? (int) m.WParam.ToInt64() : m.WParam.ToInt32();
                bool Blend = NativeMethods.NativeToBool((IntPtr.Size == 8) ? (int) m.LParam.ToInt64() : m.LParam.ToInt32());
                OnAeroColorChanged(new AeroColorChangedEventArgs(Color.FromArgb(NativeColor), Blend));
            }
        }

        private Color ActiveCaption {
            get {
                return Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            }
        }

        private Color InactiveCaption {
            get {
                return Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            }
        }

        private bool IsOutside(Padding p, Point point, Size size) {
            return (point.X < p.Left ||
                    point.X > (size.Width - p.Right) ||
                    point.Y < p.Top ||
                    point.Y > (size.Height - p.Bottom));
        }

        private bool IsAeroSheet {
            get {
                return _AeroMargin.Left == -1 && _AeroMargin.Right == -1 && _AeroMargin.Top == -1 && _AeroMargin.Bottom == -1;
            }
        }

        #endregion

    }
}
