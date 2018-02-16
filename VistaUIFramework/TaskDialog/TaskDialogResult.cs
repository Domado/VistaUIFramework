//--------------------------------------------------------------------
// <copyright file="TaskDialogResult.cs" company="myapkapp">
//     Copyright (c) myapkapp. All rights reserved.
// </copyright>                                                                
//--------------------------------------------------------------------
// This open-source project is licensed under Apache License 2.0
//--------------------------------------------------------------------

using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework.TaskDialog {

    /// <summary>
    /// The TaskDialog result when button is pressed
    /// </summary>
    public class TaskDialogResult {

        private bool _IsCheckboxChecked;
        private TaskDialogRadioButton _RadioButton;

        /// <summary>
        /// Initializes a new instance of TaskDialogResult
        /// </summary>
        /// <param name="Button">The common button clicked</param>
        internal TaskDialogResult(DialogResult Button) {
            CommonButton = Button;
            IsCustomButton = false;
        }

        /// <summary>
        /// Initializes a new instance of TaskDialogResult
        /// </summary>
        /// <param name="Button">The custom button clicked</param>
        internal TaskDialogResult(TaskDialogButton Button) {
            this.Button = Button;
            IsCustomButton = true;
        }

        /// <summary>
        /// The common button clicked
        /// </summary>
        public DialogResult CommonButton { get; }

        /// <summary>
        /// The custom button clicked
        /// </summary>
        public TaskDialogButton Button { get; }

        /// <summary>
        /// Check if button clicked was custom or common
        /// </summary>
        public bool IsCustomButton { get; }

        /// <summary>
        /// The radio button selected
        /// </summary>
        public TaskDialogRadioButton RadioButton {
            get {
                return _RadioButton;
            }
        }

        /// <summary>
        /// Check if checkbox was checked
        /// </summary>
        public bool IsCheckboxChecked {
            get {
                return _IsCheckboxChecked;
            }
        }

        internal void SetRadioButton(TaskDialogRadioButton RadioButton) {
            _RadioButton = RadioButton;
        }

        internal void SetCheckboxChecked(bool Checked) {
            _IsCheckboxChecked = Checked;
        }

    }
}