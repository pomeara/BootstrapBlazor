// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace BootstrapBlazor.Components;

/// <summary>
/// Baidu search engine IP locator
/// </summary>
public class BaiduIpLocatorProvider(IHttpClientFactory httpClientFactory, IOptions<BootstrapBlazorOptions> options, ILogger<BaiduIpLocatorProvider> logger) : DefaultIpLocatorProvider(options)
{
    private HttpClient? _client;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="ip"></param>
    protected override async Task<string?> LocateByIp(string ip)
    {
        string? ret = null;
        var url = GetUrl(ip);
        try
        {
            _client ??= GetHttpClient();
            using var token = new CancellationTokenSource(3000);
            ret = await Fetch(url, _client, token.Token);
        }
        catch (TaskCanceledException) { }
        catch (Exception ex)
        {
            logger.LogError(ex, "Url: {url}", url);
        }
        return ret;
    }

    /// <summary>
    /// Get HttpClient instance method
    /// </summary>
    /// <returns></returns>
    protected virtual HttpClient GetHttpClient() => httpClientFactory.CreateClient();

    /// <summary>
    /// Get URL address
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    protected virtual string GetUrl(string ip) => $"https://sp0.baidu.com/8aQDcjqpAAV3otqbppnN2DJv/api.php?resource_id=6006&query={ip}";

    /// <summary>
    /// Request to get geographic location interface method
    /// </summary>
    /// <param name="url"></param>
    /// <param name="client"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    protected virtual async Task<string?> Fetch(string url, HttpClient client, CancellationToken token)
    {
        var result = await client.GetFromJsonAsync<LocationResult>(url, token);
        return result?.ToString();
    }

    /// <summary>
    /// LocationResult 结构体
    /// </summary>
    [ExcludeFromCodeCoverage]
    class LocationResult
    {
        /// <summary>
        /// Get/Set result status return code, communication is normal when 0
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Get/Set location information
        /// </summary>
        public List<LocationData>? Data { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override string? ToString()
        {
            string? ret = null;
            if (Status == "0")
            {
                ret = Data?.FirstOrDefault()?.Location;
            }
            return ret;
        }
    }

    [ExcludeFromCodeCoverage]
    class LocationData
    {
        /// <summary>
        /// Get/Set location information
        /// </summary>
        public string? Location { get; set; }
    }
}
