// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

class DefaultCalendarFestivals : ICalendarFestivals
{
    private static readonly Dictionary<string, string> Festivals = new() {
        {"0101", "New Year's Day"},
        {"0214", "Valentine's Day"},
        {"0308", "Women's Day"},
        {"0312", "Arbor Day"},
        {"0401", "April Fools' Day"},
        {"0501", "Labor Day"},
        {"0504", "Youth Day"},
        {"0601", "Children's Day"},
        {"0701", "Party Founding Day"},
        {"0801", "Army Day"},
        {"1001", "National Day"},
        {"1225", "Christmas"}
    };

    private static readonly Dictionary<string, string> LunarFestivals = new() {
        {"0101", "Spring Festival"},
        {"0115", "Lantern Festival"},
        {"0505", "Dragon Boat Festival"},
        {"0815", "Mid-Autumn Festival"},
        {"0909", "Double Ninth Festival"},
        {"1208", "Laba Festival"},
        {"1230", "New Year's Eve"},
    };

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public string? GetFestival(DateTime dt)
    {
        string? ret = null;
        var key = $"{dt:MMdd}";
        var (_, Month, Day) = dt.ToLunarDateTime();
        var lunarKey = $"{Month:00}{Day:00}";
        if (LunarFestivals.TryGetValue(lunarKey, out var v1))
        {
            ret = v1;
        }
        else if (Festivals.TryGetValue(key, out var v2))
        {
            ret = v2;
        }
        return ret;
    }
}
