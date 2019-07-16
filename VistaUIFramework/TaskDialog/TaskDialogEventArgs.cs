//--------------------------------------------------------------------
// <copyright file="TaskDialogEventArgs.cs" company="MyAPKapp">
//     Copyright (c) MyAPKapp. All rights reserved.
// </copyright>                                                                
//--------------------------------------------------------------------
// This open-source project is licensed under Apache License 2.0
//--------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework.TaskDialog {

    /// <summary>
    /// Provides data for the ButtonClick event
    /// </summary>
    public class ButtonClickEventArgs : CancelEventArgs {

        /// <summary>
        /// Initializes a new instance of ButtonClickEventArgs
        /// </summary>
        /// <param name="Button">The button clicked in dialog, for custom buttons</param>
        public ButtonClickEventArgs(TaskDialogButton Button) : base() {
            this.Button = Button;
            IsCustomButton = true;
        }

        /// <summary>
        /// Initializes a new instance of ButtonClickEventArgs
        /// </summary>
        /// <param name="Button">The button clicked in dialog, for custom buttons</param>
        /// <param name="cancel"></param>
        public ButtonClickEventArgs(TaskDialogButton Button, bool cancel) : base(cancel) {
            this.Button = Button;
            IsCustomButton = true;
        }

        /// <summary>
        /// Initializes a new instance of ButtonClickEventArgs
        /// </summary>
        /// <param name="Button">The button clicked in dialog, for common buttons</param>
        public ButtonClickEventArgs(DialogResult Button) : base() {
            CommonButton = Button;
            IsCustomButton = false;
        }

        /// <summary>
        /// Initializes a new instance of ButtonClickEventArgs
        /// </summary>
        /// <param name="Button">The button clicked in dialog, for common buttons</param>
        /// <param name="cancel"></param>
        public ButtonClickEventArgs(DialogResult Button, bool cancel) : base(cancel) {
            CommonButton = Button;
            IsCustomButton = false;
        }

        /// <summary>
        /// The button clicked in dialog, for custom buttons
        /// </summary>
        public TaskDialogButton Button { get; }

        /// <summary>
        /// The button clicked in dialog, for common buttons
        /// </summary>
        public DialogResult CommonButton { get; }

        /// <summary>
        /// Check if clicked button is a custom or common button
        /// </summary>
        public bool IsCustomButton { get; }

    }

    /// <summary>
    /// Provides data for the RadioButtonClick event
    /// </summary>
    public class RadioButtonClickEventArgs : EventArgs {

        public RadioButtonClickEventArgs(TaskDialogRadioButton RadioButton) : base() {
            this.RadioButton = RadioButton;
        }

        /// <summary>
        /// The radio button clicked in dialog
        /// </summary>
        public TaskDialogRadioButton RadioButton { get; }

    }

    /// <summary>
    /// Provides data for the CheckboxChecked event
    /// </summary>
    public class CheckboxCheckedEventArgs : EventArgs {

        public CheckboxCheckedEventArgs(bool Checked) {
            this.Checked = Checked;
        }

        /// <summary>
        /// Check if checkbox is checked
        /// </summary>
        public bool Checked { get; set; }

    }

    /// <summary>
    /// Provides data for the TimerTick event
    /// </summary>
    public class TimerTickEventArgs : EventArgs {

        public TimerTickEventArgs(int ElapsedTime) : base() {
            this.ElapsedTime = ElapsedTime;
        }

        /// <summary>
        /// Get the elapsed time since dialog was created
        /// </summary>
        public int ElapsedTime { get; }
        
        internal bool ResetPending { get; set; }

        /// <summary>
        /// Reset the timer in order to count from beginning
        /// </summary>
        public void ResetTimer() {
            ResetPending = true;
        }

    }

    /// <summary>
    /// Provides data for the ExpandCollapse event
    /// </summary>
    public class ExpandCollapseEventArgs : EventArgs {

        public ExpandCollapseEventArgs(bool Expanded) {
            this.Expanded = Expanded;
        }

        /// <summary>
        /// Get if expanded information was expanded
        /// </summary>
        public bool Expanded { get; }

    }

}
