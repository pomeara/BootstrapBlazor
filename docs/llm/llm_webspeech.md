# WebSpeech Component Documentation

## Overview
The WebSpeech component in BootstrapBlazor provides an interface to the Web Speech API, enabling speech recognition and speech synthesis capabilities in web applications. It allows applications to convert text to speech (speech synthesis) and speech to text (speech recognition), enhancing accessibility and providing alternative interaction methods.

## Features
- **Speech Synthesis**: Convert text to speech with customizable voices and parameters
- **Speech Recognition**: Convert spoken words to text with real-time processing
- **Multiple Language Support**: Works with various languages based on browser capabilities
- **Voice Selection**: Choose from available system voices for speech synthesis
- **Continuous Recognition**: Support for continuous speech recognition with interim results
- **Customizable Parameters**: Control pitch, rate, volume for speech synthesis
- **Event Callbacks**: Rich event system for monitoring speech processes
- **Browser Integration**: Leverages native browser speech capabilities

## Main Components

### WebSpeechService
The central service that creates speech synthesis and recognition instances.

| Method | Return Type | Description |
| --- | --- | --- |
| CreateSynthesizerAsync() | WebSpeechSynthesizer | Creates a speech synthesis instance |
| CreateRecognitionAsync() | WebSpeechRecognition | Creates a speech recognition instance |

### WebSpeechSynthesizer
Handles text-to-speech functionality.

| Property/Method | Type | Description |
| --- | --- | --- |
| OnEndAsync | Func<Task>? | Callback when speech synthesis completes |
| OnErrorAsync | Func<WebSpeechSynthesisError, Task>? | Callback when an error occurs |
| GetVoices() | Task<WebSpeechSynthesisVoice[]?> | Gets available voices for synthesis |
| SpeakAsync(utterance) | Task | Starts speaking with specified utterance parameters |
| SpeakAsync(text, lang) | Task | Starts speaking with specified text and language |
| SpeakAsync(text, voice) | Task | Starts speaking with specified text and voice |
| PauseAsync() | Task | Pauses the current speech |
| ResumeAsync() | Task | Resumes paused speech |
| CancelAsync() | Task | Cancels the current speech |

### WebSpeechRecognition
Handles speech-to-text functionality.

| Property/Method | Type | Description |
| --- | --- | --- |
| OnStartAsync | Func<Task>? | Callback when recognition starts |
| OnEndAsync | Func<Task>? | Callback when recognition ends |
| OnSpeechStartAsync | Func<Task>? | Callback when speech is detected |
| OnSpeechEndAsync | Func<Task>? | Callback when speech ends |
| OnResultAsync | Func<WebSpeechRecognitionEvent, Task>? | Callback when results are available |
| OnErrorAsync | Func<WebSpeechRecognitionError, Task>? | Callback when an error occurs |
| OnNoMatchAsync | Func<WebSpeechRecognitionError, Task>? | Callback when no match is found |
| StartAsync(lang) | Task | Starts recognition with specified language |
| StartAsync(option) | Task | Starts recognition with specified options |
| StopAsync() | Task | Stops the current recognition |
| AbortAsync() | Task | Aborts the current recognition |

## Configuration Classes

### WebSpeechSynthesisUtterance
Configuration for speech synthesis.

| Property | Type | Description |
| --- | --- | --- |
| Text | string? | The text to be synthesized |
| Lang | string? | The language for synthesis (BCP 47 language tag) |
| Pitch | float? | The pitch at which the utterance will be spoken (0 to 2) |
| Rate | float? | The speed at which the utterance will be spoken (0.1 to 10) |
| Voice | WebSpeechSynthesisVoice? | The voice that will be used |
| Volume | float? | The volume of the utterance (0 to 1) |

### WebSpeechRecognitionOption
Configuration for speech recognition.

| Property | Type | Description |
| --- | --- | --- |
| MaxAlternatives | float? | Maximum number of alternatives per result |
| Continuous | bool? | Whether to return continuous results |
| InterimResults | bool? | Whether to return interim (non-final) results |
| Lang | string? | The language for recognition (BCP 47 language tag) |

## Events

### Speech Synthesis Events
- **OnEndAsync**: Triggered when speech synthesis completes
- **OnErrorAsync**: Triggered when an error occurs during synthesis

### Speech Recognition Events
- **OnStartAsync**: Triggered when recognition service begins listening
- **OnEndAsync**: Triggered when recognition service disconnects
- **OnSpeechStartAsync**: Triggered when speech is detected
- **OnSpeechEndAsync**: Triggered when speech ends
- **OnResultAsync**: Triggered when recognition results are available
- **OnErrorAsync**: Triggered when an error occurs during recognition
- **OnNoMatchAsync**: Triggered when no match is found

## Usage Examples

### Example 1: Basic Service Registration
```csharp
// In Program.cs or Startup.cs
builder.Services.AddBootstrapBlazor(); // WebSpeechService is registered automatically
```

### Example 2: Basic Text-to-Speech
```csharp
@inject WebSpeechService WebSpeechService

@code {
    private WebSpeechSynthesizer? _synthesizer;
    
    protected override async Task OnInitializedAsync()
    {
        _synthesizer = await WebSpeechService.CreateSynthesizerAsync();
    }
    
    private async Task SpeakText(string text)
    {
        if (_synthesizer != null)
        {
            await _synthesizer.SpeakAsync(text, "en-US");
        }
    }
}
```

### Example 3: Speech Synthesis with Custom Voice
```csharp
@inject WebSpeechService WebSpeechService

@code {
    private WebSpeechSynthesizer? _synthesizer;
    private List<WebSpeechSynthesisVoice> _voices = new();
    private WebSpeechSynthesisVoice? _selectedVoice;
    
    protected override async Task OnInitializedAsync()
    {
        _synthesizer = await WebSpeechService.CreateSynthesizerAsync();
        var voices = await _synthesizer.GetVoices();
        if (voices != null)
        {
            _voices.AddRange(voices);
            _selectedVoice = _voices.FirstOrDefault(v => v.Lang == "en-US");
        }
    }
    
    private async Task SpeakWithCustomVoice(string text)
    {
        if (_synthesizer != null && _selectedVoice != null)
        {
            await _synthesizer.SpeakAsync(new WebSpeechSynthesisUtterance
            {
                Text = text,
                Voice = _selectedVoice,
                Rate = 1.0f,
                Pitch = 1.0f,
                Volume = 1.0f
            });
        }
    }
}
```

### Example 4: Basic Speech Recognition
```csharp
@inject WebSpeechService WebSpeechService
@inject ToastService ToastService

@code {
    private WebSpeechRecognition? _recognition;
    private string _recognizedText = "";
    
    protected override async Task OnInitializedAsync()
    {
        _recognition = await WebSpeechService.CreateRecognitionAsync();
        
        _recognition.OnResultAsync = e =>
        {
            _recognizedText = e.Transcript;
            StateHasChanged();
            return Task.CompletedTask;
        };
        
        _recognition.OnErrorAsync = async e =>
        {
            await ToastService.Error("Recognition Error", e.Message);
        };
    }
    
    private async Task StartRecognition()
    {
        if (_recognition != null)
        {
            await _recognition.StartAsync("en-US");
        }
    }
}
```

### Example 5: Continuous Speech Recognition
```csharp
@inject WebSpeechService WebSpeechService

@code {
    private WebSpeechRecognition? _recognition;
    private string _finalResult = "";
    private string _interimResult = "";
    
    protected override async Task OnInitializedAsync()
    {
        _recognition = await WebSpeechService.CreateRecognitionAsync();
        
        _recognition.OnResultAsync = e =>
        {
            if (e.IsFinal)
            {
                _finalResult += e.Transcript + " ";
                _interimResult = "";
            }
            else
            {
                _interimResult = e.Transcript;
            }
            StateHasChanged();
            return Task.CompletedTask;
        };
    }
    
    private async Task StartContinuousRecognition()
    {
        if (_recognition != null)
        {
            await _recognition.StartAsync(new WebSpeechRecognitionOption
            {
                Continuous = true,
                InterimResults = true,
                Lang = "en-US"
            });
        }
    }
    
    private async Task StopRecognition()
    {
        if (_recognition != null)
        {
            await _recognition.StopAsync();
        }
    }
}
```

### Example 6: Speech Recognition with Status Indicators
```csharp
@inject WebSpeechService WebSpeechService

@code {
    private WebSpeechRecognition? _recognition;
    private bool _isListening = false;
    private string _result = "";
    
    protected override async Task OnInitializedAsync()
    {
        _recognition = await WebSpeechService.CreateRecognitionAsync();
        
        _recognition.OnSpeechStartAsync = () =>
        {
            _isListening = true;
            StateHasChanged();
            return Task.CompletedTask;
        };
        
        _recognition.OnSpeechEndAsync = () =>
        {
            _isListening = false;
            StateHasChanged();
            return Task.CompletedTask;
        };
        
        _recognition.OnResultAsync = e =>
        {
            _result = e.Transcript;
            StateHasChanged();
            return Task.CompletedTask;
        };
    }
}
```

### Example 7: Complete Speech Interface
```csharp
@page "/speech-demo"
@inject WebSpeechService WebSpeechService

<div class="container">
    <div class="row mb-3">
        <div class="col-md-6">
            <h3>Text to Speech</h3>
            <Select Items="_voices" @bind-Value="_selectedVoiceName" class="mb-2"></Select>
            <Textarea @bind-Value="_textToSpeak" rows="4" class="mb-2"></Textarea>
            <div>
                <Button Text="Speak" OnClick="SpeakText" Icon="fa-solid fa-microphone"></Button>
                <Button Text="Stop" OnClick="StopSpeaking" Icon="fa-solid fa-stop"></Button>
            </div>
        </div>
        <div class="col-md-6">
            <h3>Speech to Text</h3>
            <div class="mb-2">
                <Button Text="Start Recognition" OnClick="StartRecognition" 
                        IsDisabled="_isRecognizing" Icon="fa-solid fa-microphone"></Button>
                <Button Text="Stop" OnClick="StopRecognition" 
                        IsDisabled="!_isRecognizing" Icon="fa-solid fa-stop"></Button>
            </div>
            <div class="p-3 border rounded @(_isRecognizing ? "border-primary" : "")">
                <p>@_recognizedText</p>
            </div>
        </div>
    </div>
</div>

@code {
    private WebSpeechSynthesizer? _synthesizer;
    private WebSpeechRecognition? _recognition;
    private List<SelectedItem> _voices = new();
    private List<WebSpeechSynthesisVoice> _speechVoices = new();
    private string? _selectedVoiceName;
    private string _textToSpeak = "Hello, welcome to BootstrapBlazor!";
    private string _recognizedText = "";
    private bool _isRecognizing = false;
    
    protected override async Task OnInitializedAsync()
    {
        // Initialize speech synthesis
        _synthesizer = await WebSpeechService.CreateSynthesizerAsync();
        var voices = await _synthesizer.GetVoices();
        if (voices != null)
        {
            _speechVoices.AddRange(voices);
            _voices.AddRange(_speechVoices.Select(v => 
                new SelectedItem($"{v.Name}({v.Lang})", $"{v.Name}({v.Lang})"));
            
            var defaultVoice = _speechVoices.FirstOrDefault(v => v.Lang.StartsWith("en"));
            if (defaultVoice != null)
            {
                _selectedVoiceName = $"{defaultVoice.Name}({defaultVoice.Lang})";
            }
        }
        
        // Initialize speech recognition
        _recognition = await WebSpeechService.CreateRecognitionAsync();
        
        _recognition.OnSpeechStartAsync = () =>
        {
            _isRecognizing = true;
            StateHasChanged();
            return Task.CompletedTask;
        };
        
        _recognition.OnSpeechEndAsync = () =>
        {
            _isRecognizing = false;
            StateHasChanged();
            return Task.CompletedTask;
        };
        
        _recognition.OnResultAsync = e =>
        {
            _recognizedText = e.Transcript;
            StateHasChanged();
            return Task.CompletedTask;
        };
    }
    
    private async Task SpeakText()
    {
        if (_synthesizer != null && !string.IsNullOrEmpty(_textToSpeak))
        {
            var selectedVoice = _speechVoices.FirstOrDefault(v => 
                $"{v.Name}({v.Lang})" == _selectedVoiceName);
                
            await _synthesizer.SpeakAsync(new WebSpeechSynthesisUtterance
            {
                Text = _textToSpeak,
                Voice = selectedVoice
            });
        }
    }
    
    private async Task StopSpeaking()
    {
        if (_synthesizer != null)
        {
            await _synthesizer.CancelAsync();
        }
    }
    
    private async Task StartRecognition()
    {
        if (_recognition != null)
        {
            _recognizedText = "";
            await _recognition.StartAsync("en-US");
        }
    }
    
    private async Task StopRecognition()
    {
        if (_recognition != null)
        {
            await _recognition.StopAsync();
        }
    }
}
```

## Browser Compatibility

The WebSpeech component relies on the Web Speech API, which has varying levels of support across browsers:

- Chrome: Full support for both synthesis and recognition
- Edge: Full support for both synthesis and recognition
- Firefox: Support for synthesis only
- Safari: Support for synthesis, limited recognition support
- Mobile browsers: Varies by platform and browser

## Notes and Best Practices

1. **Permission Handling**: Speech recognition requires microphone access permission from the user. Applications should handle cases where permission is denied.

2. **Error Handling**: Always implement error handlers to gracefully handle recognition errors or synthesis failures.

3. **Language Support**: Check available voices and languages at runtime as they depend on the user's operating system and browser.

4. **Offline Support**: The Web Speech API typically requires an internet connection for recognition services.

5. **Accessibility**: While speech interfaces enhance accessibility, always provide alternative interaction methods for users who cannot or prefer not to use speech.

6. **Performance**: Continuous speech recognition can be resource-intensive. Consider implementing controls to start/stop recognition when needed.

7. **Testing**: Test across multiple browsers and devices to ensure consistent behavior.

## CSS Customization

The WebSpeech component itself doesn't have specific CSS variables, but you can style the UI elements that interact with it (buttons, text areas, etc.) using BootstrapBlazor's theming system.