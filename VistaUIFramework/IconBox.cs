//--------------------------------------------------------------------
// <copyright file="IconBox.cs" company="MyAPKapp">
//     Copyright (c) MyAPKapp. All rights reserved.
// </copyright>                                                                
//--------------------------------------------------------------------
// This open-source project is licensed under Apache License 2.0
//--------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyAPKapp.VistaUIFramework {

    /// <summary>
    /// The <see cref="IconBox"/> is like the <see cref="IconBox"/>, but it displays icons from .ico, .exe and .dll
    /// </summary>
    [ToolboxBitmap(typeof(PictureBox))]
    [Description("The IconBox is like the PictureBox, but it works with Windows icons")]
    public class IconBox : PictureBox {

        private string icon;

        /// <summary>
        /// Initializes an instance of <see cref="IconBox"/>
        /// </summary>
        public IconBox() : base() {}

        #region Public properties

        public string Icon {
            get {
                return icon;
            }
            set {
                if (icon != value) {
                    icon = value;
                }
            }
        }

        #endregion

        #region Hidden properties

        /// <summary>
        /// This isn't a <see cref="PictureBox"/>, use Icon instead
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [Obsolete("This isn't a PictureBox, use Icon instead")]
        public new Image Image {
            get { return base.Image; }
            set { base.Image = value; }
        }

        #endregion

    }
}