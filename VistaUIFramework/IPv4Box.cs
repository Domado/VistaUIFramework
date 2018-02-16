//--------------------------------------------------------------------
// <copyright file="IPv4Box.cs" company="myapkapp">
//     Copyright (c) myapkapp. All rights reserved.
// </copyright>                                                                
//--------------------------------------------------------------------
// This open-source project is licensed under Apache License 2.0
//--------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace MyAPKapp.VistaUIFramework {

    /// <summary>
    /// <see cref="IPv4Box"/> is a box that shows a field for an IPv4 address
    /// </summary>
    [ToolboxBitmap(typeof(System.Windows.Forms.TextBox))]
    [Description("IPv4Box is a box that shows a field for an IPv4 address")]
    [Designer(typeof(IPv4BoxDesigner))]
    public class IPv4Box : System.Windows.Forms.TextBox {

        public IPv4Box() : base() {
            Text = "0.0.0.0";
        }

        protected override void CreateHandle() {
            if (!RecreatingHandle) {
                NativeMethods.INITCOMMONCONTROLSEX iccex = new NativeMethods.INITCOMMONCONTROLSEX {
                    dwSize = Marshal.SizeOf(typeof(NativeMethods.INITCOMMONCONTROLSEX)),
                    dwICC = NativeMethods.ICC_INTERNET_CLASSES
                };
                NativeMethods.InitCommonControlsEx(ref iccex);
            }
            base.CreateHandle();
        }

        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                cp.ClassName = NativeMethods.WC_IPADDRESS;
                cp.ClassStyle = NativeMethods.CS_VREDRAW | NativeMethods.CS_HREDRAW | NativeMethods.CS_DBLCLKS | NativeMethods.CS_GLOBALCLASS;
                cp.ExStyle |= NativeMethods.WS_EX_NOPARENTNOTIFY;
                cp.ExStyle &= ~(NativeMethods.WS_EX_CLIENTEDGE);
                cp.Style &= ~(NativeMethods.WS_BORDER);
                switch (BorderStyle) {
                    case BorderStyle.FixedSingle:
                        cp.Style |= NativeMethods.WS_BORDER;
                        break;
                    case BorderStyle.Fixed3D:
                        cp.ExStyle |= NativeMethods.WS_EX_CLIENTEDGE;
                        break;
                }
                if (RightToLeft == RightToLeft.Yes) {
                    cp.ExStyle |= NativeMethods.WS_EX_LAYOUTRTL;
                    cp.ExStyle &= ~(NativeMethods.WS_EX_RIGHT | NativeMethods.WS_EX_RTLREADING | NativeMethods.WS_EX_LEFTSCROLLBAR);
                }
                return cp;
            }
        }

        protected override bool IsInputKey(Keys keyData) {
            if ((keyData & Keys.Alt) != Keys.Alt) {
                switch (keyData & Keys.KeyCode) {
                    case Keys.Back:
                        if (!ReadOnly) return true;
                        break;
                    case Keys.PageUp:
                    case Keys.PageDown:
                    case Keys.Home:
                    case Keys.End:
                    case Keys.Left:
                    case Keys.Right:
                        return true;
                }
            }
            return base.IsInputKey(keyData);
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

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new bool Multiline { get => base.Multiline; set => base.Multiline = value; }

        [DefaultValue("0.0.0.0")]
        [Editor(typeof(UITypeEditor), typeof(UITypeEditor))]
        public override string Text { get => base.Text; set => base.Text = value; }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public new event EventHandler ContextMenuChanged { add => base.ContextMenuChanged += value; remove => base.ContextMenuChanged -= value; }

        private class IPv4BoxDesigner : ControlDesigner {

            public override DesignerActionListCollection ActionLists => null;

            public override SelectionRules SelectionRules {
                get {
                    return base.SelectionRules & ~(SelectionRules.TopSizeable) & ~(SelectionRules.BottomSizeable);
                }
            }

        }

    }
}
