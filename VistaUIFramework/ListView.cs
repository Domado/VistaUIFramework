using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework {
    [ToolboxBitmap(typeof(System.Windows.Forms.ListView))]
    public class ListView : System.Windows.Forms.ListView {

        private bool styled;
        private bool _SelectRequired;

        public ListView() : base() {}

        protected override void WndProc(ref Message m) {
            switch (m.Msg) {
                case NativeMethods.WM_PAINT:
                    if (!styled) {
                        NativeMethods.SetWindowTheme(Handle, "explorer", null);
                        NativeMethods.SendMessage(Handle, NativeMethods.LVM_SETEXTENDEDLISTVIEWSTYLE, NativeMethods.LVS_EX_DOUBLEBUFFER, NativeMethods.LVS_EX_DOUBLEBUFFER);
                        styled = true;
                    }
                    break;
            }
            if (m.Msg >= 0x201 && m.Msg <= 0x209 && _SelectRequired) {
                Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                var hit = this.HitTest(pos);
                switch (hit.Location) {
                    case ListViewHitTestLocations.AboveClientArea:
                    case ListViewHitTestLocations.BelowClientArea:
                    case ListViewHitTestLocations.LeftOfClientArea:
                    case ListViewHitTestLocations.RightOfClientArea:
                    case ListViewHitTestLocations.None:
                    return;
                }
            }
            base.WndProc(ref m);
        }

        #region Public properties

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

        /// <summary>
        /// Set if ListView is not allowed to have zero selected items
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Set if ListView is not allowed to have zero selected items")]
        public bool SelectRequired {
            get {
                return _SelectRequired;
            }
            set {
                if (_SelectRequired != value) {
                    _SelectRequired = value;
                    RecreateHandle();
                }
            }
        }

        #endregion

    }
}
