using System.ComponentModel;
using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework {
    public class SplitClickEventArgs : CancelEventArgs {

        public SplitClickEventArgs(ContextMenu menu) : base() {
            Menu = menu;
        }

        /// <summary>
        /// Get the split button's menu, set the SplitClick e.cancel to true to prevent the menu from showing
        /// </summary>
        public ContextMenu Menu {get;}

    }
}
