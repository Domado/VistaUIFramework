//--------------------------------------------------------------------
// <copyright file="ProgressBar.cs" company="MyAPKapp">
//     Copyright (c) MyAPKapp. All rights reserved.
// </copyright>                                                                
//--------------------------------------------------------------------
// This open-source project is licensed under Apache License 2.0
//--------------------------------------------------------------------

using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System;

namespace MyAPKapp.VistaUIFramework {
    [ToolboxBitmap(typeof(System.Windows.Forms.ProgressBar))]
    public class ProgressBar : System.Windows.Forms.ProgressBar {

        private ProgressState _State;

        public ProgressBar() : base() {
            NativeMethods.SendMessage(Handle, NativeMethods.PBM_SETSTATE, NativeMethods.PBST_NORMAL, 0);
        }

        /// <summary>
        /// Set the progress state as the progress color
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(ProgressState.Normal)]
        [Description("Set the progress state as the progress color")]
        public ProgressState State {
            get {
                return _State;
            }
            set {
                _State = value;
                SetProgressBarState(value);
            }
        }

        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                cp.Style |= NativeMethods.PBS_SMOOTHREVERSE;
                return cp;
            }
        }

        private void SetProgressBarState(ProgressState state) {
            NativeMethods.SendMessage(Handle, NativeMethods.PBM_SETSTATE, NativeMethods.PBST_NORMAL, 0);
            switch (state) {
                case ProgressState.Normal:
                    NativeMethods.SendMessage(Handle, NativeMethods.PBM_SETSTATE, NativeMethods.PBST_NORMAL, 0);
                    break;
                case ProgressState.Error:
                    NativeMethods.SendMessage(Handle, NativeMethods.PBM_SETSTATE, NativeMethods.PBST_ERROR, 0);
                    break;
                case ProgressState.Paused:
                    NativeMethods.SendMessage(Handle, NativeMethods.PBM_SETSTATE, NativeMethods.PBST_PAUSED, 0);
                    break;
                default:
                    NativeMethods.SendMessage(Handle, NativeMethods.PBM_SETSTATE, NativeMethods.PBST_NORMAL, 0);
                    break;
            }
        }

        protected override void WndProc(ref Message m) {
            switch (m.Msg) {
                case NativeMethods.WM_PAINT:
                    SetProgressBarState(_State);
                    break;
            }
            base.WndProc(ref m);
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