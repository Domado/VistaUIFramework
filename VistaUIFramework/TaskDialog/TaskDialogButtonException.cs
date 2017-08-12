using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPKapp.VistaUIFramework.TaskDialog {
    public class TaskDialogButtonException : Exception {

        public TaskDialogButtonException() : base("Selected button is not found or belongs to other TaskDialog") {}

    }
}
