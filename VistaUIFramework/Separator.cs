using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace MyAPKapp.VistaUIFramework {

    [Designer(typeof(SeparatorDesigner))]
    [ToolboxBitmap(typeof(Separator), "MyAPKapp.VistaUIFramework.Separator.bmp")]
    public class Separator : System.Windows.Forms.Label {

        public Separator() : base() {
            base.FlatStyle = FlatStyle.System;
            AutoSize = false;
            Height = 2;
            BorderStyle = BorderStyle.Fixed3D;
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

        [Browsable(false)]
        public override bool AutoSize {
            get {
                return false;
            }
        }

        [Browsable(false)]
        public override Size MaximumSize {
            get {
                return new Size(int.MaxValue, 2);
            }
        }

        [Browsable(false)]
        public override Size MinimumSize {
            get {
                return new Size(1, 2);
            }
        }

        [Browsable(false)]
        public override string Text {
            get {
                return string.Empty;
            }
        }

        [Browsable(false)]
        public override ContentAlignment TextAlign {
            get {
                return base.TextAlign;
            }
            set {
                base.TextAlign = value;
            }
        }

        private class SeparatorDesigner : ControlDesigner {
            public SeparatorDesigner() {
                AutoResizeHandles = true;
            }
            public override SelectionRules SelectionRules {
                get {
                    return SelectionRules.LeftSizeable | SelectionRules.RightSizeable | SelectionRules.Moveable;
                }
            }
        }

    }
}
