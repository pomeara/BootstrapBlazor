// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Caching.Memory;

namespace BootstrapBlazor.Components;

/// <summary>
/// CacheManager 接口类
/// </summary>
public interface ICacheManager
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="key"></param>
    /// <param name="factory"></param>
    /// <returns></returns>
    TItem GetOrCreate<TItem>(object key, Func<ICacheEntry, TItem> factory);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="key"></param>
    /// <param name="factory"></param>
    /// <returns></returns>
    Task<TItem> GetOrCreateAsync<TItem>(object key, Func<ICacheEntry, Task<TItem>> factory);

    /// <summary>
    /// Gets the value associated with the specified key
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    bool TryGetValue<TItem>(object key, [NotNullWhen(true)] out TItem? value);

    /// <summary>
    /// Sets the application start time
    /// </summary>
    void SetStartTime();

    /// <summary>
    /// Gets the application start time
    /// </summary>
    /// <returns></returns>
    DateTimeOffset GetStartTime();

    /// <summary>
    /// Clears cache by specified key
    /// </summary>
    /// <param name="key"></param>
    void Clear(object? key = null);

    /// <summary>
    /// Gets the number of cache entries
    /// </summary>
    long Count { get; }

#if NET9_0_OR_GREATER
    /// <summary>
    /// Gets the collection of cache keys
    /// </summary>
    IEnumerable<object> Keys { get; }

    /// <summary>
    /// Gets the cache entry instance by specified key
    /// </summary>
    /// <param name="key"></param>
    /// <param name="entry"></param>
    /// <returns></returns>
    bool TryGetCacheEntry(object? key, [NotNullWhen(true)] out ICacheEntry? entry);
#endif
}
