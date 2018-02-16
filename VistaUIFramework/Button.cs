//--------------------------------------------------------------------
// <copyright file="Button.cs" company="myapkapp">
//     Copyright (c) myapkapp. All rights reserved.
// </copyright>                                                                
//--------------------------------------------------------------------
// This open-source project is licensed under Apache License 2.0
//--------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework {
    [ToolboxBitmap(typeof(System.Windows.Forms.Button))]
    public class Button : System.Windows.Forms.Button {

        private Icon _Icon;
        private Image _Image;
        private bool _Shield;
        private bool _ImageOnly;

        /// <summary>
        /// The native button inherited from WinForms button<br />
        /// Use ContextMenu instead of ContextMenuStrip
        /// </summary>
        public Button() : base() {
            base.FlatStyle = FlatStyle.System;
        }

        #region Public properties

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

        /// <summary>
        /// Set the button's icon, the default icon size is defined by Windows API
        /// </summary>
        [Category("Appearance")]
        [DefaultValue((Icon) null)]
        [Description("Set the button's icon, the default icon size is defined by Windows API")]
        public Icon Icon {
            get {
                return _Icon;
            }
            set {
                SetShield(false);
                _Shield = false;
                _Icon = value;
                if (_Image != null) {
                    _Image = null;
                    RemoveImage();
                }
                if (value != null) {
                    SetIcon(_Icon);
                } else {
                    RemoveIcon();
                }
            }
        }

        [Browsable(true)]
        [DefaultValue((Image) null)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public new Image Image {
            get {
                return _Image;
            }
            set {
                SetShield(false);
                _Shield = false;
                _Image = value;
                if (_Icon != null) {
                    _Icon = null;
                    RemoveIcon();
                }
                if (value != null) {
                    SetImage(_Image);
                } else {
                    RemoveImage();
                }
            }
        }

        /// <summary>
        /// Set if button has image only
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("Set if button has image only")]
        public bool ImageOnly {
            get {
                return _ImageOnly;
            }
            set {
                _ImageOnly = value;
                RecreateHandle();
            }
        }

        /// <summary>
        /// Set the button's shield to tell user action requires administrator privileges
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("Set the button's shield to tell user action requires administrator privileges")]
        public bool Shield {
            get {
                return _Shield;
            }
            set {
                _Shield = value;
                if (value) {
                    if (_Icon != null) {
                        _Icon = null;
                        RemoveIcon();
                    }
                    if (_Image != null) {
                        _Image = null;
                        RemoveImage();
                    }
                }
                SetShield(value);
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

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public new event EventHandler ContextMenuChanged { add => base.ContextMenuChanged += value; remove => base.ContextMenuChanged -= value; }

        #endregion

        #region Protected and private methods

        protected override CreateParams CreateParams {
            get {
                if (_ImageOnly) {
                    CreateParams cp = base.CreateParams;
                    cp.Style |= NativeMethods.BS_ICON;
                    return cp;
                }
                return base.CreateParams;
            }
        }

        private void SetImage(Image img) {
            Bitmap bitmap = new Bitmap(img);
            NativeMethods.SendMessage(Handle, NativeMethods.BM_SETIMAGE, NativeMethods.IMAGE_BITMAP, bitmap.GetHbitmap().ToInt32());
        }

        private void RemoveImage() {
            NativeMethods.SendMessage(Handle, NativeMethods.BM_SETIMAGE, NativeMethods.IMAGE_BITMAP, 0);
        }

        private void SetIcon(Icon icon) {
            NativeMethods.SendMessage(Handle, NativeMethods.BM_SETIMAGE, NativeMethods.IMAGE_ICON, icon.ToBitmap().GetHicon().ToInt32());
        }

        private void RemoveIcon() {
            NativeMethods.SendMessage(Handle, NativeMethods.BM_SETIMAGE, NativeMethods.IMAGE_ICON, 0);
        }

        private void SetShield(bool shield) {
            int bin = shield ? 1 : 0;
            NativeMethods.SendMessage(Handle, NativeMethods.BCM_SETSHIELD, 0, bin);
        }

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);

            if (_Icon != null) {
                SetIcon(_Icon);
            } else if (_Image != null) {
                SetImage(_Image);
            } else if (_Shield) {
                SetShield(_Shield);
            }
        }

        #endregion

    }
}
