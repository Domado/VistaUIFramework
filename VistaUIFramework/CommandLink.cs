﻿//--------------------------------------------------------------------
// <copyright file="CommandLink.cs" company="MyAPKapp">
//     Copyright (c) MyAPKapp. All rights reserved.
// </copyright>                                                                
//--------------------------------------------------------------------
// This open-source project is licensed under Apache License 2.0
//--------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace MyAPKapp.VistaUIFramework {

    /// <summary>
    /// <see cref="CommandLink"/> is a button with green arrow and a description (<code>Note</code> property)
    /// </summary>
    [ToolboxBitmap(typeof(System.Windows.Forms.Button))]
    [Description("CommandLink is a button with green arrow and a description")]
    public class CommandLink : Button {

        private string note;

        /// <summary>
        /// <see cref="CommandLink"/> is a button with green arrow and a description (<code>Note</code> property)
        /// </summary>
        public CommandLink() : base() {}

        /// <summary>
        /// The <see cref="CommandLink"/> summary
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(null)]
        [Description("The CommandLink summary")]
        [Editor(typeof(MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Localizable(true)]
        public string Note {
            get {
                return note;
            }
            set {
                if (note != value) {
                    note = value;
                    UpdateButton();
                }
            }
        }

        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                cp.Style |= NativeMethods.BS_COMMANDLINK;
                if (IsDefault) {
                    cp.Style |= NativeMethods.BS_DEFCOMMANDLINK;
                }
                return cp;
            }
        }

        protected override Size DefaultSize {
            get {
                return new Size(200, 74);
            }
        }

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            UpdateButton();
        }

        private void UpdateButton() {
            NativeMethods.SendMessage(Handle, NativeMethods.BCM_SETNOTE, IntPtr.Zero, note);
        }

    }
}
