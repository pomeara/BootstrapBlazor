// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// USB serial port device information class
/// </summary>
public class SerialPortUsbInfo
{
    /// <summary>
    /// Vendor ID
    /// </summary>
    public string? UsbVendorId { get; set; }

    /// <summary>
    /// Product ID
    /// </summary>
    public string? UsbProductId { get; set; }
}
