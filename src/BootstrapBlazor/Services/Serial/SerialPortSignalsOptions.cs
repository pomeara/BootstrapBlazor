// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Serial port signal settings
/// </summary>
public class SerialPortSignalsOptions
{
    /// <summary>
    /// Break
    /// <para></para>If Break is true, it means interrupted. If Break is false, it means not interrupted.
    /// </summary>
    [JsonPropertyName("break")]
    public bool Break { get; set; }

    /// <summary>
    /// Data Terminal Ready (DTR)
    /// <para></para>If DTR is true, ready to receive data. If DTR is false, not ready to receive data. Pin 4
    /// </summary>
    [JsonPropertyName("dataTerminalReady")]
    public bool DTR { get; set; }

    /// <summary>
    /// Request To Send (RTS)
    /// <para></para>If RTS is true, ready to send data. If RTS is false, not ready to send data. Pin 7
    /// </summary>
    [JsonPropertyName("requestToSend")]
    public bool RTS { get; set; }
}
