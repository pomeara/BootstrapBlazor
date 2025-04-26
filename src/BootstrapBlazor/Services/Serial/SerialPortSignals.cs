// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

//RS-232C interface definition (DB9)
//Pin definition symbols
//1 Data Carrier Detect (DCD)
//2 Received Data (RXD)
//3 Transmit Data (TXD)
//4 Data Terminal Ready (DTR)
//5 Signal Ground (SG)
//6 Data Set Ready (DSR)
//7 Request To Send (RTS)
//8 Clear To Send (CTS)
//9 Ring Indicator (RI)

/// <summary>
/// Serial port signals
/// </summary>
public class SerialPortSignals
{
    /// <summary>
    /// Ring Indicator (RI)
    /// If RI is true, a ring has been detected. If RI is false, no ring has been detected. Pin 9
    /// </summary>
    [JsonPropertyName("ringIndicator")]
    public bool RING { get; set; }

    /// <summary>
    /// Data Set Ready (DSR)
    /// If DSR is true, it's ready to receive data. If DSR is false, it's not ready to receive data. Pin 6
    /// </summary>
    [JsonPropertyName("dataSetReady")]
    public bool DSR { get; set; }

    /// <summary>
    /// Clear To Send (CTS)
    /// If CTS is true, it's ready to send data. If CTS is false, it's not ready to send data. Pin 8
    /// </summary>
    [JsonPropertyName("clearToSend")]
    public bool CTS { get; set; }

    /// <summary>
    /// Data Carrier Detect (DCD)
    /// If DCD is true, a carrier has been detected. If DCD is false, no carrier has been detected. Pin 1
    /// </summary>
    [JsonPropertyName("dataCarrierDetect")]
    public bool DCD { get; set; }
}
