// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// IEntityFrameworkCoreDataService interface
/// </summary>
public interface IEntityFrameworkCoreDataService
{
    /// <summary>
    /// Cancel method, since cloned data is used during editing, common cancel usage requires no code and can save data for next edit
    /// </summary>
    /// <returns></returns>
    Task CancelAsync();

    /// <summary>
    /// Edit method, can populate data without edit UI
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task EditAsync(object model);
}
