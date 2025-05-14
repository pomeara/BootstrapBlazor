# Speech Component

## Overview
The Speech component in BootstrapBlazor provides a user-friendly interface for speech recognition and text-to-speech functionality. It enables applications to convert spoken language into text and text into spoken language, enhancing accessibility and providing alternative input/output methods for users.

## Features
- **Speech Recognition**: Converts spoken words into text in real-time
- **Text-to-Speech**: Converts text into natural-sounding speech
- **Multiple Language Support**: Works with various languages and dialects
- **Voice Control**: Enables voice command functionality for applications
- **Customizable Recognition Parameters**: Adjust confidence levels, timeout periods, and other recognition settings
- **Continuous Recognition Mode**: Option for ongoing speech recognition
- **Interim Results**: Access to preliminary recognition results before final confirmation
- **Recognition Status Indicators**: Visual feedback on recognition state

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Language` | string | "en-US" | Sets the language for speech recognition and synthesis |
| `Continuous` | bool | false | When true, recognition doesn't stop after the first recognized phrase |
| `InterimResults` | bool | false | When true, returns interim results during recognition |
| `MaxAlternatives` | int | 1 | Maximum number of alternative recognitions to return |
| `RecognitionTimeout` | int | 10000 | Time in milliseconds before recognition times out |
| `SpeechRate` | double | 1.0 | Rate of speech for text-to-speech (0.1 to 10.0) |
| `Pitch` | double | 1.0 | Pitch of speech for text-to-speech (0.0 to 2.0) |
| `Volume` | double | 1.0 | Volume of speech for text-to-speech (0.0 to 1.0) |
| `Voice` | string | null | Specific voice to use for speech synthesis |
| `IsListening` | bool | false | Indicates if speech recognition is currently active |
| `IsSpeaking` | bool | false | Indicates if text-to-speech is currently active |
| `RecognizedText` | string | "" | The text recognized from speech |
| `TextToSpeak` | string | "" | The text to be converted to speech |

## Events

| Event | Description |
|-------|-------------|
| `OnRecognitionStarted` | Triggered when speech recognition begins |
| `OnRecognitionEnded` | Triggered when speech recognition ends |
| `OnRecognitionResult` | Triggered when speech is recognized, providing the recognized text |
| `OnRecognitionError` | Triggered when an error occurs during recognition |
| `OnSpeechStarted` | Triggered when text-to-speech begins |
| `OnSpeechEnded` | Triggered when text-to-speech ends |
| `OnSpeechPaused` | Triggered when text-to-speech is paused |
| `OnSpeechResumed` | Triggered when text-to-speech is resumed |
| `OnSpeechError` | Triggered when an error occurs during speech synthesis |

## Usage Examples

### Example 1: Basic Speech Recognition

```html
<Speech @bind-RecognizedText="recognizedText" 
        Language="en-US"
        OnRecognitionResult="HandleRecognitionResult" />

<Button Text="Start Listening" OnClick="StartListening" />
<Button Text="Stop Listening" OnClick="StopListening" />

<div class="mt-3">
    <h5>Recognized Text:</h5>
    <p>@recognizedText</p>
</div>
```

```csharp
@code {
    private string recognizedText = "";
    private Speech speechComponent;
    
    private void StartListening()
    {
        speechComponent.StartRecognition();
    }
    
    private void StopListening()
    {
        speechComponent.StopRecognition();
    }
    
    private void HandleRecognitionResult(string text)
    {
        recognizedText = text;
        StateHasChanged();
    }
}
```

### Example 2: Text-to-Speech

```html
<div class="form-group">
    <TextArea @bind-Value="textToSpeak" Rows="4" placeholder="Enter text to speak..." />
</div>

<Speech TextToSpeak="@textToSpeak"
        SpeechRate="1.0"
        Pitch="1.0"
        Volume="1.0"
        OnSpeechStarted="HandleSpeechStarted"
        OnSpeechEnded="HandleSpeechEnded" />

<Button Text="Speak" OnClick="SpeakText" />
<Button Text="Stop" OnClick="StopSpeaking" />

<div class="mt-3">
    <Badge Color="@statusColor">@statusText</Badge>
</div>
```

```csharp
@code {
    private string textToSpeak = "";
    private Speech speechComponent;
    private string statusText = "Ready";
    private Color statusColor = Color.Secondary;
    
    private void SpeakText()
    {
        speechComponent.Speak();
    }
    
    private void StopSpeaking()
    {
        speechComponent.StopSpeaking();
    }
    
    private void HandleSpeechStarted()
    {
        statusText = "Speaking...";
        statusColor = Color.Primary;
        StateHasChanged();
    }
    
    private void HandleSpeechEnded()
    {
        statusText = "Finished";
        statusColor = Color.Success;
        StateHasChanged();
    }
}
```

### Example 3: Voice Commands

```html
<Speech @bind-RecognizedText="recognizedCommand"
        Continuous="true"
        Language="en-US"
        OnRecognitionResult="ProcessVoiceCommand" />

<Button Text="Start Voice Commands" OnClick="StartVoiceCommands" />
<Button Text="Stop Voice Commands" OnClick="StopVoiceCommands" />

<div class="mt-3">
    <h5>Command Status:</h5>
    <p>@commandStatus</p>
</div>

<div class="mt-3">
    <h5>Available Commands:</h5>
    <ul>
        <li>"Open dashboard"</li>
        <li>"Show profile"</li>
        <li>"Log out"</li>
        <li>"Toggle dark mode"</li>
    </ul>
</div>
```

```csharp
@code {
    private string recognizedCommand = "";
    private string commandStatus = "Waiting for command...";
    private Speech speechComponent;
    
    private void StartVoiceCommands()
    {
        speechComponent.StartRecognition();
        commandStatus = "Listening for commands...";
    }
    
    private void StopVoiceCommands()
    {
        speechComponent.StopRecognition();
        commandStatus = "Voice commands stopped";
    }
    
    private void ProcessVoiceCommand(string command)
    {
        recognizedCommand = command.ToLower();
        
        if (recognizedCommand.Contains("open dashboard"))
        {
            commandStatus = "Executing: Opening dashboard";
            // Navigation logic here
        }
        else if (recognizedCommand.Contains("show profile"))
        {
            commandStatus = "Executing: Showing profile";
            // Profile display logic here
        }
        else if (recognizedCommand.Contains("log out"))
        {
            commandStatus = "Executing: Logging out";
            // Logout logic here
        }
        else if (recognizedCommand.Contains("toggle dark mode"))
        {
            commandStatus = "Executing: Toggling dark mode";
            // Theme toggle logic here
        }
        else
        {
            commandStatus = $"Unknown command: {recognizedCommand}";
        }
        
        StateHasChanged();
    }
}
```

### Example 4: Multilingual Support

```html
<div class="form-group">
    <Select TValue="string" @bind-Value="selectedLanguage" Items="availableLanguages" />
</div>

<Speech @bind-RecognizedText="recognizedText"
        Language="@selectedLanguage"
        OnRecognitionResult="HandleRecognitionResult" />

<Button Text="Start Listening" OnClick="StartListening" />
<Button Text="Stop Listening" OnClick="StopListening" />

<div class="mt-3">
    <h5>Recognized Text (@selectedLanguage):</h5>
    <p>@recognizedText</p>
</div>
```

```csharp
@code {
    private string recognizedText = "";
    private Speech speechComponent;
    private string selectedLanguage = "en-US";
    
    private List<SelectedItem> availableLanguages = new List<SelectedItem>
    {
        new SelectedItem { Text = "English (US)", Value = "en-US" },
        new SelectedItem { Text = "Spanish", Value = "es-ES" },
        new SelectedItem { Text = "French", Value = "fr-FR" },
        new SelectedItem { Text = "German", Value = "de-DE" },
        new SelectedItem { Text = "Chinese", Value = "zh-CN" },
        new SelectedItem { Text = "Japanese", Value = "ja-JP" }
    };
    
    private void StartListening()
    {
        speechComponent.StartRecognition();
    }
    
    private void StopListening()
    {
        speechComponent.StopRecognition();
    }
    
    private void HandleRecognitionResult(string text)
    {
        recognizedText = text;
        StateHasChanged();
    }
}
```

### Example 5: Dictation with Interim Results

```html
<Speech @bind-RecognizedText="dictationText"
        Continuous="true"
        InterimResults="true"
        Language="en-US"
        OnRecognitionResult="UpdateDictation" />

<div class="form-group">
    <Button Text="Start Dictation" OnClick="StartDictation" />
    <Button Text="Stop Dictation" OnClick="StopDictation" />
    <Button Text="Clear" OnClick="ClearDictation" />
</div>

<div class="mt-3">
    <h5>Dictation:</h5>
    <div class="dictation-box p-3 border rounded">
        <p>@dictationText</p>
        <p class="text-muted">@interimText</p>
    </div>
</div>
```

```csharp
@code {
    private string dictationText = "";
    private string interimText = "";
    private Speech speechComponent;
    private bool isInterim = false;
    
    private void StartDictation()
    {
        speechComponent.StartRecognition();
    }
    
    private void StopDictation()
    {
        speechComponent.StopRecognition();
        interimText = "";
    }
    
    private void ClearDictation()
    {
        dictationText = "";
        interimText = "";
    }
    
    private void UpdateDictation(string text, bool isInterimResult = false)
    {
        if (isInterimResult)
        {
            interimText = text;
        }
        else
        {
            dictationText += (dictationText.Length > 0 ? " " : "") + text;
            interimText = "";
        }
        
        StateHasChanged();
    }
}
```

### Example 6: Accessibility Reader

```html
<div class="form-group">
    <h5>Accessibility Reader</h5>
    <p>Hover over elements to hear their descriptions</p>
</div>

<Speech @ref="speechComponent"
        TextToSpeak="@textToSpeak"
        SpeechRate="1.2"
        Volume="0.8" />

<div class="accessibility-demo mt-4">
    <div class="card mb-3" @onmouseover="() => ReadText('This is a product card for a laptop computer')">
        <div class="card-body">
            <h5 class="card-title">Laptop Computer</h5>
            <p class="card-text">High-performance laptop with 16GB RAM and 512GB SSD</p>
            <Button Text="View Details" />
        </div>
    </div>
    
    <div class="alert alert-info" @onmouseover="() => ReadText('Information alert: Your order has been shipped')">
        Your order has been shipped and will arrive in 2-3 business days.
    </div>
    
    <div class="form-group" @onmouseover="() => ReadText('Input field for email address')">
        <label for="email">Email Address</label>
        <input type="email" class="form-control" id="email" placeholder="Enter your email" />
    </div>
</div>
```

```csharp
@code {
    private string textToSpeak = "";
    private Speech speechComponent;
    
    private void ReadText(string text)
    {
        if (speechComponent.IsSpeaking)
        {
            speechComponent.StopSpeaking();
        }
        
        textToSpeak = text;
        speechComponent.Speak();
    }
}
```

### Example 7: Speech Recognition with Confidence Levels

```html
<Speech @bind-RecognizedText="recognizedText"
        MaxAlternatives="5"
        OnRecognitionResult="HandleRecognitionWithAlternatives" />

<Button Text="Start Recognition" OnClick="StartRecognition" />
<Button Text="Stop Recognition" OnClick="StopRecognition" />

<div class="mt-3">
    <h5>Recognition Results:</h5>
    <div class="primary-result p-2 border rounded mb-2">
        <strong>Primary:</strong> @recognizedText
    </div>
    
    <h6>Alternatives:</h6>
    <ul class="list-group">
        @foreach (var alt in alternatives)
        {
            <li class="list-group-item d-flex justify-content-between align-items-center">
                @alt.Text
                <span class="badge bg-primary rounded-pill">@(alt.Confidence.ToString("P1"))</span>
            </li>
        }
    </ul>
</div>
```

```csharp
@code {
    private string recognizedText = "";
    private List<RecognitionAlternative> alternatives = new List<RecognitionAlternative>();
    private Speech speechComponent;
    
    private void StartRecognition()
    {
        alternatives.Clear();
        speechComponent.StartRecognition();
    }
    
    private void StopRecognition()
    {
        speechComponent.StopRecognition();
    }
    
    private void HandleRecognitionWithAlternatives(SpeechRecognitionResult result)
    {
        recognizedText = result.Alternatives[0].Text;
        
        alternatives = result.Alternatives
            .Skip(1) // Skip the primary result
            .Select(a => new RecognitionAlternative { Text = a.Text, Confidence = a.Confidence })
            .ToList();
            
        StateHasChanged();
    }
    
    private class RecognitionAlternative
    {
        public string Text { get; set; }
        public double Confidence { get; set; }
    }
}
```

## CSS Customization

The Speech component can be customized using CSS variables and classes:

```css
/* Custom styles for Speech component */
.bb-speech {
    /* Component container */
}

.bb-speech-status {
    /* Status indicator styles */
}

.bb-speech-status-listening {
    /* Styles when actively listening */
    animation: pulse 1.5s infinite;
}

.bb-speech-status-speaking {
    /* Styles when actively speaking */
    animation: wave 1.2s infinite;
}

@keyframes pulse {
    0% { opacity: 1; }
    50% { opacity: 0.5; }
    100% { opacity: 1; }
}

@keyframes wave {
    0% { transform: scaleY(1); }
    50% { transform: scaleY(0.6); }
    100% { transform: scaleY(1); }
}
```

## JavaScript Interop

The Speech component uses JavaScript interop to access the Web Speech API. You can extend its functionality by using the following methods:

```csharp
// Start speech recognition
await JSRuntime.InvokeVoidAsync("bootstrapBlazor.speech.startRecognition", elementRef, options);

// Stop speech recognition
await JSRuntime.InvokeVoidAsync("bootstrapBlazor.speech.stopRecognition", elementRef);

// Start text-to-speech
await JSRuntime.InvokeVoidAsync("bootstrapBlazor.speech.speak", elementRef, text, options);

// Stop text-to-speech
await JSRuntime.InvokeVoidAsync("bootstrapBlazor.speech.stopSpeaking", elementRef);

// Check if speech synthesis is supported
var isSupported = await JSRuntime.InvokeAsync<bool>("bootstrapBlazor.speech.isSpeechSynthesisSupported");

// Get available voices
var voices = await JSRuntime.InvokeAsync<List<VoiceInfo>>("bootstrapBlazor.speech.getVoices");
```

## Accessibility

The Speech component is designed with accessibility in mind:

- Provides alternative input methods for users with motor impairments
- Offers text-to-speech for users with visual impairments
- Includes ARIA attributes for screen reader compatibility
- Supports keyboard navigation and control

## Browser Compatibility

The Speech component relies on the Web Speech API, which has varying levels of support across browsers:

- Chrome: Full support
- Edge: Full support
- Firefox: Partial support (may require enabling flags)
- Safari: Partial support
- Mobile browsers: Varies by platform

The component includes fallback mechanisms for browsers with limited or no support for the Web Speech API.

## Integration with Other Components

The Speech component can be integrated with various other BootstrapBlazor components:

- Use with Form components for voice-controlled form input
- Combine with Modal or Dialog for voice-activated popups
- Integrate with Table for voice-controlled data filtering
- Pair with Navigation components for voice-controlled navigation
- Use with Notification components for spoken alerts