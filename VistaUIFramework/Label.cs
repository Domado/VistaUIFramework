using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework {
    [ToolboxBitmap(typeof(System.Windows.Forms.Label))]
    public class Label : System.Windows.Forms.Label {

        private LabelStyle _LabelStyle = LabelStyle.Default;
        private Font defaultFont;
        private Color defaultColor;
        private bool _Aero;

        public Label() : base() {
            base.FlatStyle = FlatStyle.System;
            FontChanged += LabelStyle_FontChanged;
            ForeColorChanged += LabelStyle_ForeColorChanged;
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint, true);

            BackColor = Color.Transparent;
        }

        /// <summary>
        /// The Vista label styles
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(LabelStyle.Default)]
        [Description("The Vista label styles")]
        public LabelStyle LabelStyle {
            get {
                return _LabelStyle;
            }
            set {
                _LabelStyle = value;
                changeLabelStyle(value);
            }
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
                ForeColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            } else if (LabelStyle == LabelStyle.GroupHeader) {
                defaultFont = base.Font;
                defaultColor = base.ForeColor;
                Font = new Font("Segoe UI", 11F, FontStyle.Regular);
                ForeColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            }
            FontChanged += LabelStyle_FontChanged;
            ForeColorChanged += LabelStyle_ForeColorChanged;
        }

        private void LabelStyle_FontChanged(object sender, EventArgs e) {
            defaultFont = base.Font;
            defaultColor = base.ForeColor;
            LabelStyle = LabelStyle.Default;
        }

        private void LabelStyle_ForeColorChanged(object sender, EventArgs e) {
            defaultFont = base.Font;
            defaultColor = base.ForeColor;
            LabelStyle = LabelStyle.Default;
        }

        [DefaultValue(FlatStyle.System)]
        public new FlatStyle FlatStyle {
            get { return base.FlatStyle; }
            set { base.FlatStyle = value; }
        }

        [DefaultValue(typeof(Color), "Transparent")]
        public override Color BackColor {
            get {
                return base.BackColor;
            }
            set {
                base.BackColor = value;
            }
        }

    }
}
