// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Grid of provider status tiles — name, online/offline badge and model count; raises
/// <see cref="OnProviderSelected"/> when a tile is clicked.
/// </summary>
public sealed partial class ProviderStatusGrid
{
    /// <summary>Provider tiles to render.</summary>
    [Parameter]
    public IReadOnlyList<ProviderStatusItem> ProviderStatuses { get; set; } = [];

    /// <summary>Raised with the clicked provider's <see cref="ProviderStatusItem.ProviderType"/>.</summary>
    [Parameter]
    public EventCallback<string> OnProviderSelected { get; set; }
}
