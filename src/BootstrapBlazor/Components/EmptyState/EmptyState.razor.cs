// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Renders a centered empty-state placeholder with icon, title, description and an optional action.
/// </summary>
public sealed partial class EmptyState
{
    /// <summary>
    /// FontAwesome icon class or a short name resolved to an icon
    /// ("project", "session", "artifact", "search", "error"). Default "fa-solid fa-inbox fa-2x".
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// Title text. Default "No Data".
    /// </summary>
    [Parameter]
    public string Title { get; set; } = "No Data";

    /// <summary>
    /// Description text (alias for Message — used when Message is not set)
    /// </summary>
    [Parameter]
    public string Description { get; set; } = "There are no items to display.";

    /// <summary>
    /// Message text. Takes precedence over Description when set.
    /// </summary>
    [Parameter]
    public string? Message { get; set; }

    /// <summary>
    /// Text of the built-in action button. Rendered only when OnAction has a handler.
    /// </summary>
    [Parameter]
    public string? ActionText { get; set; }

    /// <summary>
    /// Handler invoked by the built-in action button
    /// </summary>
    [Parameter]
    public EventCallback OnAction { get; set; }

    /// <summary>
    /// Custom action content rendered below the message (e.g. a button group)
    /// </summary>
    [Parameter]
    public RenderFragment? Actions { get; set; }

    private string DisplayMessage => !string.IsNullOrEmpty(Message) ? Message : Description;

    private string ResolvedIcon => Icon?.Contains(' ') == true
        ? Icon
        : (Icon ?? "default").ToLowerInvariant() switch
        {
            "project" => "fa-solid fa-folder-open fa-2x",
            "session" => "fa-solid fa-comments fa-2x",
            "artifact" => "fa-solid fa-file fa-2x",
            "search" => "fa-solid fa-magnifying-glass fa-2x",
            "error" => "fa-solid fa-triangle-exclamation fa-2x",
            "default" => "fa-solid fa-inbox fa-2x",
            _ => $"fa-solid fa-{Icon} fa-2x"
        };

    private async Task HandleAction()
    {
        if (OnAction.HasDelegate)
        {
            await OnAction.InvokeAsync();
        }
    }
}
