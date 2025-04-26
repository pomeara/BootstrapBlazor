// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Serial communication parameters
/// </summary>
public class SerialPortOptions
{
    /// <summary>
    /// Baud rate, default 9600
    /// </summary>
    public int BaudRate { get; set; } = 9600;

    /// <summary>
    /// Data bits 7 or 8, default 8
    /// </summary>
    public int DataBits { get; set; } = 8;

    /// <summary>
    /// Stop bits 1 or 2, default 1
    /// </summary>
    public int StopBits { get; set; } = 1;

    /// <summary>
    /// Parity none, even, odd, default "none" 
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public SerialPortParityType ParityType { get; set; }

    /// <summary>
    /// Read/write buffer, default 255
    /// </summary>
    public int BufferSize { get; set; } = 255;

    /// <summary>
    /// Flow control "none" or "hardware", default "none" 
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public SerialPortFlowControlType FlowControlType { get; set; }
}
