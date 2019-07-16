//--------------------------------------------------------------------
// <copyright file="TreeView.cs" company="MyAPKapp">
//     Copyright (c) MyAPKapp. All rights reserved.
// </copyright>                                                                
//--------------------------------------------------------------------
// This open-source project is licensed under Apache License 2.0
//--------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework {
    [ToolboxBitmap(typeof(System.Windows.Forms.ListView))]
    public class TreeView : System.Windows.Forms.TreeView {

        public TreeView() : base() {}

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            NativeMethods.SetWindowTheme(Handle, "explorer", null);
            int extended = NativeMethods.SendMessage(Handle, NativeMethods.TVM_GETEXTENDEDSTYLE, 0, 0).ToInt32();
            extended |= (NativeMethods.TVS_EX_AUTOHSCROLL | NativeMethods.TVS_EX_FADEINOUTEXPANDOS | NativeMethods.TVS_EX_DOUBLEBUFFER);
            NativeMethods.SendMessage(Handle, NativeMethods.TVM_SETEXTENDEDSTYLE, 0, extended);
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

    }
}