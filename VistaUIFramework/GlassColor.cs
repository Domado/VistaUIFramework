//--------------------------------------------------------------------
// <copyright file="GlassColor.cs" company="myapkapp">
//     Copyright (c) myapkapp. All rights reserved.
// </copyright>                                                                
//--------------------------------------------------------------------
// This open-source project is licensed under Apache License 2.0
//--------------------------------------------------------------------

using System.Drawing;

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
