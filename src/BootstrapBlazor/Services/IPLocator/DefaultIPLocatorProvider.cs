// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Caching.Memory;

namespace BootstrapBlazor.Components;

/// <summary>
/// Default IP geolocation locator
/// </summary>
public abstract class DefaultIpLocatorProvider : IIpLocatorProvider
{
    /// <summary>
    /// Gets the IP location result cache
    /// </summary>
    protected MemoryCache IpCache { get; } = new(new MemoryCacheOptions());

    /// <summary>
    /// Gets the IpLocator configuration information
    /// </summary>
    protected IpLocatorOptions Options { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    protected DefaultIpLocatorProvider(IOptions<BootstrapBlazorOptions> options)
    {
        Options = options.Value.IpLocatorOptions;
        Key = GetType().Name;
    }

    /// <summary>
    /// Gets the list of localhost addresses
    /// </summary>
    private readonly List<string> _localhostList = [.. new[] { "::1", "127.0.0.1" }];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? Key { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public async Task<string?> Locate(string? ip)
    {
        string? ret = null;

        // Resolve localhost address
        if (string.IsNullOrEmpty(ip) || _localhostList.Any(p => p == ip))
        {
            ret = "localhost";
        }
        else if (Options.EnableCache)
        {
            if (IpCache.TryGetValue(ip, out var v) && v is string city && !string.IsNullOrEmpty(city))
            {
                ret = city;
            }
            else
            {
                ret = await LocateByIp(ip);
                if (!string.IsNullOrEmpty(ret))
                {
                    using var entry = IpCache.CreateEntry(ip);
                    entry.Value = ret;
                    entry.SetSlidingExpiration(Options.SlidingExpiration);
                }
            }
        }
        else
        {
            ret = await LocateByIp(ip);
        }
        return ret;
    }

    /// <summary>
    /// Internal location method
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    protected abstract Task<string?> LocateByIp(string ip);
}
