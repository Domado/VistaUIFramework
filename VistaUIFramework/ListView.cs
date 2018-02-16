//--------------------------------------------------------------------
// <copyright file="ListView.cs" company="myapkapp">
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

namespace MyAPKapp.VistaUIFramework {
    [ToolboxBitmap(typeof(System.Windows.Forms.ListView))]
    public class ListView : System.Windows.Forms.ListView {

        private bool styled;
        private bool _SelectRequired;
        private bool _CollapsibleGroups;

        public ListView() : base() {
        }

        protected override void WndProc(ref Message m) {
            switch (m.Msg) {
                case NativeMethods.WM_PAINT:
                    if (!styled) {
                        NativeMethods.SetWindowTheme(Handle, "explorer", null);
                        NativeMethods.SendMessage(Handle, NativeMethods.LVM_SETEXTENDEDLISTVIEWSTYLE, NativeMethods.LVS_EX_DOUBLEBUFFER, NativeMethods.LVS_EX_DOUBLEBUFFER);
                        styled = true;
                    }
                    break;
                case NativeMethods.WM_LBUTTONUP:
                    if (CollapsibleGroups) base.DefWndProc(ref m);
                    return;
            }
            if (m.Msg >= 0x201 && m.Msg <= 0x209 && _SelectRequired) {
                Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                ListViewHitTestInfo hit = HitTest(pos);
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

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            if (CollapsibleGroups && Groups.Count > 0) {
                for (int i = 0; i < Groups.Count; i++) {
                    NativeMethods.LVGROUP group = new NativeMethods.LVGROUP {
                        cbSize = Marshal.SizeOf(typeof(NativeMethods.LVGROUP)),
                        mask = NativeMethods.LVGF_GROUPID
                    };
                    IntPtr Result = NativeMethods.SendMessage(Handle, NativeMethods.LVM_GETGROUPINFOBYINDEX, i, ref group);
                    if (NativeMethods.NativeToBool(Result.ToInt32())) {
                        int groupId = group.iGroupId;
                        group = new NativeMethods.LVGROUP {
                            cbSize = Marshal.SizeOf(group),
                            state = NativeMethods.LVGS_COLLAPSIBLE,
                            mask = NativeMethods.LVGF_STATE,
                            iGroupId = groupId
                        };
                        NativeMethods.SendMessage(Handle, NativeMethods.LVM_SETGROUPINFO, group.iGroupId, ref group);
                    }
                }
            }
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

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public new event EventHandler ContextMenuChanged { add => base.ContextMenuChanged += value; remove => base.ContextMenuChanged -= value; }

        /// <summary>
        /// Gets or sets if <see cref="ListView"/> is not allowed to have zero selected items
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Gets or sets if ListView is not allowed to have zero selected items")]
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

        /// <summary>
        /// Gets or sets if <see cref="System.Windows.Forms.ListView.Groups"/> are collapsible
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Gets or sets if ListView groups are collapsible")]
        public bool CollapsibleGroups {
            get {
                return _CollapsibleGroups;
            }
            set {
                if (_CollapsibleGroups != value) {
                    _CollapsibleGroups = value;
                    RecreateHandle();
                }
            }
        }

        #endregion

    }
}
