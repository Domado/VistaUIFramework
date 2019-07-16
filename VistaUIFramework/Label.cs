//--------------------------------------------------------------------
// <copyright file="Label.cs" company="MyAPKapp">
//     Copyright (c) MyAPKapp. All rights reserved.
// </copyright>                                                                
//--------------------------------------------------------------------
// This open-source project is licensed under Apache License 2.0
//--------------------------------------------------------------------

using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;

namespace MyAPKapp.VistaUIFramework {

    [ToolboxBitmap(typeof(System.Windows.Forms.Label))]
    public class Label : System.Windows.Forms.Label {

        private LabelStyle _LabelStyle = LabelStyle.Default;
        private Font defaultFont;
        private Color defaultColor;
        private bool _Aero;
        private int _GlowSize = 8;

        [DllImport("dwmapi.dll", EntryPoint = "#127", PreserveSig = false)]
        private static extern void DwmGetColorizationParameters(out DWM_COLORIZATION_PARAMS parameters);

        private struct DWM_COLORIZATION_PARAMS {
            public int clrColor;
            public uint clrAfterGlow;
            public uint nIntensity;
            public uint clrAfterGlowBalance;
            public uint clrBlurBalance;
            public uint clrGlassReflectionIntensity;
            public bool fOpaque;
        }

        public Label() : base() {

            //Make the Control act the native way
            base.FlatStyle = FlatStyle.System;

            //Add temporary events for design time
            FontChanged += LabelStyle_FontChanged;
            ForeColorChanged += LabelStyle_ForeColorChanged;

            //Set the required styles to make the Control work the native way
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint, true);
            BackColor = Color.Transparent;
        }

        /// <summary>
        /// The Vista label styles
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(LabelStyle.Default)]
        [Description("The Vista label styles")]
        public LabelStyle LabelStyle {
            get {
                return _LabelStyle;
            }
            set {
                _LabelStyle = value;
                changeLabelStyle(value);
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

        // ContextMenu and ContextMenuStrip overrides were made to avoid any conflict between both properties

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

        /// <summary>
        /// Update the basic <see cref="Label"/> style
        /// </summary>
        /// <param name="labelStyle">The chosen style</param>
        private void changeLabelStyle(LabelStyle labelStyle) {
            FontChanged -= LabelStyle_FontChanged;
            ForeColorChanged -= LabelStyle_ForeColorChanged;
            if (labelStyle == LabelStyle.Default) {
                Font = defaultFont;
                ForeColor = defaultColor;
            } else if (labelStyle == LabelStyle.MainInstructions) {
                defaultFont = base.Font;
                defaultColor = base.ForeColor;
                Font = new Font("Segoe UI", 12F, FontStyle.Regular);
                ForeColor = Color.FromArgb(0, 51, 153);
            } else if (LabelStyle == LabelStyle.GroupHeader) {
                defaultFont = base.Font;
                defaultColor = base.ForeColor;
                Font = new Font("Segoe UI", 11F, FontStyle.Regular);
                ForeColor = Color.FromArgb(0, 51, 153);
            }
            FontChanged += LabelStyle_FontChanged;
            ForeColorChanged += LabelStyle_ForeColorChanged;
        }

        private void LabelStyle_FontChanged(object sender, EventArgs e) {
            defaultFont = base.Font;
            defaultColor = base.ForeColor;
            LabelStyle = LabelStyle.Default;
        }

        private void LabelStyle_ForeColorChanged(object sender, EventArgs e) {
            defaultFont = base.Font;
            defaultColor = base.ForeColor;
            LabelStyle = LabelStyle.Default;
        }

        [DefaultValue(FlatStyle.System)]
        public new FlatStyle FlatStyle {
            get { return base.FlatStyle; }
            set { base.FlatStyle = value; }
        }

        [DefaultValue(typeof(Color), "Transparent")]
        public override Color BackColor {
            get {
                return base.BackColor;
            }
            set {
                base.BackColor = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            int DWMResult = NativeMethods.DwmIsCompositionEnabled(out bool DWMEnabled);

            //Check if the computer and the control meets the requirements for a Vista-style glowed text
            if (NativeMethods.Succeeded(DWMResult) && DWMEnabled && Aero && Visible && !DesignMode) {

                //The procedure is based on on the title bar because there's where Aero is displayed
                VisualStyleRenderer Renderer = new VisualStyleRenderer(VisualStyleElement.Window.Caption.Active);
                IntPtr PrimaryHDC = e.Graphics.GetHdc();
                IntPtr MemoryHDC = NativeMethods.CreateCompatibleDC(PrimaryHDC);

                //Declaring the properties of the bitmap
                NativeMethods.BITMAPINFO Info = new NativeMethods.BITMAPINFO {
                    biSize = Marshal.SizeOf(typeof(NativeMethods.BITMAPINFO)), //Structure size
                    biWidth = Size.Width,     //DIB width based on 
                    biHeight = -Size.Height,  //DIB use top-down ref system, thus we set negative height
                    biPlanes = 1,             //Always 1
                    biBitCount = 32,          //32-bit depth bitmap
                    biCompression = 0         //Avoid compression for labels
                };
                IntPtr DIBSection = NativeMethods.CreateDIBSection(PrimaryHDC, ref Info, 0, out IntPtr PPVBits, IntPtr.Zero, 0);
                NativeMethods.SelectObject(MemoryHDC, DIBSection);
                IntPtr NativeFont = Font.ToHfont();
                NativeMethods.SelectObject(MemoryHDC, NativeFont);

                //Set the options for the text bitmap
                NativeMethods.DTTOPTS Opts = new NativeMethods.DTTOPTS {
                    dwSize = Marshal.SizeOf(typeof(NativeMethods.DTTOPTS)),
                    dwFlags = NativeMethods.DTT.Composited | NativeMethods.DTT.TextColor
                };
                if (GlowSize > 0) {
                    Opts.dwFlags |= NativeMethods.DTT.GlowSize;
                    Opts.iGlowSize = GlowSize;
                }
                Opts.crText = ColorTranslator.ToWin32(ForeColor);

                //Constructing the padding
                NativeMethods.RECT PaddingRect = new NativeMethods.RECT(Padding.Left, Padding.Top, Width - Padding.Right, Height - Padding.Bottom);
                int Result = NativeMethods.DrawThemeTextEx(Renderer.Handle, MemoryHDC, 0, 0, Text, -1, (int) BuildTextFormatFlags(), ref PaddingRect, ref Opts);

                //Throw an exception to the runtime debugger if the procedure somehow fails
                if (NativeMethods.Failed(Result)) {
                    Marshal.ThrowExceptionForHR(Result);
                }

                //Blocks the bitmap and delete the objects to free-up memory
                NativeMethods.BitBlt(PrimaryHDC, 0, 0, Size.Width, Size.Height, MemoryHDC, 0, 0, NativeMethods.TernaryRasterOperations.SRCCOPY);
                NativeMethods.DeleteObject(NativeFont);
                NativeMethods.DeleteObject(DIBSection);
                NativeMethods.DeleteDC(MemoryHDC);
                e.Graphics.ReleaseHdc(PrimaryHDC);
            }
        }

        /// <summary>
        /// Build the <see cref="TextFormatFlags"/> based on the <see cref="Label"/>'s properties
        /// </summary>
        /// <returns>Self-explanatory</returns>
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
            if (AutoSize && Text.Split('\n').Length <= 1) {
                Flags |= TextFormatFlags.SingleLine;
            } else {
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
            if (!UseMnemonic) {
                Flags |= TextFormatFlags.NoPrefix;
            } else if (!ShowKeyboardCues) {
                Flags |= TextFormatFlags.HidePrefix;
            }
            return Flags;
        }

    }
}