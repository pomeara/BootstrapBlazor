// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components.Utils;

/// <summary>
/// Catalog freshness math — pure static, no IO: is a model "new" (first seen
/// within the configured window) and a compact age label. Consumed by the
/// <see cref="NewBadge"/> atom and selector/catalog render paths; unit-testable without Blazor.
/// </summary>
public static class ModelFreshness
{
    /// <summary>
    /// True when <paramref name="firstSeenUtc"/> falls within
    /// <see cref="ModelFreshnessOptions.NewWindowDays"/> of <paramref name="now"/>. A model with
    /// no freshness stamp is never "new" — no data, no claim.
    /// </summary>
    public static bool IsNew(DateTimeOffset? firstSeenUtc, DateTimeOffset? now = null)
    {
        if (firstSeenUtc is null || !ModelFreshnessOptions.ShowBadges)
        {
            return false;
        }

        var age = (now ?? DateTimeOffset.UtcNow) - firstSeenUtc.Value;
        return age >= TimeSpan.Zero && age.TotalDays <= ModelFreshnessOptions.NewWindowDays;
    }

    /// <summary>
    /// Compact age since first-seen: <c>3d</c>, <c>6w</c>, <c>4mo</c>, <c>2y</c>.
    /// Null first-seen returns empty (render nothing).
    /// </summary>
    public static string AgeLabel(DateTimeOffset? firstSeenUtc, DateTimeOffset? now = null)
    {
        if (firstSeenUtc is null)
        {
            return string.Empty;
        }

        var age = (now ?? DateTimeOffset.UtcNow) - firstSeenUtc.Value;
        if (age < TimeSpan.Zero)
        {
            return string.Empty;
        }

        return age.TotalDays switch
        {
            < 1 => "today",
            < 14 => $"{(int)age.TotalDays}d",
            < 60 => $"{(int)(age.TotalDays / 7)}w",
            < 365 => $"{(int)(age.TotalDays / 30.44)}mo",
            _ => $"{(int)(age.TotalDays / 365.25)}y"
        };
    }
}
