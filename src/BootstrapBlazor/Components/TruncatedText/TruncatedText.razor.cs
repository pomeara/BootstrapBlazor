// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Renders a value truncated to <see cref="MaxLength"/> characters with an ellipsis,
/// exposing the full value through the title tooltip.
/// </summary>
public sealed partial class TruncatedText
{
    /// <summary>
    /// The value to display
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    /// <summary>
    /// Maximum number of characters before truncation. Default 16.
    /// </summary>
    [Parameter]
    public int MaxLength { get; set; } = 16;

    private string DisplayText
    {
        get
        {
            if (string.IsNullOrEmpty(Value) || MaxLength < 1 || Value.Length <= MaxLength)
            {
                return Value ?? string.Empty;
            }
            return Value[..(MaxLength - 1)] + "…";
        }
    }
}
