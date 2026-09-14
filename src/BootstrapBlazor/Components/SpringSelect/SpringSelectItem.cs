// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// One selectable row of a <see cref="SpringSelect"/>. <see cref="Data"/> is a free
/// payload slot for consumers that render an <c>ItemTemplate</c> — the select itself never
/// reads it; the template casts it back to the consumer's own model.
/// </summary>
/// <param name="Value">Selection value.</param>
/// <param name="Label">Display label for the default rendering.</param>
/// <param name="Icon">Optional FontAwesome icon name (without the fa-solid prefix).</param>
/// <param name="Data">Free payload slot for template-rendering consumers.</param>
public sealed record SpringSelectItem(string Value, string Label, string? Icon = null, object? Data = null);
