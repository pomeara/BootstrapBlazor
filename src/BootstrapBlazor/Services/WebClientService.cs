// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// WebClient service class
/// </summary>
/// <param name="ipLocatorFactory"></param>
/// <param name="options"></param>
/// <param name="runtime"></param>
/// <param name="navigation"></param>
/// <param name="logger"></param>
public class WebClientService(IIpLocatorFactory ipLocatorFactory,
    IOptionsMonitor<BootstrapBlazorOptions> options,
    IJSRuntime runtime,
    NavigationManager navigation,
    ILogger<WebClientService> logger) : IAsyncDisposable
{
    private TaskCompletionSource? _taskCompletionSource;
    private JSModule? _jsModule;
    private DotNetObjectReference<WebClientService>? _interop;
    private ClientInfo? _client;
    private IIpLocatorProvider? _provider;

    /// <summary>
    /// Get ClientInfo instance method
    /// </summary>
    /// <returns></returns>
    public async Task<ClientInfo> GetClientInfo()
    {
        _taskCompletionSource = new TaskCompletionSource();
        _client = new ClientInfo()
        {
            RequestUrl = navigation.Uri
        };

        try
        {
            _jsModule ??= await runtime.LoadModuleByName("client");
            if (_jsModule != null)
            {
                _interop ??= DotNetObjectReference.Create(this);
                await _jsModule.InvokeVoidAsync("ping", "ip.axd", _interop, nameof(SetData));
                // Wait for SetData method to complete
                await _taskCompletionSource.Task.WaitAsync(TimeSpan.FromSeconds(3));
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{GetClientInfo} throw exception", nameof(GetClientInfo));
        }

        // Supplement IP address information
        if (options.CurrentValue.WebClientOptions.EnableIpLocator && string.IsNullOrEmpty(_client.City))
        {
            _provider ??= ipLocatorFactory.Create(options.CurrentValue.IpLocatorOptions.ProviderName);
            _client.City = await _provider.Locate(_client.Ip);
        }
        return _client;
    }

    /// <summary>
    /// SetData method called by JS
    /// </summary>
    /// <param name="client"></param>
    [JSInvokable]
    public void SetData(ClientInfo client)
    {
        _client = client;
        _client.RequestUrl = navigation.Uri;
        _taskCompletionSource?.TrySetResult();
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.
    /// </summary>
    /// <param name="disposing"></param>
    /// <returns></returns>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            // Dispose DotNetObjectReference instance
            _interop?.Dispose();

            // Dispose JSModule
            if (_jsModule != null)
            {
                await _jsModule.DisposeAsync();
                _jsModule = null;
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}

/// <summary>
/// 客户端请求信息实体类
/// </summary>
public class ClientInfo
{
    /// <summary>
    /// Gets/Sets the connection Id
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Gets/Sets the client IP
    /// </summary>
    public string? Ip { get; set; }

    /// <summary>
    /// Gets/Sets the client location
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Gets/Sets the client browser
    /// </summary>
    public string? Browser { get; set; }

    /// <summary>
    /// Gets/Sets the client operating system
    /// </summary>
    public string? OS { get; set; }

    /// <summary>
    /// Gets/Sets the client device type
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public WebClientDeviceType Device { get; set; }

    /// <summary>
    /// Gets/Sets the client browser语言
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// Gets/Sets the request URL
    /// </summary>
    public string? RequestUrl { get; set; }

    /// <summary>
    /// Gets/Sets the client UserAgent
    /// </summary>
    public string? UserAgent { get; set; }

    /// <summary>
    /// Gets/Sets the browser engine information
    /// </summary>
    public string? Engine { get; set; }
}
