// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// ISerialPort interface
/// </summary>
public interface ISerialPort : IAsyncDisposable
{
    /// <summary>
    /// Gets whether the port is open
    /// </summary>
    bool IsOpen { get; }

    /// <summary>
    /// Closes the port
    /// </summary>
    /// <returns></returns>
    Task<bool> Close(CancellationToken token = default);

    /// <summary>
    /// Opens the port
    /// </summary>
    /// <returns></returns>
    Task<bool> Open(SerialPortOptions options, CancellationToken token = default);

    /// <summary>
    /// Data receive callback method
    /// </summary>
    /// <returns></returns>
    Func<byte[], Task>? DataReceive { get; set; }

    /// <summary>
    /// Writes data
    /// </summary>
    /// <returns></returns>
    Task<bool> Write(byte[] data, CancellationToken token = default);

    /// <summary>
    /// Gets USB device information
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<SerialPortUsbInfo?> GetUsbInfo(CancellationToken token = default);

    /// <summary>
    /// Gets device parameters
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<SerialPortSignals?> GetSignals(CancellationToken token = default);

    /// <summary>
    /// Sets device parameters
    /// </summary>
    /// <param name="options"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<bool> SetSignals(SerialPortSignalsOptions options, CancellationToken token = default);
}
