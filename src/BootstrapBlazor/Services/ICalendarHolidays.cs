// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Holiday interface
/// </summary>
public interface ICalendarHolidays
{
    /// <summary>
    /// Whether it's a holiday
    /// </summary>
    /// <returns></returns>
    bool IsHoliday(DateTime dt);

    /// <summary>
    /// Whether it's a workday
    /// </summary>
    /// <returns></returns>
    bool IsWorkday(DateTime dt);
}
