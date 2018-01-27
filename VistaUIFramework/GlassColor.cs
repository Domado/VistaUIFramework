using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MyAPKapp.VistaUIFramework {
    public struct GlassColor {
        internal GlassColor(Color Color, bool Blend) {
            this.Color = Color;
            this.Blend = Blend;
        }

        /// <summary>
        /// The Aero glass color (includes alpha channel)
        /// </summary>
        public Color Color { get; }

        /// <summary>
        /// Returns if glass is opaque or transparent
        /// </summary>
        public bool Blend { get; }
    }
}
