// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Statistics card supporting three render modes: colored (Bootstrap card with theme color),
/// plain (icon + value + label row), and custom (ChildContent with an optional label heading).
/// </summary>
public sealed partial class StatCard
{
    /// <summary>
    /// The statistic value to display. Numeric values honor <see cref="Format"/>.
    /// </summary>
    [Parameter]
    public object? Value { get; set; }

    /// <summary>
    /// Label text below (or above, in custom mode) the value
    /// </summary>
    [Parameter]
    public string Label { get; set; } = "Items";

    /// <summary>
    /// Card color theme for colored mode — primary, success, warning, danger, info, secondary. Default "primary".
    /// </summary>
    [Parameter]
    public string Color { get; set; } = "primary";

    /// <summary>
    /// Optional FontAwesome icon class (e.g. "fa-solid fa-server")
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// Secondary text below the label
    /// </summary>
    [Parameter]
    public string? SubText { get; set; }

    /// <summary>
    /// Text displayed before the value (e.g. "$")
    /// </summary>
    [Parameter]
    public string? Prefix { get; set; }

    /// <summary>
    /// Text displayed after the value (e.g. "%", " ms")
    /// </summary>
    [Parameter]
    public string? Suffix { get; set; }

    /// <summary>
    /// Format string for IFormattable values (e.g. "N0", "C2", "P0")
    /// </summary>
    [Parameter]
    public string? Format { get; set; }

    /// <summary>
    /// When true, renders a plain card without colored background
    /// </summary>
    [Parameter]
    public bool Plain { get; set; }

    /// <summary>
    /// When true, the card shows a pointer cursor
    /// </summary>
    [Parameter]
    public bool Clickable { get; set; }

    /// <summary>
    /// Click handler for interactive cards
    /// </summary>
    [Parameter]
    public EventCallback OnClick { get; set; }

    /// <summary>
    /// Additional CSS class applied to the card wrapper
    /// </summary>
    [Parameter]
    public string? CssClass { get; set; }

    /// <summary>
    /// CSS class applied to the value element
    /// </summary>
    [Parameter]
    public string? ValueCssClass { get; set; }

    /// <summary>
    /// Custom body content. When set, overrides the default layout;
    /// Label renders as a heading above it.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string ColorClass => Color?.ToLowerInvariant() switch
    {
        "primary" => "primary",
        "success" => "success",
        "info" => "info",
        "warning" => "warning",
        "danger" => "danger",
        "secondary" => "secondary",
        _ => "primary"
    };

    private string TextClass => ColorClass is "warning" or "secondary"
        ? "dark"
        : "white";

    private string FormattedValue
    {
        get
        {
            if (Value is null) return string.Empty;
            if (!string.IsNullOrEmpty(Format) && Value is IFormattable formattable)
                return formattable.ToString(Format, null);
            return Value.ToString() ?? string.Empty;
        }
    }

    private string ClickCursorStyle => (Clickable || OnClick.HasDelegate) ? "cursor: pointer;" : string.Empty;

    private async Task HandleClick()
    {
        if (OnClick.HasDelegate)
            await OnClick.InvokeAsync();
    }
}
