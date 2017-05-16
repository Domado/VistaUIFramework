using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework {

    [ToolboxBitmap(typeof(System.Windows.Forms.LinkLabel))]
    public class LinkLabel : System.Windows.Forms.LinkLabel {

        private Color tempColor;
        private bool _Shield;

        public LinkLabel() : base() {
            ActiveLinkColor = Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            HoverLinkColor = Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            ForeColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            LinkColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            MouseEnter += LinkLabel_MouseEnter;
            MouseLeave += LinkLabel_MouseLeave;
        }

        #region Events for HoverColor property

        private void LinkLabel_MouseLeave(object sender, EventArgs e) {
            LinkColor = tempColor;
        }

        private void LinkLabel_MouseEnter(object sender, EventArgs e) {
            tempColor = LinkColor;
            LinkColor = HoverLinkColor;
        }

        #endregion

        #region Public properties

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

        #endregion

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
