// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// "New" freshness chip — shows when the model first appeared within the configured window;
/// tooltip carries the compact age. Renders nothing otherwise.
/// </summary>
public sealed partial class NewBadge
{
    /// <summary>
    /// Gets or sets the UTC timestamp when the model was first seen. Null renders nothing.
    /// </summary>
    [Parameter]
    public DateTimeOffset? FirstSeenUtc { get; set; }

    private string Title
    {
        get
        {
            var age = Utils.ModelFreshness.AgeLabel(FirstSeenUtc);
            return string.IsNullOrEmpty(age) ? "New model" : $"New model — first seen {age} ago";
        }
    }
}
