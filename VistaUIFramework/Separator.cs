//--------------------------------------------------------------------
// <copyright file="Separator.cs" company="myapkapp">
//     Copyright (c) myapkapp. All rights reserved.
// </copyright>                                                                
//--------------------------------------------------------------------
// This open-source project is licensed under Apache License 2.0
//--------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace MyAPKapp.VistaUIFramework {

    /// <summary>
    /// Separator are controls that separates
    /// </summary>
    [Designer(typeof(SeparatorDesigner))]
    [ToolboxBitmap(typeof(Separator), "MyAPKapp.VistaUIFramework.Separator.bmp")]
    [Description("Separator are controls that separates")]
    public class Separator : System.Windows.Forms.Label {

        public Separator() : base() {
            base.FlatStyle = FlatStyle.System;
            AutoSize = false;
            Height = 2;
            BorderStyle = BorderStyle.Fixed3D;
        }

        /// <summary>
        /// This property can alter the native purpose of VistaUI
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [Obsolete("This property can alter the native purpose of VistaUI")]
        [DefaultValue(typeof(FlatStyle), "System")]
        public new FlatStyle FlatStyle {
            get { return base.FlatStyle; }
            set { base.FlatStyle = value; }
        }

        [Browsable(false)]
        public override bool AutoSize {
            get {
                return false;
            }
        }

        [Browsable(false)]
        public override Size MaximumSize {
            get {
                return new Size(int.MaxValue, 2);
            }
        }

        [Browsable(false)]
        public override Size MinimumSize {
            get {
                return new Size(1, 2);
            }
        }

        [Browsable(false)]
        public override string Text {
            get {
                return string.Empty;
            }
        }

        [Browsable(false)]
        public override ContentAlignment TextAlign {
            get {
                return base.TextAlign;
            }
            set {
                base.TextAlign = value;
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

        public class SeparatorDesigner : ControlDesigner {
            public SeparatorDesigner() {
                AutoResizeHandles = true;
            }
            public override SelectionRules SelectionRules {
                get {
                    return SelectionRules.LeftSizeable | SelectionRules.RightSizeable | SelectionRules.Moveable;
                }
            }
        }

    }
}
