// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Drawer footer row — right-aligned action area.
/// </summary>
public sealed partial class DrawerFooter
{
    /// <summary>
    /// Gets or sets the footer content (typically action buttons).
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string? ClassString => CssBuilder.Default("drawer-footer")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}
