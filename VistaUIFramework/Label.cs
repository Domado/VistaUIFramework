using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;

namespace MyAPKapp.VistaUIFramework {
    [ToolboxBitmap(typeof(System.Windows.Forms.Button))]
    public class Label : System.Windows.Forms.Label {

        private LabelStyle _LabelStyle = LabelStyle.Default;
        private Font defaultFont;
        private Color defaultColor;

        public Label() : base() {
            base.FlatStyle = FlatStyle.System;
            FontChanged += LabelStyle_FontChanged;
            ForeColorChanged += LabelStyle_ForeColorChanged;
        }

        public LabelStyle LabelStyle {
            get {
                return _LabelStyle;
            }
            set {
                _LabelStyle = value;
                changeLabelStyle(value);
            }
        }

        private void changeLabelStyle(LabelStyle labelStyle) {
            FontChanged -= LabelStyle_FontChanged;
            ForeColorChanged -= LabelStyle_ForeColorChanged;
            if (labelStyle == LabelStyle.Default) {
                Font = defaultFont;
                ForeColor = defaultColor;
            } else if (labelStyle == LabelStyle.MainInstructions) {
                defaultFont = base.Font;
                defaultColor = base.ForeColor;
                Font = new Font("Segoe UI", 12F, FontStyle.Regular);
                ForeColor = Color.FromArgb(0, 0, 51, 15);
            } else if (LabelStyle == LabelStyle.GroupHeader) {
                defaultFont = base.Font;
                defaultColor = base.ForeColor;
                Font = new Font("Segoe UI", 11F, FontStyle.Regular);
                ForeColor = Color.FromArgb(0, 0, 51, 15);
            }
            FontChanged += LabelStyle_FontChanged;
            ForeColorChanged += LabelStyle_ForeColorChanged;
        }

        private void LabelStyle_FontChanged(object sender, EventArgs e) {
            defaultFont = base.Font;
            defaultColor = base.ForeColor;
            changeLabelStyle(LabelStyle.Default);
        }

        private void LabelStyle_ForeColorChanged(object sender, EventArgs e) {
            defaultFont = base.Font;
            defaultColor = base.ForeColor;
            changeLabelStyle(LabelStyle.Default);
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

    }
}
