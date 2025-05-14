# Handwritten Component Documentation

## Overview
The Handwritten component in BootstrapBlazor provides a digital signature or drawing pad functionality that allows users to write or draw using mouse, touch, or stylus input. This component is particularly useful for capturing signatures, creating sketches, or enabling freehand drawing in web applications. It offers a natural writing experience with customizable canvas settings, making it ideal for applications requiring handwritten input such as forms, contracts, or creative tools.

## Features
- **Digital Signature Capture**: Collect handwritten signatures electronically
- **Freehand Drawing**: Enable users to create sketches or drawings
- **Multi-device Support**: Works with mouse, touch screens, and stylus input
- **Adjustable Canvas**: Customizable drawing area size and appearance
- **Line Customization**: Control stroke width, color, and style
- **Clear Functionality**: One-click option to clear the drawing area
- **Image Export**: Save drawings as images in various formats (PNG, JPEG, SVG)
- **Undo/Redo Support**: Step backward and forward through drawing actions
- **Form Integration**: Seamlessly works with form validation
- **Event Callbacks**: Notifications for drawing actions and changes
- **Responsive Design**: Adapts to different screen sizes
- **Accessibility Support**: Keyboard navigation and screen reader compatibility

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Value | string | null | Gets or sets the drawing data as a base64 encoded string |
| ValueChanged | EventCallback<string> | - | Callback when the drawing value changes |
| Width | int | 400 | Width of the drawing canvas in pixels |
| Height | int | 300 | Height of the drawing canvas in pixels |
| LineWidth | float | 2.0 | Width of the drawing stroke |
| LineColor | string | "#000000" | Color of the drawing stroke |
| BackgroundColor | string | "#FFFFFF" | Background color of the canvas |
| IsDisabled | bool | false | Whether the drawing pad is disabled |
| IsReadOnly | bool | false | Whether the drawing pad is read-only |
| ShowClearButton | bool | true | Whether to show the clear button |
| ShowUndoButton | bool | true | Whether to show the undo button |
| ShowRedoButton | bool | true | Whether to show the redo button |
| ShowSaveButton | bool | true | Whether to show the save button |
| SaveFormat | string | "png" | Format for saving the drawing (png, jpeg, svg) |
| ClassName | string | "" | Additional CSS class for the component |
| PlaceholderText | string | "" | Text to display when the canvas is empty |

## Events

| Event | Description |
| --- | --- |
| OnValueChanged | Triggered when the drawing value changes |
| OnDrawStart | Triggered when a drawing action begins |
| OnDrawEnd | Triggered when a drawing action ends |
| OnClear | Triggered when the canvas is cleared |
| OnSave | Triggered when the drawing is saved |
| OnUndo | Triggered when an undo action is performed |
| OnRedo | Triggered when a redo action is performed |

## Usage Examples

### Example 1: Basic Handwritten Signature Pad

```razor
<div class="mb-3">
    <h5>Please sign below:</h5>
    <Handwritten @bind-Value="@signature"
                Width="500"
                Height="200"
                LineWidth="2.5"
                LineColor="#0066cc" />
</div>

<div class="mt-3">
    <Button Color="Color.Primary" OnClick="@SaveSignature">Submit Signature</Button>
    <Button Color="Color.Secondary" OnClick="@ClearSignature">Clear</Button>
</div>

@if (!string.IsNullOrEmpty(signature))
{
    <div class="mt-3">
        <h6>Preview:</h6>
        <img src="@signature" alt="Signature" style="border: 1px solid #dee2e6; max-width: 100%;" />
    </div>
}

@code {
    private string signature;
    
    private void SaveSignature()
    {
        Console.WriteLine("Signature saved!");
        // Here you would typically send the signature to your backend
    }
    
    private void ClearSignature()
    {
        signature = null;
    }
}
```

### Example 2: Handwritten Component with Form Validation

```razor
<ValidateForm Model="@model" OnValidSubmit="@HandleValidSubmit">
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Name" placeholder="Your name" />
        <ValidationMessage For="@(() => model.Name)" />
    </div>
    
    <div class="mb-3">
        <label>Your Signature</label>
        <Handwritten @bind-Value="@model.Signature" />
        <ValidationMessage For="@(() => model.Signature)" />
    </div>
    
    <Button Type="ButtonType.Submit">Submit Form</Button>
</ValidateForm>

@code {
    private SignatureModel model = new SignatureModel
    {
        Name = "",
        Signature = null
    };
    
    private void HandleValidSubmit()
    {
        // Process the form data
        Console.WriteLine($"Form submitted by: {model.Name}");
        Console.WriteLine($"Signature provided: {!string.IsNullOrEmpty(model.Signature)}");
    }
    
    public class SignatureModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Signature is required")]
        public string Signature { get; set; }
    }
}
```

### Example 3: Drawing Pad with Color Options

```razor
<div class="mb-3">
    <h5>Drawing Pad</h5>
    <div class="mb-2">
        <label>Line Color:</label>
        <div class="d-flex">
            @foreach (var color in colors)
            {
                <div class="color-option @(currentColor == color ? "active" : "")" 
                     style="background-color: @color"
                     @onclick="() => ChangeColor(color)"></div>
            }
        </div>
    </div>
    <div class="mb-2">
        <label>Line Width: @currentWidth</label>
        <input type="range" class="form-range" min="1" max="10" step="0.5" @bind="currentWidth" />
    </div>
    <Handwritten @bind-Value="@drawing"
                Width="600"
                Height="400"
                LineWidth="@currentWidth"
                LineColor="@currentColor"
                BackgroundColor="#f8f9fa" />
</div>

<div class="mt-3">
    <Button Color="Color.Primary" OnClick="@SaveDrawing">Save Drawing</Button>
    <Button Color="Color.Secondary" OnClick="@ClearDrawing">Clear</Button>
</div>

<style>
    .color-option {
        width: 30px;
        height: 30px;
        border-radius: 50%;
        margin-right: 10px;
        cursor: pointer;
        border: 2px solid transparent;
    }
    
    .color-option.active {
        border-color: #000;
    }
</style>

@code {
    private string drawing;
    private float currentWidth = 2.5f;
    private string currentColor = "#000000";
    private string[] colors = new[] { "#000000", "#ff0000", "#0000ff", "#008000", "#ffa500", "#800080" };
    
    private void ChangeColor(string color)
    {
        currentColor = color;
    }
    
    private void SaveDrawing()
    {
        Console.WriteLine("Drawing saved!");
        // Here you would typically process the drawing data
    }
    
    private void ClearDrawing()
    {
        drawing = null;
    }
}
```

### Example 4: Handwritten Component with Event Handling

```razor
<Handwritten @bind-Value="@signature"
            OnDrawStart="HandleDrawStart"
            OnDrawEnd="HandleDrawEnd"
            OnClear="HandleClear"
            OnSave="HandleSave" />

<div class="mt-3">
    <h5>Event Log</h5>
    <div class="border p-3 bg-light" style="max-height: 200px; overflow-y: auto;">
        @foreach (var log in eventLogs.AsEnumerable().Reverse())
        {
            <div class="mb-1">@log</div>
        }
    </div>
</div>

<div class="mt-3">
    <Button Color="Color.Secondary" OnClick="ClearLogs">Clear Logs</Button>
</div>

@code {
    private string signature;
    private List<string> eventLogs = new();
    
    private void HandleDrawStart()
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Drawing started");
    }
    
    private void HandleDrawEnd(string value)
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Drawing ended");
    }
    
    private void HandleClear()
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Canvas cleared");
    }
    
    private void HandleSave(string value)
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Drawing saved");
    }
    
    private void ClearLogs()
    {
        eventLogs.Clear();
    }
}
```

### Example 5: Responsive Handwritten Component

```razor
<div class="container">
    <div class="row">
        <div class="col-md-6 mb-3">
            <div class="card">
                <div class="card-header">
                    <h5 class="mb-0">Sign Here</h5>
                </div>
                <div class="card-body">
                    <Handwritten @bind-Value="@signature"
                                Width="@GetCanvasWidth()"
                                Height="200" />
                </div>
                <div class="card-footer">
                    <Button Color="Color.Primary" Size="Size.Small" OnClick="@SaveSignature">Save</Button>
                    <Button Color="Color.Secondary" Size="Size.Small" OnClick="@ClearSignature">Clear</Button>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="card">
                <div class="card-header">
                    <h5 class="mb-0">Preview</h5>
                </div>
                <div class="card-body text-center">
                    @if (!string.IsNullOrEmpty(signature))
                    {
                        <img src="@signature" alt="Signature" style="max-width: 100%; border: 1px solid #dee2e6;" />
                    }
                    else
                    {
                        <div class="text-muted">No signature yet</div>
                    }
                </div>
            </div>
        </div>
    </div>
</div>

@code {
    private string signature;
    private bool isSmallScreen = false;
    
    protected override void OnInitialized()
    {
        // In a real application, you would detect screen size using JavaScript interop
        isSmallScreen = false;
    }
    
    private int GetCanvasWidth()
    {
        return isSmallScreen ? 300 : 400;
    }
    
    private void SaveSignature()
    {
        Console.WriteLine("Signature saved!");
    }
    
    private void ClearSignature()
    {
        signature = null;
    }
}
```

### Example 6: Handwritten Component with Undo/Redo

```razor
<div class="mb-3">
    <h5>Drawing with History</h5>
    <Handwritten @bind-Value="@drawing"
                Width="500"
                Height="300"
                ShowUndoButton="true"
                ShowRedoButton="true"
                OnUndo="HandleUndo"
                OnRedo="HandleRedo" />
</div>

<div class="mt-3">
    <div class="d-flex gap-2">
        <Button Color="Color.Secondary" OnClick="@(() => UndoAction())" Disabled="@(!canUndo)">
            <i class="bi bi-arrow-counterclockwise"></i> Undo
        </Button>
        <Button Color="Color.Secondary" OnClick="@(() => RedoAction())" Disabled="@(!canRedo)">
            <i class="bi bi-arrow-clockwise"></i> Redo
        </Button>
        <Button Color="Color.Danger" OnClick="@(() => ClearDrawing())">
            <i class="bi bi-trash"></i> Clear
        </Button>
    </div>
</div>

<div class="mt-3">
    <h6>Action History:</h6>
    <div class="border p-3 bg-light" style="max-height: 150px; overflow-y: auto;">
        @foreach (var action in actionHistory.AsEnumerable().Reverse())
        {
            <div class="mb-1">@action</div>
        }
    </div>
</div>

@code {
    private string drawing;
    private List<string> actionHistory = new();
    private bool canUndo = false;
    private bool canRedo = false;
    
    private void HandleUndo()
    {
        actionHistory.Add($"[{DateTime.Now:HH:mm:ss}] Undo action performed");
        canUndo = true; // In a real implementation, this would be determined by the component
    }
    
    private void HandleRedo()
    {
        actionHistory.Add($"[{DateTime.Now:HH:mm:ss}] Redo action performed");
        canRedo = false; // In a real implementation, this would be determined by the component
    }
    
    private void UndoAction()
    {
        // This would call the component's undo method via a reference
        actionHistory.Add($"[{DateTime.Now:HH:mm:ss}] External undo requested");
    }
    
    private void RedoAction()
    {
        // This would call the component's redo method via a reference
        actionHistory.Add($"[{DateTime.Now:HH:mm:ss}] External redo requested");
    }
    
    private void ClearDrawing()
    {
        drawing = null;
        actionHistory.Add($"[{DateTime.Now:HH:mm:ss}] Canvas cleared");
        canUndo = false;
        canRedo = false;
    }
}
```

### Example 7: Handwritten Component in a Multi-step Form

```razor
<div class="card">
    <div class="card-header">
        <h5 class="mb-0">Contract Signing - Step @currentStep of 3</h5>
    </div>
    <div class="card-body">
        @if (currentStep == 1)
        {
            <div class="mb-3">
                <h6>Personal Information</h6>
                <div class="mb-2">
                    <label>Full Name</label>
                    <BootstrapInput @bind-Value="@model.FullName" placeholder="Enter your full name" />
                </div>
                <div class="mb-2">
                    <label>Email</label>
                    <BootstrapInput @bind-Value="@model.Email" placeholder="Enter your email" />
                </div>
            </div>
        }
        else if (currentStep == 2)
        {
            <div class="mb-3">
                <h6>Contract Terms</h6>
                <div class="border p-3 mb-3" style="max-height: 200px; overflow-y: auto;">
                    <p>This is a sample contract text. In a real application, this would contain the full contract terms and conditions that the user needs to review before signing.</p>
                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam auctor, nisl eget ultricies tincidunt, nisl nisl aliquam nisl, eget ultricies nisl nisl eget nisl.</p>
                </div>
                <div class="form-check">
                    <input class="form-check-input" type="checkbox" id="termsCheck" @bind="model.AgreedToTerms" />
                    <label class="form-check-label" for="termsCheck">
                        I have read and agree to the terms and conditions
                    </label>
                </div>
            </div>
        }
        else if (currentStep == 3)
        {
            <div class="mb-3">
                <h6>Signature</h6>
                <p>Please sign below to complete the contract:</p>
                <Handwritten @bind-Value="@model.Signature"
                            Width="500"
                            Height="200" />
            </div>
        }
    </div>
    <div class="card-footer d-flex justify-content-between">
        <Button Color="Color.Secondary" OnClick="PreviousStep" Disabled="@(currentStep == 1)">
            Previous
        </Button>
        <Button Color="Color.Primary" OnClick="NextStep">
            @(currentStep == 3 ? "Submit" : "Next")
        </Button>
    </div>
</div>

@if (isSubmitted)
{
    <div class="alert alert-success mt-3">
        <h6>Contract Submitted Successfully!</h6>
        <p>Thank you for signing the contract, @model.FullName.</p>
    </div>
}

@code {
    private ContractModel model = new ContractModel();
    private int currentStep = 1;
    private bool isSubmitted = false;
    
    private void NextStep()
    {
        if (currentStep < 3)
        {
            currentStep++;
        }
        else
        {
            SubmitContract();
        }
    }
    
    private void PreviousStep()
    {
        if (currentStep > 1)
        {
            currentStep--;
        }
    }
    
    private void SubmitContract()
    {
        // In a real application, you would submit the contract data to your backend
        Console.WriteLine($"Contract submitted by: {model.FullName}");
        Console.WriteLine($"Email: {model.Email}");
        Console.WriteLine($"Agreed to terms: {model.AgreedToTerms}");
        Console.WriteLine($"Signature provided: {!string.IsNullOrEmpty(model.Signature)}");
        
        isSubmitted = true;
    }
    
    public class ContractModel
    {
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public bool AgreedToTerms { get; set; } = false;
        public string Signature { get; set; }
    }
}
```

## Customization Notes

The Handwritten component can be customized using the following CSS variables:

```css
:root {
    --bb-handwritten-border-color: rgba(0, 0, 0, 0.125);
    --bb-handwritten-border-radius: 0.25rem;
    --bb-handwritten-min-height: 300px;
    --bb-handwritten-buttons-margin-top: 1rem;
    --bb-handwritten-button-spacing: 0.5rem;
    --bb-handwritten-button-color: #6c757d;
    --bb-handwritten-button-hover-color: #5a6268;
    --bb-handwritten-button-active-color: #545b62;
}
```

Additionally, you can customize the appearance and behavior of the Handwritten component by:

1. Using the `Width` and `Height` properties to adjust the canvas size
2. Using the `LineWidth` and `LineColor` properties to customize the drawing stroke
3. Using the `BackgroundColor` property to change the canvas background
4. Using the `ShowClearButton`, `ShowUndoButton`, `ShowRedoButton`, and `ShowSaveButton` properties to control which buttons are displayed
5. Using the `SaveFormat` property to specify the image format for saving
6. Using the `PlaceholderText` property to display guidance text when the canvas is empty
7. Applying custom CSS classes to the component using the `ClassName` property