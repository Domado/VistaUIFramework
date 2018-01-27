using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MyAPKapp.VistaUIFramework {
    public class AeroColorChangedEventArgs : EventArgs {

        /// <summary>
        /// Initializes the <code>EventArgs</code>
        /// </summary>
        /// <param name="Colorization">The Aero glass colro</param>
        /// <param name="Blend">Whether glass is opaque or transparent</param>
        public AeroColorChangedEventArgs(Color Colorization, bool Blend) : base() {
            this.Colorization = Colorization;
            this.Blend = Blend;
        }

        /// <summary>
        /// Get the Aero glass color
        /// </summary>
        public Color Colorization { get; }

        /// <summary>
        /// Get if Aero glass is opaque or transparent
        /// </summary>
        public bool Blend { get; }

    }
}
