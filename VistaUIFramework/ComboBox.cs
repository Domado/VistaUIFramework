//--------------------------------------------------------------------
// <copyright file="ComboBox.cs" company="myapkapp">
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

namespace MyAPKapp.VistaUIFramework {
    [ToolboxBitmap(typeof(System.Windows.Forms.ComboBox))]
    public class ComboBox : System.Windows.Forms.ComboBox {

        private string _Hint;

        public ComboBox() : base() {
            base.FlatStyle = FlatStyle.System;
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

        /// <summary>
        /// Set the ComboBox's gray text when ComboBox is editable and empty
        /// </summary>
        /// <remarks>
        /// Unlike <code>TextBox</code>, there's not <code>HideHintOnFocus</code> property available
        /// </remarks>
        [Category("Appearance")]
        [DefaultValue(null)]
        [Localizable(true)]
        [Description("Set the ComboBox's gray text when ComboBox is editable and empty")]
        public string Hint {
            get {
                return _Hint;
            }
            set {
                _Hint = value;
                NativeMethods.SendMessage(Handle, NativeMethods.CB_SETCUEBANNER, IntPtr.Zero, value);
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

    }
}
