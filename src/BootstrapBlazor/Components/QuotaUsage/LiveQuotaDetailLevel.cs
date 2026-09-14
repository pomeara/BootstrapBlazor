// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// How much of a provider's live-quota card is rendered. One renderer, three tiers:
/// <list type="bullet">
/// <item><see cref="Compact"/> — header strip only (icon, provider, headline %). Chrome/footer sized.</item>
/// <item><see cref="Standard"/> — compact + the quota window bars. Default.</item>
/// <item><see cref="Detailed"/> — standard + extended footer space. Full dashboard card.</item>
/// </list>
/// </summary>
public enum LiveQuotaDetailLevel
{
    /// <summary>Header strip only — chrome/footer sized.</summary>
    Compact,

    /// <summary>Compact + the quota window bars. Default.</summary>
    Standard,

    /// <summary>Standard + extended footer space — full dashboard card.</summary>
    Detailed
}
