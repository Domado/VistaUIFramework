//--------------------------------------------------------------------
// <copyright file="LinkLabel.cs" company="myapkapp">
//     Copyright (c) myapkapp. All rights reserved.
// </copyright>                                                                
//--------------------------------------------------------------------
// This open-source project is licensed under Apache License 2.0
//--------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace MyAPKapp.VistaUIFramework {

    [ToolboxBitmap(typeof(System.Windows.Forms.LinkLabel))]
    public class LinkLabel : System.Windows.Forms.LinkLabel {

        private Color tempColor;
        private bool _Aero;
        private int _GlowSize = 8;
        private int drawState;

        public LinkLabel() : base() {
            ActiveLinkColor = Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            HoverLinkColor = Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            ForeColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            LinkColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            MouseEnter += LinkLabel_MouseEnter;
            MouseLeave += LinkLabel_MouseLeave;
            MouseDown += LinkLabel_MouseDown;
            MouseUp += LinkLabel_MouseUp;
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint, true);
            BackColor = Color.Transparent;
        }

        #region Events for HoverColor property

        private void LinkLabel_MouseLeave(object sender, EventArgs e) {
            LinkColor = tempColor;
            drawState = 0;
            if (Aero) Invalidate();
        }

        private void LinkLabel_MouseEnter(object sender, EventArgs e) {
            tempColor = LinkColor;
            LinkColor = HoverLinkColor;
            drawState = 1;
            if (Aero) Invalidate();
        }

        private void LinkLabel_MouseDown(object sender, MouseEventArgs e) {
            drawState = 2;
            if (Aero) Invalidate();
        }

        private void LinkLabel_MouseUp(object sender, MouseEventArgs e) {
            drawState = 0;
            if (Aero) Invalidate();
        }

        #endregion

        #region Public properties

        [DefaultValue(typeof(Color), "Transparent")]
        public override Color BackColor {
            get {
                return base.BackColor;
            }
            set {
                base.BackColor = value;
            }
        }

        /// <summary>
        /// Gets or sets if label should be compatible with Aero glass
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Gets or sets if label should be compatible with Aero glass")]
        public bool Aero {
            get {
                return _Aero;
            }
            set {
                if (_Aero != value) {
                    _Aero = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// The Label glow size if Aero is enabled. Set to 0 for no glow"
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(8)]
        [Description("The Label glow size if Aero is enabled. Set to 0 for no glow")]
        public int GlowSize {
            get {
                return _GlowSize;
            }
            set {
                if (_GlowSize != value) {
                    _GlowSize = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "51, 153, 255")]
        public new Color ActiveLinkColor {
            get {
                return base.ActiveLinkColor;
            }
            set {
                base.ActiveLinkColor = value;
            }
        }


        /// <summary>
        /// Set link color on cursor hover
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "51, 153, 255")]
        [Description("Set link color on cursor hover")]
        public Color HoverLinkColor { get; set; }

        [DefaultValue(typeof(Color), "0, 102, 204")]
        public new Color LinkColor {
            get {
                return base.LinkColor;
            }
            set {
                base.LinkColor = value;
            }
        }

        [DefaultValue(typeof(Color), "0, 102, 204")]
        public override Color ForeColor {
            get {
                return base.ForeColor;
            }

            set {
                base.ForeColor = value;
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

        #endregion

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            int DWMResult = NativeMethods.DwmIsCompositionEnabled(out bool DWMEnabled);
            if (NativeMethods.Succeeded(DWMResult) && DWMEnabled && Aero && Visible && !DesignMode) {
                VisualStyleRenderer Renderer = new VisualStyleRenderer(VisualStyleElement.Window.Caption.Active);
                IntPtr PrimaryHDC = e.Graphics.GetHdc();
                IntPtr MemoryHDC = NativeMethods.CreateCompatibleDC(PrimaryHDC);
                NativeMethods.BITMAPINFO Info = new NativeMethods.BITMAPINFO {
                    biSize = Marshal.SizeOf(typeof(NativeMethods.BITMAPINFO)),
                    biWidth = Size.Width,
                    biHeight = -Size.Height,
                    biPlanes = 1,
                    biBitCount = 32,
                    biCompression = 0
                };
                IntPtr PPVBits = IntPtr.Zero;
                IntPtr DIBSection = NativeMethods.CreateDIBSection(PrimaryHDC, ref Info, 0, out PPVBits, IntPtr.Zero, 0);
                NativeMethods.SelectObject(MemoryHDC, DIBSection);
                IntPtr NativeFont = Font.ToHfont();
                NativeMethods.SelectObject(MemoryHDC, NativeFont);
                NativeMethods.DTTOPTS Opts = new NativeMethods.DTTOPTS {
                    dwSize = Marshal.SizeOf(typeof(NativeMethods.DTTOPTS)),
                    dwFlags = NativeMethods.DTT.Composited | NativeMethods.DTT.TextColor
                };
                if (GlowSize > 0) {
                    Opts.dwFlags |= NativeMethods.DTT.GlowSize;
                    Opts.iGlowSize = GlowSize;
                }
                if (drawState == 0) {
                    Opts.crText = ColorTranslator.ToWin32(ForeColor);
                } else if (drawState == 1) {
                    Opts.crText = ColorTranslator.ToWin32(HoverLinkColor);
                } else if (drawState == 2) {
                    Opts.crText = ColorTranslator.ToWin32(ActiveLinkColor);
                }
                NativeMethods.RECT PaddingRect = new NativeMethods.RECT(Padding.Left, Padding.Top, Width - Padding.Right, Height - Padding.Bottom);
                int Result = NativeMethods.DrawThemeTextEx(Renderer.Handle, MemoryHDC, 0, 0, Text, -1, (int)BuildTextFormatFlags(), ref PaddingRect, ref Opts);
                if (NativeMethods.Failed(Result)) {
                    Marshal.ThrowExceptionForHR(Result);
                }
                NativeMethods.BitBlt(PrimaryHDC, 0, 0, Size.Width, Size.Height, MemoryHDC, 0, 0, NativeMethods.TernaryRasterOperations.SRCCOPY);
                NativeMethods.DeleteObject(NativeFont);
                NativeMethods.DeleteObject(DIBSection);
                NativeMethods.DeleteDC(MemoryHDC);
                e.Graphics.ReleaseHdc(PrimaryHDC);
            }
        }

        private TextFormatFlags BuildTextFormatFlags() {
            TextFormatFlags Flags = TextFormatFlags.Default;
            bool Left = TextAlign == System.Drawing.ContentAlignment.TopLeft || TextAlign == System.Drawing.ContentAlignment.MiddleLeft || TextAlign == System.Drawing.ContentAlignment.BottomLeft;
            bool Center = TextAlign == System.Drawing.ContentAlignment.TopCenter || TextAlign == System.Drawing.ContentAlignment.MiddleCenter || TextAlign == System.Drawing.ContentAlignment.BottomCenter;
            bool Right = TextAlign == System.Drawing.ContentAlignment.TopRight || TextAlign == System.Drawing.ContentAlignment.MiddleRight || TextAlign == System.Drawing.ContentAlignment.BottomRight;
            bool Top = TextAlign == System.Drawing.ContentAlignment.TopLeft || TextAlign == System.Drawing.ContentAlignment.TopCenter || TextAlign == System.Drawing.ContentAlignment.TopRight;
            bool Middle = TextAlign == System.Drawing.ContentAlignment.MiddleLeft || TextAlign == System.Drawing.ContentAlignment.MiddleCenter || TextAlign == System.Drawing.ContentAlignment.MiddleRight;
            bool Bottom = TextAlign == System.Drawing.ContentAlignment.BottomLeft || TextAlign == System.Drawing.ContentAlignment.BottomCenter || TextAlign == System.Drawing.ContentAlignment.BottomRight;
            if (Left) {
                Flags |= TextFormatFlags.Left;
            } else if (Center) {
                Flags |= TextFormatFlags.HorizontalCenter;
            } else if (Right) {
                Flags |= TextFormatFlags.Right;
            }
            if (Top) {
                Flags |= TextFormatFlags.Top;
            } else if (Middle) {
                Flags |= TextFormatFlags.VerticalCenter;
            } else if (Bottom) {
                Flags |= TextFormatFlags.Bottom;
            }
            if (Text.Split('\n').Length <= 1) {
                Flags |= TextFormatFlags.SingleLine;
            }
            if (!AutoSize) {
                Flags |= TextFormatFlags.WordBreak;
            }
            if (AutoEllipsis) {
                Flags |= TextFormatFlags.EndEllipsis;
            }
            if (RightToLeft == RightToLeft.Yes) {
                Flags |= TextFormatFlags.RightToLeft;
            }
            if (Padding == Padding.Empty) {
                Flags |= TextFormatFlags.NoPadding;
            }
            return Flags;
        }

        protected override void WndProc(ref Message msg) {
            if (msg.Msg == NativeMethods.WM_SETCURSOR) {
                NativeMethods.SetCursor(NativeMethods.LoadCursor(IntPtr.Zero, NativeMethods.IDC_HAND));
                msg.Result = IntPtr.Zero;
                return;
            }
            base.WndProc(ref msg);
        }

    }
}
