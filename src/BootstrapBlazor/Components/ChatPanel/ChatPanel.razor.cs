// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Minimal prompt panel — provider + model pick (selection policy via <see cref="ModelSelectorBase"/>),
/// a prompt box and a streaming output area. The response stream comes from the host via
/// <see cref="ChatStream"/> (provider, model, prompt → chunks of text); the component owns only
/// the UX: validation toasts, per-chunk rendering and the run-button spinner state.
/// </summary>
public sealed partial class ChatPanel
{
    [Inject]
    [NotNull]
    private ToastService? ToastService { get; set; }

    /// <summary>Response source: (provider, model, prompt) → streamed text chunks. Throwing it fails the run.</summary>
    [Parameter]
    public Func<string, string, string, IAsyncEnumerable<string>>? ChatStream { get; set; }

    /// <summary>Card title.</summary>
    [Parameter]
    public string Title { get; set; } = "Chat";

    /// <summary>Prompt box placeholder.</summary>
    [Parameter]
    public string PromptPlaceHolder { get; set; } = "Type a prompt...";

    /// <summary>Placeholder shown before the first run.</summary>
    [Parameter]
    public string EmptyText { get; set; } = "Run a prompt to see the model response here.";

    /// <summary>Shown (instead of an empty area) when the stream completes without any chunks.</summary>
    [Parameter]
    public string EmptyResponseText { get; set; } = "Model returned empty response.";

    /// <summary>Raised after a successful run with the full response text.</summary>
    [Parameter]
    public EventCallback<string> OnResponseComplete { get; set; }

    private string? _prompt;
    private string? _result;
    private bool _isRunning;

    private List<SelectedItem> _providerItems = [];
    private List<SelectedItem> _modelItems = [];

    private string? ProviderValue
    {
        get => Provider;
        set
        {
            if (value is not null)
            {
                SelectProvider(value);
            }
        }
    }

    private string? ModelValue
    {
        get => SelectedModel;
        set
        {
            if (value is null)
            {
                return;
            }
            var model = CurrentModels.FirstOrDefault(m => m.Id == value);
            if (model is not null)
            {
                SelectModel(model);
            }
        }
    }

    /// <inheritdoc />
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        _providerItems = Sets.Select(p =>
        {
            var name = string.IsNullOrEmpty(p.DisplayName) ? p.Name : p.DisplayName;
            return new SelectedItem(p.Name, $"{name} ({(p.IsOnline ? "Online" : "Offline")})");
        }).ToList();
        _modelItems = CurrentModels.Select(m => new SelectedItem(m.Id, FormatModelDetail(m, false))).ToList();
    }

    private async Task RunChatAsync()
    {
        if (_isRunning)
        {
            return;
        }

        if (string.IsNullOrEmpty(Provider) || string.IsNullOrEmpty(SelectedModel))
        {
            await ToastService.Warning(Title, "Select a provider and model first");
            return;
        }

        if (string.IsNullOrWhiteSpace(_prompt))
        {
            await ToastService.Warning(Title, "Enter a prompt first");
            return;
        }

        if (ChatStream is null)
        {
            await ToastService.Error(Title, "No chat stream configured");
            return;
        }

        var provider = Provider;
        var model = SelectedModel;
        var prompt = _prompt;
        _result = "";
        _isRunning = true;

        try
        {
            await foreach (var chunk in ChatStream(provider, model, prompt))
            {
                _result += chunk;
                // Blazor renders once after the handler completes — without this the
                // output area stays blank until the stream has fully drained.
                await InvokeAsync(StateHasChanged);
            }

            if (string.IsNullOrEmpty(_result))
            {
                _result = EmptyResponseText;
            }
            await OnResponseComplete.InvokeAsync(_result);
        }
        catch (Exception ex)
        {
            _result = $"Error: {ex.Message}";
            await ToastService.Error("Chat Failed", ex.Message);
        }
        finally
        {
            _isRunning = false;
            await InvokeAsync(StateHasChanged);
        }
    }
}
