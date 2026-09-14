// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Centered loading indicator wrapping a Bootstrap spinner with an optional message.
/// </summary>
public sealed partial class LoadingState
{
    /// <summary>
    /// Loading message displayed below the spinner. Default "Loading..."
    /// </summary>
    [Parameter]
    public string Message { get; set; } = "Loading...";

    /// <summary>
    /// Spinner color theme (primary, success, warning, danger, info, secondary). Default "primary".
    /// </summary>
    [Parameter]
    public string SpinnerColor { get; set; } = "primary";

    private string? ClassString => CssBuilder.Default("loading-state-wrap")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}
