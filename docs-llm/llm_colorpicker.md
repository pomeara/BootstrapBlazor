# ColorPicker Component Documentation

## Overview
The ColorPicker component in BootstrapBlazor provides an intuitive interface for users to select colors. It offers a visual color palette, sliders for adjusting color values, and input fields for precise color specification. This component is useful in applications that require color selection for theming, design customization, or any scenario where users need to choose colors.

## Features
- Visual color palette for quick selection
- RGB, HEX, and HSL color format support
- Alpha channel support for transparency
- Color value input fields for precise control
- Predefined color presets
- Recent colors history
- Customizable trigger button
- Popup and inline display modes
- Two-way data binding
- Form validation integration

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Value | string | "#000000" | Gets or sets the selected color value |
| ValueChanged | EventCallback<string> | - | Callback when the color value changes |
| ShowAlpha | bool | false | Whether to show alpha channel slider |
| ShowPresets | bool | true | Whether to show predefined color presets |
| ShowHistory | bool | true | Whether to show recently used colors |
| ShowClear | bool | true | Whether to show the clear button |
| ShowInput | bool | true | Whether to show color value input fields |
| Format | ColorFormat | HEX | The format of the color value (HEX, RGB, HSL) |
| Placement | Placement | Bottom | The placement of the color picker dropdown |
| IsDisabled | bool | false | Whether the color picker is disabled |
| IsInline | bool | false | Whether to display the color picker inline (not as a dropdown) |
| Presets | IEnumerable<string> | null | Custom color presets to display |
| MaxHistoryCount | int | 8 | Maximum number of colors to keep in history |
| TriggerClassName | string | null | CSS class for the trigger button |

## Events

| Event | Description |
| --- | --- |
| OnValueChanged | Triggered when the color value changes |
| OnVisibleChanged | Triggered when the dropdown visibility changes |
| OnClear | Triggered when the color value is cleared |

## Usage Examples

### Example 1: Basic ColorPicker

```razor
<ColorPicker @bind-Value="@selectedColor" />

<div class="mt-3">
    Selected color: <span style="color: @selectedColor">@selectedColor</span>
    <div style="width: 100px; height: 50px; background-color: @selectedColor; border: 1px solid #ccc;"></div>
</div>

@code {
    private string selectedColor = "#4A6BF5";
}
```

### Example 2: ColorPicker with Alpha Channel

```razor
<ColorPicker @bind-Value="@selectedColor" 
             ShowAlpha="true" 
             Format="ColorFormat.RGBA" />

<div class="mt-3">
    <div style="display: flex; align-items: center;">
        <div style="width: 100px; height: 50px; background-color: @selectedColor; border: 1px solid #ccc;"></div>
        <span class="ml-3">@selectedColor</span>
    </div>
</div>

@code {
    private string selectedColor = "rgba(74, 107, 245, 0.5)";
}
```

### Example 3: Inline ColorPicker

```razor
<div class="card">
    <div class="card-header">Inline Color Picker</div>
    <div class="card-body">
        <ColorPicker @bind-Value="@selectedColor" 
                     IsInline="true" 
                     ShowAlpha="true" />
    </div>
    <div class="card-footer">
        Selected color: <span style="color: @selectedColor">@selectedColor</span>
    </div>
</div>

@code {
    private string selectedColor = "#4A6BF5";
}
```

### Example 4: ColorPicker with Custom Presets

```razor
<ColorPicker @bind-Value="@selectedColor" 
             Presets="@customPresets" />

<div class="mt-3">
    Selected color: <span style="color: @selectedColor">@selectedColor</span>
    <div style="width: 100px; height: 50px; background-color: @selectedColor; border: 1px solid #ccc;"></div>
</div>

@code {
    private string selectedColor = "#4A6BF5";
    
    private List<string> customPresets = new List<string>
    {
        "#FF4136", // Red
        "#FF851B", // Orange
        "#FFDC00", // Yellow
        "#2ECC40", // Green
        "#0074D9", // Blue
        "#B10DC9", // Purple
        "#F012BE", // Magenta
        "#01FF70", // Lime
        "#39CCCC", // Teal
        "#85144b"  // Maroon
    };
}
```

### Example 5: ColorPicker with Different Formats

```razor
<div class="mb-3">
    <RadioGroup @bind-Value="@colorFormat" TValue="ColorFormat">
        <Radio TValue="ColorFormat" Value="ColorFormat.HEX">HEX</Radio>
        <Radio TValue="ColorFormat" Value="ColorFormat.RGB">RGB</Radio>
        <Radio TValue="ColorFormat" Value="ColorFormat.HSL">HSL</Radio>
    </RadioGroup>
</div>

<ColorPicker @bind-Value="@selectedColor" 
             Format="@colorFormat" 
             ShowAlpha="@(colorFormat != ColorFormat.HEX)" />

<div class="mt-3">
    <div style="display: flex; align-items: center;">
        <div style="width: 100px; height: 50px; background-color: @selectedColor; border: 1px solid #ccc;"></div>
        <span class="ml-3">@selectedColor</span>
    </div>
</div>

@code {
    private string selectedColor = "#4A6BF5";
    private ColorFormat colorFormat = ColorFormat.HEX;
    
    protected override void OnParametersSet()
    {
        // Update color format when changing between formats
        if (colorFormat == ColorFormat.HEX && !selectedColor.StartsWith("#"))
        {
            selectedColor = "#4A6BF5";
        }
        else if (colorFormat == ColorFormat.RGB && !selectedColor.StartsWith("rgb"))
        {
            selectedColor = "rgb(74, 107, 245)";
        }
        else if (colorFormat == ColorFormat.HSL && !selectedColor.StartsWith("hsl"))
        {
            selectedColor = "hsl(230, 90%, 63%)";
        }
    }
}
```

### Example 6: ColorPicker with Form Validation

```razor
<ValidateForm Model="@model" OnValidSubmit="@HandleValidSubmit">
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Name" placeholder="Theme name" />
        <ValidationMessage For="@(() => model.Name)" />
    </div>
    
    <div class="mb-3">
        <label>Primary Color</label>
        <ColorPicker @bind-Value="@model.PrimaryColor" />
        <ValidationMessage For="@(() => model.PrimaryColor)" />
    </div>
    
    <div class="mb-3">
        <label>Secondary Color</label>
        <ColorPicker @bind-Value="@model.SecondaryColor" />
        <ValidationMessage For="@(() => model.SecondaryColor)" />
    </div>
    
    <div class="mb-3">
        <label>Background Color</label>
        <ColorPicker @bind-Value="@model.BackgroundColor" ShowAlpha="true" Format="ColorFormat.RGBA" />
        <ValidationMessage For="@(() => model.BackgroundColor)" />
    </div>
    
    <Button Type="ButtonType.Submit">Save Theme</Button>
</ValidateForm>

<div class="mt-4 p-3" style="background-color: @model.BackgroundColor;">
    <h4 style="color: @model.PrimaryColor;">Theme Preview</h4>
    <p style="color: @model.SecondaryColor;">This is how your theme will look.</p>
    <Button Color="Color.Primary" style="background-color: @model.PrimaryColor; border-color: @model.PrimaryColor;">Primary Button</Button>
    <Button Color="Color.Secondary" style="background-color: @model.SecondaryColor; border-color: @model.SecondaryColor;">Secondary Button</Button>
</div>

@code {
    private ThemeModel model = new ThemeModel
    {
        Name = "",
        PrimaryColor = "#007bff",
        SecondaryColor = "#6c757d",
        BackgroundColor = "rgba(255, 255, 255, 1)"
    };
    
    private void HandleValidSubmit()
    {
        // Save the theme
        Console.WriteLine($"Theme saved: {model.Name}");
        Console.WriteLine($"Primary: {model.PrimaryColor}");
        Console.WriteLine($"Secondary: {model.SecondaryColor}");
        Console.WriteLine($"Background: {model.BackgroundColor}");
    }
    
    public class ThemeModel
    {
        [Required(ErrorMessage = "Theme name is required")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Primary color is required")]
        [RegularExpression(@"^#([A-Fa-f0-9]{6})$", ErrorMessage = "Invalid HEX color format")]
        public string PrimaryColor { get; set; }
        
        [Required(ErrorMessage = "Secondary color is required")]
        [RegularExpression(@"^#([A-Fa-f0-9]{6})$", ErrorMessage = "Invalid HEX color format")]
        public string SecondaryColor { get; set; }
        
        [Required(ErrorMessage = "Background color is required")]
        public string BackgroundColor { get; set; }
    }
}
```

### Example 7: ColorPicker with Custom Trigger

```razor
<div class="d-flex align-items-center">
    <span class="mr-2">Text Color:</span>
    <ColorPicker @bind-Value="@textColor" TriggerClassName="color-picker-trigger">
        <Trigger>
            <div style="width: 30px; height: 30px; border-radius: 50%; background-color: @textColor; cursor: pointer; border: 1px solid #ccc;"></div>
        </Trigger>
    </ColorPicker>
</div>

<div class="mt-3">
    <h4 style="color: @textColor;">Sample Text</h4>
    <p style="color: @textColor;">This text will change color based on your selection.</p>
</div>

<style>
    .color-picker-trigger {
        display: inline-flex;
        align-items: center;
        justify-content: center;
    }
</style>

@code {
    private string textColor = "#333333";
}
```

## Customization Notes

The ColorPicker component can be customized using the following CSS variables:

```css
:root {
    --bb-colorpicker-width: 240px;
    --bb-colorpicker-height: auto;
    --bb-colorpicker-panel-padding: 10px;
    --bb-colorpicker-panel-background: #fff;
    --bb-colorpicker-panel-border-color: rgba(0, 0, 0, 0.15);
    --bb-colorpicker-panel-border-radius: 0.25rem;
    --bb-colorpicker-panel-box-shadow: 0 2px 8px rgba(0, 0, 0, 0.15);
    --bb-colorpicker-trigger-width: 30px;
    --bb-colorpicker-trigger-height: 30px;
    --bb-colorpicker-trigger-border-radius: 4px;
    --bb-colorpicker-trigger-border-color: #ccc;
    --bb-colorpicker-slider-height: 12px;
    --bb-colorpicker-slider-border-radius: 6px;
    --bb-colorpicker-preset-size: 20px;
    --bb-colorpicker-preset-margin: 4px;
    --bb-colorpicker-preset-border-radius: 2px;
}
```

Additionally, you can customize the appearance of the ColorPicker component by:

1. Using the `Trigger` template to provide a custom trigger button
2. Using the `Presets` property to define custom color presets
3. Using the `Format` property to change the color format
4. Using the `ShowAlpha` property to enable/disable transparency
5. Using the `IsInline` property to display the color picker inline instead of as a dropdown
6. Applying custom CSS classes to the component using the `ClassName` property