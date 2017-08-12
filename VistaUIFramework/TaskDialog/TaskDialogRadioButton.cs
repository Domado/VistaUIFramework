using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Text;

namespace MyAPKapp.VistaUIFramework.TaskDialog {

    /// <summary>
    /// The radio button made for TaskDialog
    /// </summary>
    [ToolboxItem(false)]
    [DesignTimeVisible(false)]
    [DefaultProperty("Text")]
    public class TaskDialogRadioButton : Component {

        private NativeMethods.TASKDIALOG_BUTTON _NativeButton;
        private TaskDialog _Parent;
        private bool _Enabled;

        /// <summary>
        /// Initializes a new instance of radio button
        /// </summary>
        public TaskDialogRadioButton() : base() {
            _NativeButton = new NativeMethods.TASKDIALOG_BUTTON();
            _Enabled = true;
        }

        internal void SetParent(TaskDialog Dialog) {
            _Parent = Dialog;
        }

        /// <summary>
        /// Simulates a click on the radio button
        /// </summary>
        public void PerformClick() {
            if (_Parent != null && _Parent.IsShown) {
                NativeMethods.SendMessage(_Parent.Handle, NativeMethods.TDM_CLICK_RADIO_BUTTON, ID, 0);
            }
        }

        #region Public properties

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal int ID {
            get {
                return _NativeButton.nButtonID;
            }
            set {
                if (_NativeButton.nButtonID != value) {
                    _NativeButton.nButtonID = value;
                }
            }
        }

        /// <summary>
        /// Set the button base text
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(null)]
        [Description("Set the radio button text")]
        [Localizable(true)]
        public string Text {
            get {
                return _NativeButton.pszButtonText;
            }
            set {
                if (_NativeButton.pszButtonText != value) {
                    _NativeButton.pszButtonText = value.Replace("\r", "");
                }
            }
        }

        /// <summary>
        /// Set if radio button is enabled or disabled
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("Set if radio button is enabled or disabled")]
        public bool Enabled {
            get {
                return _Enabled;
            }
            set {
                if (_Enabled != value) {
                    _Enabled = value;
                    if (_Parent != null && _Parent.IsShown) {
                        NativeMethods.SendMessage(_Parent.Handle, NativeMethods.TDM_ENABLE_RADIO_BUTTON, ID, NativeMethods.BoolToNative(value));
                    }
                }
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal NativeMethods.TASKDIALOG_BUTTON NativeButton {
            get {
                return _NativeButton;
            }
        }

        /// <summary>
        /// Get the dialog that contains this button
        /// </summary>
        /// <remarks>
        /// Parent is not null when dialog is shown
        /// </remarks>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public TaskDialog Parent {
            get {
                return _Parent;
            }
        }

        #endregion

        public override string ToString() {
            return Text;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _Enabled = true;
            }
            _NativeButton = new NativeMethods.TASKDIALOG_BUTTON();
            base.Dispose(disposing);
        }

    }

}
