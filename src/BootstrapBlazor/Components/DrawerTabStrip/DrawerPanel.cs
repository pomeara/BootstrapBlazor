// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// A single content panel within a drawer. Multiple panels are shown as tabs.
/// </summary>
/// <param name="Key">Unique panel identifier.</param>
/// <param name="Title">Tab display title.</param>
/// <param name="Icon">Optional FontAwesome icon class.</param>
/// <param name="Content">Optional panel content.</param>
public sealed record DrawerPanel(string Key, string Title, string? Icon = null, RenderFragment? Content = null);
