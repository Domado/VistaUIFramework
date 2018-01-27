using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework {

    /// <summary>
    /// It's a splitted button with an arrow
    /// </summary>
    [ToolboxBitmap(typeof(System.Windows.Forms.Button))]
    public class SplitButton : Button {

        public SplitButton() : base() {}

        /// <summary>
        /// Fires when split arrow is clicked
        /// </summary>
        [Category("Action")]
        [Description("Fires when split arrow is clicked")]
        public event SplitClickEventHandler SplitClick;

        /// <summary>
        /// Set the split button's menu, set the SplitClick e.cancel to true to prevent the menu from showing
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(null)]
        [Description("Set the split button's menu, set the SplitClick e.cancel to true to prevent the menu from showing")]
        public ContextMenu Menu {get; set;}

        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                cp.Style &= ~NativeMethods.BS_PUSHBUTTON;
                cp.Style |= NativeMethods.BS_SPLITBUTTON;
                if (IsDefault) {
                    cp.Style &= ~NativeMethods.BS_DEFPUSHBUTTON;
                    cp.Style |= NativeMethods.BS_DEFSPLITBUTTON;
                }
                return cp;
            }
        }

        protected override void WndProc(ref Message m) {
            switch (m.Msg) {
                case (NativeMethods.BCM_SETDROPDOWNSTATE):
                    if (m.HWnd==Handle && m.WParam.ToInt32()==NativeMethods.TRUE) {
                        if (SplitClick != null) {
                            OnSplitClick(new SplitClickEventArgs(Menu));
                        } else if (Menu != null) {
                            Menu.Show(this, new Point(0, Height));
                        }
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnSplitClick(SplitClickEventArgs e) {
            SplitClick(this, e);
            if (!e.Cancel && Menu != null) {
                Menu.Show(this, new Point(0, Height));
            }
        }

        protected override Size DefaultSize {
            get {
                return new Size(108, base.DefaultSize.Height);
            }
        }

    }
}
