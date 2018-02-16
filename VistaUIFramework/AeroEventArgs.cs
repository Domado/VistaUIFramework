//--------------------------------------------------------------------
// <copyright file="AeroEventArgs.cs" company="myapkapp">
//     Copyright (c) myapkapp. All rights reserved.
// </copyright>                                                                
//--------------------------------------------------------------------
// This open-source project is licensed under Apache License 2.0
//--------------------------------------------------------------------

using System;
using System.Drawing;

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

    public class AeroCompChangedEventArgs : EventArgs {

        public AeroCompChangedEventArgs(bool CompositionEnabled) : base() {
            this.CompositionEnabled = CompositionEnabled;
        }

        /// <summary>
        /// Get if DWM composition was enabled or disabled
        /// </summary>
        public bool CompositionEnabled { get; }

    }
}