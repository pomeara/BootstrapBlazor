// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Pull-a-model form for local runtimes — a name input plus a live progress bar fed by
/// the host's <see cref="PullStream"/> (an <see cref="IAsyncEnumerable{T}"/> of
/// <see cref="ModelPullProgress"/>). The component owns only the UX: it knows nothing
/// about which registry or provider performs the pull.
/// </summary>
public sealed partial class ModelPullPanel
{
    [Inject]
    [NotNull]
    private ToastService? ToastService { get; set; }

    /// <summary>Pull-stream source: model name → progress updates. Throwing it fails the pull.</summary>
    [Parameter]
    public Func<string, IAsyncEnumerable<ModelPullProgress>>? PullStream { get; set; }

    /// <summary>Raised after a successful pull — hosts typically re-fetch their model list.</summary>
    [Parameter]
    public EventCallback OnModelPulled { get; set; }

    /// <summary>Section title.</summary>
    [Parameter]
    public string Title { get; set; } = "Pull New Model";

    /// <summary>Input placeholder.</summary>
    [Parameter]
    public string PlaceHolder { get; set; } = "Model name (e.g., llama3.2)";

    /// <summary>Toast title on success.</summary>
    [Parameter]
    public string SuccessTitle { get; set; } = "Model Pulled";

    private string _modelName = "";
    private string? _progressText;
    private bool _isPulling;

    private async Task PullModelAsync()
    {
        if (string.IsNullOrWhiteSpace(_modelName) || _isPulling)
        {
            return;
        }

        if (PullStream is null)
        {
            await ToastService.Error(Title, "No pull stream configured");
            return;
        }

        var modelName = _modelName;
        _progressText = "Initializing...";
        _isPulling = true;

        try
        {
            await foreach (var progress in PullStream(modelName))
            {
                _progressText = progress.DisplayText;
                // Blazor renders once after the handler completes — without this the
                // pull progress bar updates only when the download finishes.
                await InvokeAsync(StateHasChanged);
            }

            await ToastService.Success(SuccessTitle, $"Model {modelName} is ready");
            _modelName = "";
            await OnModelPulled.InvokeAsync();
        }
        catch (Exception ex)
        {
            await ToastService.Error("Pull Failed", ex.Message);
        }
        finally
        {
            _isPulling = false;
            _progressText = null;
        }
    }
}
