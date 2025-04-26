// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Caching.Memory;

namespace BootstrapBlazor.Components;

/// <summary>
/// Browser notification service
/// </summary>
public class NotificationService : IAsyncDisposable
{
    private IJSRuntime JSRuntime { get; }

    private JSModule? Module { get; set; }

    private DotNetObjectReference<NotificationService> Interop { get; }

    private ICacheManager Cache { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="runtime"></param>
    /// <param name="cache"></param>
    public NotificationService(IJSRuntime runtime, ICacheManager cache)
    {
        JSRuntime = runtime;
        Cache = cache;
        Interop = DotNetObjectReference.Create(this);
    }

    private Task<JSModule> LoadModule() => JSRuntime.LoadModuleByName("noti");

    /// <summary>
    /// Check browser notification permission status
    /// </summary>
    /// <param name="requestPermission">Whether to request permission, default is true</param>
    /// <returns></returns>
    public async ValueTask<bool> CheckPermission(bool requestPermission = true)
    {
        Module ??= await LoadModule();
        return await Module.InvokeAsync<bool>("check", requestPermission);
    }

    /// <summary>
    /// Send browser notification
    /// </summary>
    /// <param name="item">NotificationItem instance</param>
    /// <returns></returns>
    public async Task<bool> Dispatch(NotificationItem item)
    {
        Module ??= await LoadModule();
        item.Id ??= $"noti_item_{item.GetHashCode()}";
        Cache.GetOrCreate<NotificationItem>(item.Id, entry =>
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
            return item;
        });
        return await Module.InvokeAsync<bool>("notify", Interop, nameof(DispatchCallback), item);
    }

    /// <summary>
    /// Message notification callback method triggered by JS click
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task DispatchCallback(string id)
    {
        if (Cache.TryGetValue(id, out NotificationItem? val))
        {
            Cache.Clear(id);

            if (val.OnClick != null)
            {
                await val.OnClick();
            }
        }
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
            Interop.Dispose();

            // Dispose JSModule
            if (Module != null)
            {
                await Module.DisposeAsync();
                Module = null;
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
