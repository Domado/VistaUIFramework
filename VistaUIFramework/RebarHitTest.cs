//--------------------------------------------------------------------
// <copyright file="RebarHitTest.cs" company="myapkapp">
//     Copyright (c) myapkapp. All rights reserved.
// </copyright>                                                                
//--------------------------------------------------------------------
// This open-source project is licensed under Apache License 2.0
//--------------------------------------------------------------------

namespace MyAPKapp.VistaUIFramework {
    public enum RebarHitTest : uint {
        NoWhere = 0x0001,
        Caption = 0x0002,
        Client = 0x0003,
        Grabber = 0x0004,
        Chevron = 0x0008,
        Splitter = 0x0010
    }
}
