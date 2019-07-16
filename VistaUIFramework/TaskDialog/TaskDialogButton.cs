//--------------------------------------------------------------------
// <copyright file="TaskDialogButton.cs" company="MyAPKapp">
//     Copyright (c) MyAPKapp. All rights reserved.
// </copyright>                                                                
//--------------------------------------------------------------------
// This open-source project is licensed under Apache License 2.0
//--------------------------------------------------------------------

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace MyAPKapp.VistaUIFramework.TaskDialog {

    /// <summary>
    /// The button made for TaskDialog
    /// </summary>
    [ToolboxItem(false)]
    [DesignTimeVisible(false)]
    [DefaultProperty("Text")]
    public class TaskDialogButton : TaskDialogRadioButton {

        private bool _Enabled;

        /// <summary>
        /// Initializes a new instance of button
        /// </summary>
        public TaskDialogButton() : base() {
            _Enabled = true;
        }

        /// <summary>
        /// Simulates a click on the button
        /// </summary>
        public new void PerformClick() {
            if (Parent != null && Parent.IsShown) {
                NativeMethods.SendMessage(Parent.Handle, NativeMethods.TDM_CLICK_BUTTON, ID, 0);
            }
        }

        #region Public properties

        /// <summary>
        /// Set the button text. If UseCommandLinks is enabled, text beginning from 2nd line will be converted into CommandLink summary
        /// </summary>
        [Description("Set the button text. If UseCommandLinks is enabled, text beginning from 2nd line will be converted into CommandLink summary")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public new string Text {
            get {
                return base.Text;
            }
            set {
                base.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets if button is enabled or disabled
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("Gets or sets if button is enabled or disabled")]
        public new bool Enabled {
            get {
                return _Enabled;
            }
            set {
                if (_Enabled != value) {
                    _Enabled = value;
                    if (Parent != null && Parent.IsShown) {
                        NativeMethods.SendMessage(Parent.Handle, NativeMethods.TDM_ENABLE_RADIO_BUTTON, ID, NativeMethods.BoolToNative(value));
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets if button has the UAC Shield
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("Gets or sets if button has the UAC Shield")]
        public bool Shield { get; set; }

        #endregion

        protected override void Dispose(bool disposing) {
            if (disposing) {
                Shield = false;
            }
            base.Dispose(disposing);
        }

    }
}