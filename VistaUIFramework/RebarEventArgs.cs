//--------------------------------------------------------------------
// <copyright file="RebarEventArgs.cs" company="MyAPKapp">
//     Copyright (c) MyAPKapp. All rights reserved.
// </copyright>                                                                
//--------------------------------------------------------------------
// This open-source project is licensed under Apache License 2.0
//--------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Drawing;

namespace MyAPKapp.VistaUIFramework {

    /// <summary>
    /// Provides data for any <see cref="RebarBand"/>-related events
    /// </summary>
    public class BandEventArgs : EventArgs {

        /// <summary>
        /// Initializes a new instance of the <see cref="BandEventArgs"/> class
        /// </summary>
        /// <param name="Band">The <see cref="RebarBand"/> that called the event</param>
        public BandEventArgs(RebarBand Band) : base() {
            this.Band = Band;
        }

        /// <summary>
        /// Gets the <see cref="RebarBand"/> associated to the event
        /// </summary>
        public RebarBand Band { get; }

    }

    /// <summary>
    /// Provides data for any <see cref="RebarBand"/>-related cancellable events
    /// </summary>
    public class BandCancelEventArgs : CancelEventArgs {

        /// <summary>
        /// Initializes a new instance of the <see cref="BandCancelEventArgs"/> class
        /// </summary>
        /// <param name="Band"></param>
        public BandCancelEventArgs(RebarBand Band) : base() {
            this.Band = Band;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BandCancelEventArgs"/> class
        /// </summary>
        /// <param name="Band">The <see cref="RebarBand"/> that called the event</param>
        /// <param name="Cancel">true to cancel the event; otherwise, false</param>
        public BandCancelEventArgs(RebarBand Band, bool Cancel) : base(Cancel) {
            this.Band = Band;
        }

        /// <summary>
        /// Gets the <see cref="RebarBand"/> associated to the event
        /// </summary>
        public RebarBand Band { get; }

    }

    /// <summary>
    /// Provides data for the <see cref="Rebar.AutoBreak"/> event
    /// </summary>
    public class AutoBreakEventArgs : BandEventArgs {

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoBreakEventArgs"/> class
        /// </summary>
        /// <param name="Band">The <see cref="RebarBand"/> that called the event</param>
        /// <param name="AutoBreak">Should a break occur?</param>
        public AutoBreakEventArgs(RebarBand Band, bool AutoBreak) : base(Band) {
            this.AutoBreak = AutoBreak;
        }

        /// <summary>
        /// Gets if a break should occur
        /// </summary>
        public bool AutoBreak { get; }

    }

    /// <summary>
    /// Provides data for the <see cref="Rebar.BandAutoSize"/> event
    /// </summary>
    public class BandAutoSizeEventArgs : EventArgs {
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BandAutoSizeEventArgs"/> class
        /// </summary>
        /// <param name="Changed">The size changed?</param>
        /// <param name="Actual">The actual <see cref="Rebar"/></param>
        /// <param name="Target"></param>
        public BandAutoSizeEventArgs(bool Changed, Size Actual, Size Target) : base() {
            this.Changed = Changed;
            this.Actual = Actual;
            this.Target = Target;
        }

        /// <summary>
        /// Gets if size was changed
        /// </summary>
        public bool Changed { get; }

        /// <summary>
        /// Gets the actual <see cref="Rebar"/> size
        /// </summary>
        public Size Actual { get; }

        /// <summary>
        /// Gets the size <see cref="Rebar"/> tries to resize itself
        /// </summary>
        public Size Target { get; }

    }

    /// <summary>
    /// Provides data for the <see cref="Rebar.ChevronPushed"/> event
    /// </summary>
    public class ChevronPushedEventArgs : BandEventArgs {

        /// <summary>
        /// Initializes a new instance of the <see cref="ChevronPushedEventArgs"/> class
        /// </summary>
        /// <param name="Band">The <see cref="RebarBand"/> that called the event</param>
        /// <param name="Area">The area covered by the chevron</param>
        public ChevronPushedEventArgs(RebarBand Band, Rectangle Area) : base(Band) {
            this.Area = Area;
        }

        /// <summary>
        /// Gets the area covered by the chevron
        /// </summary>
        public Rectangle Area { get; }

    }

    /// <summary>
    /// Provides data for the <see cref="Rebar.ChildSizeChanged"/> event
    /// </summary>
    public class ChildSizeChangedEventArgs : BandEventArgs {

        /// <summary>
        /// Initializes a new instance of the <see cref="ChildSizeChangedEventArgs"/> class
        /// </summary>
        /// <param name="Band">The <see cref="RebarBand"/> that called the event</param>
        /// <param name="ChildSize">The <see cref="Size"/> of the <see cref="RebarBand"/>'s child</param>
        /// <param name="BandSize">The <see cref="Size"/> of the <see cref="RebarBand"/></param>
        public ChildSizeChangedEventArgs(RebarBand Band, Size ChildSize, Size BandSize) : base(Band) {

        }

        /// <summary>
        /// Gets the size of the <see cref="RebarBand"/>'s child
        /// </summary>
        public Size ChildSize { get; }

        /// <summary>
        /// Gets the size of the <see cref="RebarBand"/>
        /// </summary>
        public Size BandSize { get; }

    }

    /// <summary>
    /// Provides data for the <see cref="Rebar.SplitterDrag"/> event
    /// </summary>
    public class SplitterDragEventArgs : EventArgs {

        /// <summary>
        /// Initializes a new instance of the <see cref="SplitterDragEventArgs"/> class
        /// </summary>
        /// <param name="Size">The <see cref="Size"/> of the <see cref="Rebar"/></param>
        public SplitterDragEventArgs(Size RebarSize) : base() {
            this.RebarSize = RebarSize;
        }

        /// <summary>
        /// Gets the size of the <see cref="Rebar"/>
        /// </summary>
        public Size RebarSize { get; }

    }

}