// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// A single rolling quota window — labelled progress bar with used/limit and a reset countdown.
/// Atom: one concern (one window), parameters only.
/// </summary>
public sealed partial class QuotaWindowBar
{
    /// <summary>
    /// Gets or sets the window label (e.g. "Session", "Weekly").
    /// </summary>
    [Parameter]
    public string Label { get; set; } = "";

    /// <summary>
    /// Gets or sets the usage percentage.
    /// </summary>
    [Parameter]
    public double Percent { get; set; }

    /// <summary>
    /// Gets or sets the used amount.
    /// </summary>
    [Parameter]
    public long Used { get; set; }

    /// <summary>
    /// Gets or sets the limit amount.
    /// </summary>
    [Parameter]
    public long Limit { get; set; }

    /// <summary>
    /// Gets or sets the UTC timestamp when the window resets.
    /// </summary>
    [Parameter]
    public DateTimeOffset? ResetsAt { get; set; }

    private string? ClassString => CssBuilder.Default("quota-window mb-3")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string BarClass => Percent >= 95 ? "bg-danger"
        : Percent >= 80 ? "bg-warning text-dark"
        : "bg-success";

    private string Countdown
    {
        get
        {
            if (ResetsAt is null)
            {
                return "—";
            }

            var remaining = ResetsAt.Value - DateTimeOffset.Now;
            return remaining <= TimeSpan.Zero
                ? "resetting…"
                : remaining.TotalHours >= 24
                    ? $"{(int)remaining.TotalDays}d {remaining.Hours}h"
                    : $"{(int)remaining.TotalHours}h {remaining.Minutes}m";
        }
    }

    private string ResetLabel => ResetsAt?.ToLocalTime().ToString("ddd HH:mm") ?? "—";

    private string UsedLabel => Limit > 0
        ? $"{Used} / {Limit}"
        : (Used > 0 ? $"{Used}" : "—");
}
