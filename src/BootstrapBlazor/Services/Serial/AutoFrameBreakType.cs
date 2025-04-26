// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Auto frame break method
/// </summary>
public enum AutoFrameBreakType
{
    /// <summary>
    /// Auto frame break not enabled
    /// </summary>
    None,

    /// <summary>
    /// Character frame break
    /// </summary>
    Character,

    /// <summary>
    /// Idle interrupt (not implemented)
    /// </summary>
    Timeout,

    /// <summary>
    /// Frame header and footer (not implemented)
    /// <para></para>Example: Frame header (AA, BB) + data length + data + CRC check + frame footer (CC, DD)
    /// </summary>
    FrameTail,

    /// <summary>
    /// Character interval (not implemented)
    /// </summary>
    CharacterInterval,
}
