using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPKapp.VistaUIFramework {
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
