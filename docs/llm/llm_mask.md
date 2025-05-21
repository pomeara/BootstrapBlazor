# Mask Component

## Overview
The Mask component in BootstrapBlazor provides a way to apply input masks to form fields, ensuring data is entered in a specific format. It helps validate and format user input in real-time according to predefined patterns, making it easier to collect structured data like phone numbers, dates, credit card numbers, and other formatted inputs. The component enhances user experience by guiding input and reducing errors during data entry.

## Features
- **Predefined Mask Types**: Common mask patterns for phone numbers, dates, credit cards, etc.
- **Custom Mask Patterns**: Support for custom mask definitions using special characters
- **Real-time Formatting**: Formats input as the user types
- **Placeholder Support**: Visual guides showing the expected format
- **Validation Integration**: Works with form validation components
- **Customizable Appearance**: Styling options to match application design
- **Clear Button**: Optional button to clear the masked input
- **Copy Support**: Ability to copy the formatted or raw value
- **Multiple Input Types**: Works with text, number, and date inputs
- **Accessibility Support**: Screen reader compatible with proper ARIA attributes
- **Localization**: Support for different regional formats

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Value` | string | null | The current value of the masked input |
| `MaskType` | MaskType | MaskType.None | Predefined mask type (None, Phone, Date, SSN, etc.) |
| `Pattern` | string | null | Custom mask pattern using special characters |
| `Placeholder` | string | null | Placeholder text when input is empty |
| `PlaceholderChar` | char | '_' | Character used as placeholder for empty positions in the mask |
| `ShowPlaceholder` | bool | true | Whether to show the mask placeholder |
| `AllowClear` | bool | false | Whether to show a clear button |
| `IsReadOnly` | bool | false | Whether the input is read-only |
| `IsDisabled` | bool | false | Whether the input is disabled |
| `ShowCopyButton` | bool | false | Whether to show a copy button |
| `CopyRawValue` | bool | false | Whether to copy the raw value (without mask) when using copy button |
| `AutoFillOnLoad` | bool | false | Whether to automatically apply the mask to the initial value |
| `AllowIncomplete` | bool | false | Whether to allow incomplete values |
| `Size` | Size | Size.None | The size of the input (Small, Medium, Large) |
| `ValidateRules` | List<ValidateRule> | null | Validation rules for the input |
| `Class` | string | null | Additional CSS class for the component |

## Events

| Event | Description |
|-------|-------------|
| `OnValueChanged` | Triggered when the value changes |
| `OnValueChanging` | Triggered before the value changes, can be used to validate or modify the input |
| `OnBlur` | Triggered when the input loses focus |
| `OnFocus` | Triggered when the input gains focus |
| `OnClear` | Triggered when the clear button is clicked |
| `OnCopy` | Triggered when the copy button is clicked |
| `OnKeyDown` | Triggered when a key is pressed down in the input |
| `OnKeyUp` | Triggered when a key is released in the input |

## Usage Examples

### Example 1: Basic Phone Number Mask
```razor
<Mask @bind-Value="PhoneNumber" 
      MaskType="MaskType.Phone" 
      Placeholder="Enter phone number" />

@code {
    private string PhoneNumber { get; set; }
}
```

### Example 2: Custom Date Mask with Validation
```razor
<ValidateForm Model="@Model">
    <BootstrapInput TValue="string" @bind-Value="@Model.BirthDate" placeholder="MM/DD/YYYY">
        <ComponentPlacement>
            <Mask @bind-Value="@Model.BirthDate"
                  Pattern="99/99/9999"
                  PlaceholderChar="_"
                  ShowPlaceholder="true"
                  AllowClear="true" />
        </ComponentPlacement>
    </BootstrapInput>
    
    <Button ButtonType="ButtonType.Submit">Submit</Button>
</ValidateForm>

@code {
    private UserModel Model { get; set; } = new UserModel();
    
    public class UserModel
    {
        [Required]
        [RegularExpression(@"^(0[1-9]|1[0-2])/(0[1-9]|[12][0-9]|3[01])/\d{4}$", 
                          ErrorMessage = "Please enter a valid date in MM/DD/YYYY format")]
        public string BirthDate { get; set; }
    }
}
```

### Example 3: Credit Card Input with Copy Button
```razor
<div class="payment-form">
    <h3>Payment Information</h3>
    
    <div class="form-group">
        <Label Text="Credit Card Number" IsRequired="true" />
        <Mask @bind-Value="@CardNumber"
              Pattern="9999 9999 9999 9999"
              Placeholder="Enter card number"
              ShowCopyButton="true"
              CopyRawValue="true"
              AllowClear="true"
              OnValueChanged="HandleCardNumberChanged" />
    </div>
    
    <div class="form-row">
        <div class="form-group col-md-6">
            <Label Text="Expiration Date" IsRequired="true" />
            <Mask @bind-Value="@ExpirationDate"
                  Pattern="99/99"
                  Placeholder="MM/YY" />
        </div>
        <div class="form-group col-md-6">
            <Label Text="CVV" IsRequired="true" />
            <Mask @bind-Value="@CVV"
                  Pattern="999"
                  Placeholder="CVV" />
        </div>
    </div>
</div>

@code {
    private string CardNumber { get; set; }
    private string ExpirationDate { get; set; }
    private string CVV { get; set; }
    
    private void HandleCardNumberChanged(string value)
    {
        // Detect card type based on number
        if (!string.IsNullOrEmpty(value))
        {
            if (value.StartsWith("4"))
            {
                CardType = "Visa";
            }
            else if (value.StartsWith("5"))
            {
                CardType = "MasterCard";
            }
            // Add more card type detection logic
        }
    }
    
    private string CardType { get; set; }
}
```

### Example 4: Social Security Number with Placeholder
```razor
<div class="form-group">
    <Label Text="Social Security Number" />
    <Mask @bind-Value="@SSN"
          MaskType="MaskType.SSN"
          ShowPlaceholder="true"
          PlaceholderChar="X"
          IsReadOnly="@IsReadOnly"
          OnFocus="HandleFocus"
          OnBlur="HandleBlur" />
</div>

<Button @onclick="ToggleReadOnly">Toggle Read-Only</Button>

@code {
    private string SSN { get; set; }
    private bool IsReadOnly { get; set; } = false;
    
    private void ToggleReadOnly()
    {
        IsReadOnly = !IsReadOnly;
    }
    
    private void HandleFocus()
    {
        Console.WriteLine("Input focused");
    }
    
    private void HandleBlur()
    {
        Console.WriteLine("Input blurred");
    }
}
```

### Example 5: IP Address Input
```razor
<div class="network-config">
    <h4>Network Configuration</h4>
    
    <div class="form-group">
        <Label Text="IP Address" />
        <Mask @bind-Value="@IpAddress"
              Pattern="999.999.999.999"
              Placeholder="Enter IP address"
              AllowIncomplete="false"
              OnValueChanging="ValidateIpAddress" />
    </div>
    
    <div class="form-group">
        <Label Text="Subnet Mask" />
        <Mask @bind-Value="@SubnetMask"
              Pattern="999.999.999.999"
              Placeholder="Enter subnet mask" />
    </div>
    
    <div class="form-group">
        <Label Text="Gateway" />
        <Mask @bind-Value="@Gateway"
              Pattern="999.999.999.999"
              Placeholder="Enter gateway" />
    </div>
</div>

@code {
    private string IpAddress { get; set; }
    private string SubnetMask { get; set; }
    private string Gateway { get; set; }
    
    private Task<string> ValidateIpAddress(string value)
    {
        // Simple validation for IP address segments
        if (!string.IsNullOrEmpty(value))
        {
            var segments = value.Split('.');
            foreach (var segment in segments)
            {
                if (int.TryParse(segment, out int num) && num > 255)
                {
                    // Limit to 255
                    return Task.FromResult(value.Replace(segment, "255"));
                }
            }
        }
        return Task.FromResult(value);
    }
}
```

### Example 6: Time Input with Custom Format
```razor
<div class="time-picker">
    <Label Text="Appointment Time" />
    <Mask @bind-Value="@AppointmentTime"
          Pattern="99:99 aa"
          Placeholder="Enter time (HH:MM AM/PM)"
          ShowPlaceholder="true"
          OnValueChanging="FormatTimeInput" />
</div>

@code {
    private string AppointmentTime { get; set; }
    
    private Task<string> FormatTimeInput(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            // Extract hours from input
            var parts = value.Split(':');
            if (parts.Length > 0 && int.TryParse(parts[0], out int hours))
            {
                // Ensure hours are between 1-12 for 12-hour format
                if (hours > 12)
                {
                    return Task.FromResult(value.Replace(parts[0], "12"));
                }
                else if (hours == 0)
                {
                    return Task.FromResult(value.Replace(parts[0], "01"));
                }
            }
            
            // Format AM/PM part
            if (value.Length >= 6)
            {
                var amPm = value.Substring(value.Length - 2).ToUpper();
                if (amPm != "AM" && amPm != "PM")
                {
                    // Default to AM if invalid
                    return Task.FromResult(value.Substring(0, value.Length - 2) + "AM");
                }
            }
        }
        return Task.FromResult(value);
    }
}
```

### Example 7: Currency Input with Formatting
```razor
<div class="financial-form">
    <h4>Financial Information</h4>
    
    <div class="form-group">
        <Label Text="Amount" />
        <div class="input-group">
            <span class="input-group-text">$</span>
            <Mask @bind-Value="@Amount"
                  Pattern="9,999,999.99"
                  Placeholder="Enter amount"
                  ShowPlaceholder="true"
                  OnValueChanged="HandleAmountChanged" />
        </div>
    </div>
    
    <div class="mt-3">
        <p>Amount in words: <strong>@AmountInWords</strong></p>
    </div>
</div>

@code {
    private string Amount { get; set; }
    private string AmountInWords { get; set; } = "Zero dollars";
    
    private void HandleAmountChanged(string value)
    {
        if (decimal.TryParse(value.Replace(",", ""), out decimal amount))
        {
            // Convert amount to words (simplified example)
            if (amount == 0)
            {
                AmountInWords = "Zero dollars";
            }
            else
            {
                // In a real application, you would use a more comprehensive conversion
                AmountInWords = $"{amount:N2} dollars";
            }
        }
        else
        {
            AmountInWords = "Invalid amount";
        }
    }
}
```

## CSS Customization

The Mask component can be customized using CSS variables and classes:

```css
/* Custom Mask styling */
.bb-mask {
    --bb-mask-border-color: #ced4da;
    --bb-mask-border-radius: 0.25rem;
    --bb-mask-border-width: 1px;
    --bb-mask-padding: 0.375rem 0.75rem;
    --bb-mask-font-size: 1rem;
    --bb-mask-line-height: 1.5;
    --bb-mask-placeholder-color: #6c757d;
    --bb-mask-focus-border-color: #86b7fe;
    --bb-mask-focus-box-shadow: 0 0 0 0.25rem rgba(13, 110, 253, 0.25);
    --bb-mask-disabled-bg: #e9ecef;
    --bb-mask-disabled-color: #6c757d;
    
    border: var(--bb-mask-border-width) solid var(--bb-mask-border-color);
    border-radius: var(--bb-mask-border-radius);
    padding: var(--bb-mask-padding);
    font-size: var(--bb-mask-font-size);
    line-height: var(--bb-mask-line-height);
    transition: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
}

.bb-mask:focus {
    border-color: var(--bb-mask-focus-border-color);
    box-shadow: var(--bb-mask-focus-box-shadow);
    outline: 0;
}

.bb-mask::placeholder {
    color: var(--bb-mask-placeholder-color);
    opacity: 1;
}

.bb-mask:disabled,
.bb-mask[readonly] {
    background-color: var(--bb-mask-disabled-bg);
    color: var(--bb-mask-disabled-color);
    opacity: 1;
}

/* Size variants */
.bb-mask-sm {
    padding: 0.25rem 0.5rem;
    font-size: 0.875rem;
    border-radius: 0.2rem;
}

.bb-mask-lg {
    padding: 0.5rem 1rem;
    font-size: 1.25rem;
    border-radius: 0.3rem;
}

/* Clear button */
.bb-mask-clear-button {
    cursor: pointer;
    color: #6c757d;
    transition: color 0.15s ease-in-out;
}

.bb-mask-clear-button:hover {
    color: #343a40;
}

/* Copy button */
.bb-mask-copy-button {
    cursor: pointer;
    color: #6c757d;
    transition: color 0.15s ease-in-out;
}

.bb-mask-copy-button:hover {
    color: #343a40;
}

/* Placeholder styling */
.bb-mask-placeholder {
    color: #6c757d;
    opacity: 0.65;
}
```

## JavaScript Interop

The Mask component uses JavaScript interop for certain features:

```javascript
// This is a simplified example of the JavaScript interop used by the component
window.bootstrapBlazorMask = {
    // Initialize mask on an input element
    init: function(element, maskPattern, placeholderChar, showPlaceholder) {
        if (!element) return false;
        
        // Store original input handlers
        const originalInputHandler = element.oninput;
        const originalKeyDownHandler = element.onkeydown;
        
        // Apply mask pattern to the element
        element.dataset.maskPattern = maskPattern;
        element.dataset.placeholderChar = placeholderChar;
        element.dataset.showPlaceholder = showPlaceholder;
        
        // Set up input handler
        element.oninput = function(e) {
            const value = e.target.value;
            const formattedValue = this.formatValue(element, value);
            
            // Update the input value if it changed
            if (formattedValue !== value) {
                e.target.value = formattedValue;
            }
            
            // Call original handler if exists
            if (typeof originalInputHandler === 'function') {
                originalInputHandler.call(element, e);
            }
        }.bind(this);
        
        // Set up keydown handler for special keys
        element.onkeydown = function(e) {
            // Handle special keys (backspace, delete, etc.)
            
            // Call original handler if exists
            if (typeof originalKeyDownHandler === 'function') {
                originalKeyDownHandler.call(element, e);
            }
        };
        
        // Initial formatting
        if (element.value) {
            element.value = this.formatValue(element, element.value);
        } else if (showPlaceholder) {
            // Show placeholder if enabled
            this.showMaskPlaceholder(element);
        }
        
        return true;
    },
    
    // Format value according to mask pattern
    formatValue: function(element, value) {
        if (!element || !element.dataset.maskPattern) return value;
        
        const pattern = element.dataset.maskPattern;
        const placeholderChar = element.dataset.placeholderChar || '_';
        let result = '';
        let valueIndex = 0;
        
        // Process each character in the pattern
        for (let i = 0; i < pattern.length; i++) {
            const patternChar = pattern[i];
            
            if (valueIndex >= value.length) {
                // End of input value reached
                break;
            }
            
            const valueChar = value[valueIndex];
            
            if (patternChar === '9') {
                // Digit placeholder
                if (/\d/.test(valueChar)) {
                    result += valueChar;
                    valueIndex++;
                } else {
                    // Skip non-digit character in input
                    valueIndex++;
                    i--; // Retry with next input character
                }
            } else if (patternChar === 'a') {
                // Letter placeholder
                if (/[a-zA-Z]/.test(valueChar)) {
                    result += valueChar;
                    valueIndex++;
                } else {
                    // Skip non-letter character in input
                    valueIndex++;
                    i--; // Retry with next input character
                }
            } else if (patternChar === '*') {
                // Any character placeholder
                result += valueChar;
                valueIndex++;
            } else {
                // Literal character in pattern
                result += patternChar;
                
                // Skip input character if it matches the pattern character
                if (valueChar === patternChar) {
                    valueIndex++;
                }
            }
        }
        
        return result;
    },
    
    // Show mask placeholder
    showMaskPlaceholder: function(element) {
        if (!element || !element.dataset.maskPattern) return;
        
        const pattern = element.dataset.maskPattern;
        const placeholderChar = element.dataset.placeholderChar || '_';
        let placeholder = '';
        
        // Create placeholder based on pattern
        for (let i = 0; i < pattern.length; i++) {
            const patternChar = pattern[i];
            
            if (patternChar === '9' || patternChar === 'a' || patternChar === '*') {
                placeholder += placeholderChar;
            } else {
                placeholder += patternChar;
            }
        }
        
        // Set placeholder attribute
        element.setAttribute('placeholder', placeholder);
    },
    
    // Copy value to clipboard
    copyToClipboard: function(element, copyRawValue) {
        if (!element) return false;
        
        let textToCopy = element.value;
        
        if (copyRawValue) {
            // Extract raw value (digits only) for certain mask types
            textToCopy = textToCopy.replace(/[^0-9]/g, '');
        }
        
        // Use clipboard API if available
        if (navigator.clipboard && window.isSecureContext) {
            navigator.clipboard.writeText(textToCopy)
                .then(() => console.log('Text copied to clipboard'))
                .catch(err => console.error('Error copying text: ', err));
        } else {
            // Fallback for older browsers
            const textArea = document.createElement('textarea');
            textArea.value = textToCopy;
            textArea.style.position = 'fixed';
            textArea.style.opacity = '0';
            document.body.appendChild(textArea);
            textArea.select();
            
            try {
                document.execCommand('copy');
                console.log('Text copied to clipboard');
            } catch (err) {
                console.error('Error copying text: ', err);
            }
            
            document.body.removeChild(textArea);
        }
        
        return true;
    },
    
    // Clear input value
    clearValue: function(element) {
        if (!element) return false;
        
        element.value = '';
        element.dispatchEvent(new Event('input', { bubbles: true }));
        element.focus();
        
        return true;
    },
    
    // Dispose mask functionality
    dispose: function(element) {
        if (!element) return false;
        
        // Remove data attributes
        delete element.dataset.maskPattern;
        delete element.dataset.placeholderChar;
        delete element.dataset.showPlaceholder;
        
        // Remove event handlers (would need to store references to original handlers)
        element.oninput = null;
        element.onkeydown = null;
        
        return true;
    }
};
```

## Accessibility

The Mask component follows accessibility best practices:

- Uses semantic HTML elements for inputs
- Provides proper ARIA attributes for interactive elements
- Ensures keyboard navigation support
- Maintains focus management during interactions
- Provides clear visual feedback for different states
- Supports screen readers with appropriate labels and descriptions

## Browser Compatibility

The Mask component is compatible with all modern browsers, including:

- Chrome
- Firefox
- Safari
- Edge

## Integration with Other Components

The Mask component works well with many other BootstrapBlazor components:

- Use with `Form` and `ValidateForm` components for form validation
- Combine with `Input` and `BootstrapInput` for enhanced input capabilities
- Use with `Label` for proper form field labeling
- Integrate with `Button` components for actions related to the masked input
- Use with `Tooltip` to provide additional information about the expected format
- Combine with `Popover` for more detailed input instructions