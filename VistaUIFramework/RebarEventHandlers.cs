//--------------------------------------------------------------------
// <copyright file="RebarEventHandlers.cs" company="myapkapp">
//     Copyright (c) myapkapp. All rights reserved.
// </copyright>                                                                
//--------------------------------------------------------------------
// This open-source project is licensed under Apache License 2.0
//--------------------------------------------------------------------

namespace MyAPKapp.VistaUIFramework {

    /// <summary>
    /// Represents the method that handles any <see cref="RebarBand"/>-related event
    /// </summary>
    /// <param name="sender">The <see cref="object"/> that calls the event</param>
    /// <param name="e">A <see cref="BandEventArgs"/> that contains the event data</param>
    public delegate void BandEventHandler(object sender, BandEventArgs e);

    /// <summary>
    /// Represents the method that handles any <see cref="RebarBand"/>-related cancellable event
    /// </summary>
    /// <param name="sender">The <see cref="object"/> that calls the event</param>
    /// <param name="e">A <see cref="BandCancelEventArgs"/> that contains the event data</param>
    public delegate void BandCancelEventHandler(object sender, BandCancelEventArgs e);

    /// <summary>
    /// Represents the method that handles an <see cref="Rebar.AutoBreak"/> event
    /// </summary>
    /// <param name="sender">The <see cref="object"/> that calls the event</param>
    /// <param name="e">An <see cref="AutoBreakEventArgs"/> that contains the event data</param>
    public delegate void AutoBreakEventHandler(object sender, AutoBreakEventArgs e);

    /// <summary>
    /// Represents the method that handles a <see cref="Rebar.BandAutoSize"/> event
    /// </summary>
    /// <param name="sender">The <see cref="object"/> that calls the event</param>
    /// <param name="e">A <see cref="BandAutoSizeEventArgs"/> that contains the event data</param>
    public delegate void BandAutoSizeEventHandler(object sender, BandAutoSizeEventArgs e);

    /// <summary>
    /// Represents the method that handles a <see cref="Rebar.ChevronPushed"/> event
    /// </summary>
    /// <param name="sender">The <see cref="object"/> that calls the event</param>
    /// <param name="e">A <see cref="ChevronPushedEventArgs"/> that contains the event data</param>
    public delegate void ChevronPushedEventHandler(object sender, ChevronPushedEventArgs e);

    /// <summary>
    /// Represents the method that handles a <see cref="Rebar.ChildSizeChanged"/> event
    /// </summary>
    /// <param name="sender">The <see cref="object"/> that calls the event</param>
    /// <param name="e">A <see cref="ChildSizeChangedEventArgs"/> that contains the event data</param>
    public delegate void ChildSizeChangedEventHandler(object sender, ChildSizeChangedEventArgs e);

    /// <summary>
    /// Represents the method that handles a <see cref="Rebar.SplitterDrag"/> event
    /// </summary>
    /// <param name="sender">The <see cref="object"/> that calls the event</param>
    /// <param name="e">A <see cref="SplitterDragEventHandler"/> that contains the event data</param>
    public delegate void SplitterDragEventHandler(object sender, SplitterDragEventArgs e);

}