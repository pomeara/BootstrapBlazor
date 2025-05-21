# ValidateForm Component Documentation

## Overview
The ValidateForm component in BootstrapBlazor provides a powerful way to create forms with built-in validation capabilities. It extends the standard form functionality by integrating with .NET's data annotation validation system, allowing developers to define validation rules through model attributes. This component streamlines the process of collecting and validating user input, providing immediate feedback on validation errors, and handling form submission events. It's a cornerstone component for creating robust, user-friendly forms in Blazor applications.

## Features
- **Data Annotation Validation**: Automatic validation based on model attributes
- **Custom Validation Support**: Implement custom validation logic with IValidatableObject
- **Immediate Feedback**: Real-time validation as users interact with form fields
- **Flexible Layout Options**: Horizontal, vertical, and inline form layouts
- **Validation Summary**: Optional display of all validation errors in one place
- **Validation Styling**: Automatic visual indication of valid/invalid fields
- **Form Submission Handling**: Events for valid and invalid submission attempts
- **Model Binding**: Two-way binding with form control values
- **Loading State**: Built-in loading indicator during async operations
- **Reset Functionality**: Easy form reset capabilities
- **Localization Support**: Validation messages in multiple languages
- **Accessibility Features**: Screen reader compatibility and keyboard navigation

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Model | object | null | The data model to bind form controls to |
| OnValidSubmit | EventCallback | - | Callback when the form is submitted and validation passes |
| OnInvalidSubmit | EventCallback | - | Callback when the form is submitted but validation fails |
| OnSubmit | EventCallback | - | Callback when the form is submitted, regardless of validation |
| ValidateAllProperties | bool | false | Whether to validate all properties when the form is submitted |
| ShowRequiredMark | bool | true | Whether to show an asterisk for required fields |
| ShowValidationSummary | bool | false | Whether to show a summary of all validation errors |
| ShowValidationMessage | bool | true | Whether to show individual validation messages |
| ShowLoading | bool | false | Whether to show a loading indicator during async operations |
| LoadingText | string | "Loading..." | Text to display when loading |
| IsInline | bool | false | Whether to display the form inline |
| IsHorizontal | bool | false | Whether to display the form with horizontal layout |
| LabelAlign | Alignment | Right | The alignment of form labels |
| LabelWidth | int | 120 | The width of form labels in pixels |
| ClassName | string | "" | Additional CSS class for the component |

## Events

| Event | Description |
| --- | --- |
| OnValidSubmit | Triggered when the form is submitted and validation passes |
| OnInvalidSubmit | Triggered when the form is submitted but validation fails |
| OnSubmit | Triggered when the form is submitted, regardless of validation |
| OnReset | Triggered when the form is reset |
| OnValidationStateChanged | Triggered when the validation state changes |

## Usage Examples

### Example 1: Basic Form Validation

```razor
<ValidateForm Model="@model" OnValidSubmit="@HandleValidSubmit">
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Name" placeholder="Enter your name" />
        <ValidationMessage For="@(() => model.Name)" />
    </div>
    
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Email" placeholder="Enter your email" />
        <ValidationMessage For="@(() => model.Email)" />
    </div>
    
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Password" type="password" placeholder="Enter your password" />
        <ValidationMessage For="@(() => model.Password)" />
    </div>
    
    <Button Type="ButtonType.Submit">Register</Button>
    <Button Type="ButtonType.Reset">Reset</Button>
</ValidateForm>

@code {
    private RegisterModel model = new RegisterModel();
    
    private void HandleValidSubmit()
    {
        // Process the registration
        Console.WriteLine("Registration successful");
    }
    
    public class RegisterModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; } = "";
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = "";
        
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Password must be at least {2} characters long", MinimumLength = 6)]
        public string Password { get; set; } = "";
    }
}
```

### Example 2: Form with Custom Validation

```razor
<ValidateForm Model="@model" OnValidSubmit="@HandleValidSubmit">
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Username" placeholder="Username" />
        <ValidationMessage For="@(() => model.Username)" />
    </div>
    
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Email" placeholder="Email" />
        <ValidationMessage For="@(() => model.Email)" />
    </div>
    
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.PhoneNumber" placeholder="Phone Number" />
        <ValidationMessage For="@(() => model.PhoneNumber)" />
    </div>
    
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Website" placeholder="Website" />
        <ValidationMessage For="@(() => model.Website)" />
    </div>
    
    <Button Type="ButtonType.Submit">Submit</Button>
</ValidateForm>

@code {
    private ContactModel model = new ContactModel();
    
    private void HandleValidSubmit()
    {
        // Process the contact information
        Console.WriteLine("Contact information saved");
    }
    
    public class ContactModel : IValidatableObject
    {
        [Required(ErrorMessage = "Username is required")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username can only contain letters, numbers, and underscores")]
        public string Username { get; set; } = "";
        
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = "";
        
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string PhoneNumber { get; set; } = "";
        
        [Url(ErrorMessage = "Invalid website URL")]
        public string Website { get; set; } = "";
        
        // Custom validation logic
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // At least one contact method must be provided
            if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(PhoneNumber) && string.IsNullOrEmpty(Website))
            {
                yield return new ValidationResult(
                    "At least one contact method (Email, Phone, or Website) must be provided",
                    new[] { nameof(Email), nameof(PhoneNumber), nameof(Website) });
            }
        }
    }
}
```

### Example 3: Horizontal Form Layout

```razor
<ValidateForm Model="@model" OnValidSubmit="@HandleValidSubmit" IsHorizontal="true" LabelWidth="150">
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.FirstName" ShowLabel="true" DisplayText="First Name" />
        <ValidationMessage For="@(() => model.FirstName)" />
    </div>
    
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.LastName" ShowLabel="true" DisplayText="Last Name" />
        <ValidationMessage For="@(() => model.LastName)" />
    </div>
    
    <div class="mb-3">
        <Select @bind-Value="@model.Country" ShowLabel="true" DisplayText="Country">
            <Options>
                <SelectOption Text="United States" Value="US" />
                <SelectOption Text="Canada" Value="CA" />
                <SelectOption Text="United Kingdom" Value="UK" />
                <SelectOption Text="Australia" Value="AU" />
            </Options>
        </Select>
        <ValidationMessage For="@(() => model.Country)" />
    </div>
    
    <div class="mb-3">
        <DateTimePicker @bind-Value="@model.BirthDate" ShowLabel="true" DisplayText="Birth Date" />
        <ValidationMessage For="@(() => model.BirthDate)" />
    </div>
    
    <div class="mb-3 offset-sm-3">
        <Button Type="ButtonType.Submit">Submit</Button>
        <Button Type="ButtonType.Reset">Reset</Button>
    </div>
</ValidateForm>

@code {
    private ProfileModel model = new ProfileModel();
    
    private void HandleValidSubmit()
    {
        // Process the profile data
        Console.WriteLine($"Profile saved for {model.FirstName} {model.LastName}");
    }
    
    public class ProfileModel
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; } = "";
        
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; } = "";
        
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; } = "";
        
        [Required(ErrorMessage = "Birth date is required")]
        public DateTime? BirthDate { get; set; }
    }
}
```

### Example 4: Form with Validation Summary

```razor
<ValidateForm Model="@model" OnValidSubmit="@HandleValidSubmit" ShowValidationSummary="true">
    <ValidationSummary />
    
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Username" placeholder="Username" />
        <ValidationMessage For="@(() => model.Username)" />
    </div>
    
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Password" type="password" placeholder="Password" />
        <ValidationMessage For="@(() => model.Password)" />
    </div>
    
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.ConfirmPassword" type="password" placeholder="Confirm Password" />
        <ValidationMessage For="@(() => model.ConfirmPassword)" />
    </div>
    
    <Button Type="ButtonType.Submit">Register</Button>
</ValidateForm>

@code {
    private RegistrationModel model = new RegistrationModel();
    
    private void HandleValidSubmit()
    {
        // Process the registration
        Console.WriteLine("Registration successful");
    }
    
    public class RegistrationModel : IValidatableObject
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(20, ErrorMessage = "Username must be between {2} and {1} characters", MinimumLength = 3)]
        public string Username { get; set; } = "";
        
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Password must be at least {2} characters long", MinimumLength = 8)]
        public string Password { get; set; } = "";
        
        [Required(ErrorMessage = "Confirm Password is required")]
        public string ConfirmPassword { get; set; } = "";
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Password != ConfirmPassword)
            {
                yield return new ValidationResult(
                    "The password and confirmation password do not match.",
                    new[] { nameof(ConfirmPassword) });
            }
        }
    }
}
```

### Example 5: Async Form Submission with Loading State

```razor
<ValidateForm Model="@model" OnValidSubmit="@HandleValidSubmitAsync" ShowLoading="@isLoading" LoadingText="Submitting...">
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Title" placeholder="Article title" />
        <ValidationMessage For="@(() => model.Title)" />
    </div>
    
    <div class="mb-3">
        <Textarea @bind-Value="@model.Content" placeholder="Article content" Rows="5" />
        <ValidationMessage For="@(() => model.Content)" />
    </div>
    
    <div class="mb-3">
        <Select @bind-Value="@model.Category" placeholder="Select category">
            <Options>
                <SelectOption Text="Technology" Value="tech" />
                <SelectOption Text="Science" Value="science" />
                <SelectOption Text="Health" Value="health" />
                <SelectOption Text="Business" Value="business" />
            </Options>
        </Select>
        <ValidationMessage For="@(() => model.Category)" />
    </div>
    
    <Button Type="ButtonType.Submit">Publish Article</Button>
</ValidateForm>

@code {
    private ArticleModel model = new ArticleModel();
    private bool isLoading = false;
    
    private async Task HandleValidSubmitAsync()
    {
        isLoading = true;
        
        try
        {
            // Simulate API call
            await Task.Delay(2000);
            
            // Process the article submission
            Console.WriteLine($"Article published: {model.Title}");
            Console.WriteLine($"Category: {model.Category}");
            Console.WriteLine($"Content length: {model.Content.Length}");
        }
        finally
        {
            isLoading = false;
        }
    }
    
    public class ArticleModel
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; } = "";
        
        [Required(ErrorMessage = "Content is required")]
        [MinLength(50, ErrorMessage = "Content must be at least 50 characters")]
        public string Content { get; set; } = "";
        
        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; } = "";
    }
}
```

### Example 6: Multi-Step Form

```razor
<div class="card">
    <div class="card-header">
        <h4>Multi-Step Form</h4>
        <div class="progress">
            <div class="progress-bar" role="progressbar" style="width: @(currentStep * 33.33)%" aria-valuenow="@(currentStep * 33.33)" aria-valuemin="0" aria-valuemax="100">Step @currentStep of 3</div>
        </div>
    </div>
    
    <div class="card-body">
        @if (currentStep == 1)
        {
            <ValidateForm Model="@personalInfo" OnValidSubmit="@(() => NextStep())">
                <h5>Step 1: Personal Information</h5>
                
                <div class="mb-3">
                    <BootstrapInput @bind-Value="@personalInfo.FirstName" ShowLabel="true" DisplayText="First Name" />
                    <ValidationMessage For="@(() => personalInfo.FirstName)" />
                </div>
                
                <div class="mb-3">
                    <BootstrapInput @bind-Value="@personalInfo.LastName" ShowLabel="true" DisplayText="Last Name" />
                    <ValidationMessage For="@(() => personalInfo.LastName)" />
                </div>
                
                <div class="mb-3">
                    <BootstrapInput @bind-Value="@personalInfo.Email" ShowLabel="true" DisplayText="Email" />
                    <ValidationMessage For="@(() => personalInfo.Email)" />
                </div>
                
                <Button Type="ButtonType.Submit">Next</Button>
            </ValidateForm>
        }
        else if (currentStep == 2)
        {
            <ValidateForm Model="@addressInfo" OnValidSubmit="@(() => NextStep())">
                <h5>Step 2: Address Information</h5>
                
                <div class="mb-3">
                    <BootstrapInput @bind-Value="@addressInfo.Street" ShowLabel="true" DisplayText="Street Address" />
                    <ValidationMessage For="@(() => addressInfo.Street)" />
                </div>
                
                <div class="mb-3">
                    <BootstrapInput @bind-Value="@addressInfo.City" ShowLabel="true" DisplayText="City" />
                    <ValidationMessage For="@(() => addressInfo.City)" />
                </div>
                
                <div class="mb-3">
                    <BootstrapInput @bind-Value="@addressInfo.ZipCode" ShowLabel="true" DisplayText="Zip Code" />
                    <ValidationMessage For="@(() => addressInfo.ZipCode)" />
                </div>
                
                <Button OnClick="@(() => currentStep--)" Color="Color.Secondary">Previous</Button>
                <Button Type="ButtonType.Submit">Next</Button>
            </ValidateForm>
        }
        else if (currentStep == 3)
        {
            <ValidateForm Model="@paymentInfo" OnValidSubmit="@HandleFinalSubmit">
                <h5>Step 3: Payment Information</h5>
                
                <div class="mb-3">
                    <BootstrapInput @bind-Value="@paymentInfo.CardNumber" ShowLabel="true" DisplayText="Card Number" />
                    <ValidationMessage For="@(() => paymentInfo.CardNumber)" />
                </div>
                
                <div class="row mb-3">
                    <div class="col-md-6">
                        <BootstrapInput @bind-Value="@paymentInfo.ExpiryDate" ShowLabel="true" DisplayText="Expiry Date (MM/YY)" />
                        <ValidationMessage For="@(() => paymentInfo.ExpiryDate)" />
                    </div>
                    
                    <div class="col-md-6">
                        <BootstrapInput @bind-Value="@paymentInfo.CVV" ShowLabel="true" DisplayText="CVV" />
                        <ValidationMessage For="@(() => paymentInfo.CVV)" />
                    </div>
                </div>
                
                <div class="mb-3">
                    <BootstrapInput @bind-Value="@paymentInfo.CardholderName" ShowLabel="true" DisplayText="Cardholder Name" />
                    <ValidationMessage For="@(() => paymentInfo.CardholderName)" />
                </div>
                
                <Button OnClick="@(() => currentStep--)" Color="Color.Secondary">Previous</Button>
                <Button Type="ButtonType.Submit" Color="Color.Success">Complete Order</Button>
            </ValidateForm>
        }
    </div>
</div>

@if (isSubmitted)
{
    <div class="alert alert-success mt-3">
        <h5>Order Completed Successfully!</h5>
        <p>Thank you for your order, @personalInfo.FirstName @personalInfo.LastName.</p>
    </div>
}

@code {
    private int currentStep = 1;
    private bool isSubmitted = false;
    
    private PersonalInfoModel personalInfo = new PersonalInfoModel();
    private AddressInfoModel addressInfo = new AddressInfoModel();
    private PaymentInfoModel paymentInfo = new PaymentInfoModel();
    
    private void NextStep()
    {
        currentStep++;
    }
    
    private void HandleFinalSubmit()
    {
        // Process the complete order
        Console.WriteLine("Order submitted");
        Console.WriteLine($"Customer: {personalInfo.FirstName} {personalInfo.LastName}");
        Console.WriteLine($"Email: {personalInfo.Email}");
        Console.WriteLine($"Address: {addressInfo.Street}, {addressInfo.City}, {addressInfo.ZipCode}");
        Console.WriteLine($"Payment: Card ending in {paymentInfo.CardNumber.Substring(paymentInfo.CardNumber.Length - 4)}");
        
        isSubmitted = true;
    }
    
    public class PersonalInfoModel
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; } = "";
        
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; } = "";
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = "";
    }
    
    public class AddressInfoModel
    {
        [Required(ErrorMessage = "Street address is required")]
        public string Street { get; set; } = "";
        
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = "";
        
        [Required(ErrorMessage = "Zip code is required")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid zip code format")]
        public string ZipCode { get; set; } = "";
    }
    
    public class PaymentInfoModel
    {
        [Required(ErrorMessage = "Card number is required")]
        [CreditCard(ErrorMessage = "Invalid credit card number")]
        public string CardNumber { get; set; } = "";
        
        [Required(ErrorMessage = "Expiry date is required")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/\d{2}$", ErrorMessage = "Invalid expiry date format (MM/YY)")]
        public string ExpiryDate { get; set; } = "";
        
        [Required(ErrorMessage = "CVV is required")]
        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "Invalid CVV format")]
        public string CVV { get; set; } = "";
        
        [Required(ErrorMessage = "Cardholder name is required")]
        public string CardholderName { get; set; } = "";
    }
}
```

### Example 7: Dynamic Form with Validation

```razor
<ValidateForm Model="@model" OnValidSubmit="@HandleValidSubmit">
    <h5>Product Information</h5>
    
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.ProductName" ShowLabel="true" DisplayText="Product Name" />
        <ValidationMessage For="@(() => model.ProductName)" />
    </div>
    
    <div class="mb-3">
        <Select @bind-Value="@model.Category" ShowLabel="true" DisplayText="Category">
            <Options>
                <SelectOption Text="Electronics" Value="electronics" />
                <SelectOption Text="Clothing" Value="clothing" />
                <SelectOption Text="Books" Value="books" />
                <SelectOption Text="Home & Kitchen" Value="home" />
            </Options>
        </Select>
        <ValidationMessage For="@(() => model.Category)" />
    </div>
    
    <div class="mb-3">
        <InputNumber @bind-Value="@model.Price" ShowLabel="true" DisplayText="Price" />
        <ValidationMessage For="@(() => model.Price)" />
    </div>
    
    <h5>Product Attributes</h5>
    
    @foreach (var attribute in model.Attributes)
    {
        <div class="mb-3 border p-3">
            <div class="d-flex justify-content-between align-items-center mb-2">
                <h6>Attribute #@(model.Attributes.IndexOf(attribute) + 1)</h6>
                <Button OnClick="@(() => RemoveAttribute(attribute))" Color="Color.Danger" Size="Size.Small">Remove</Button>
            </div>
            
            <div class="mb-2">
                <BootstrapInput @bind-Value="@attribute.Name" ShowLabel="true" DisplayText="Attribute Name" />
            </div>
            
            <div class="mb-2">
                <BootstrapInput @bind-Value="@attribute.Value" ShowLabel="true" DisplayText="Attribute Value" />
            </div>
        </div>
    }
    
    <div class="mb-3">
        <Button OnClick="@AddAttribute" Color="Color.Secondary">Add Attribute</Button>
    </div>
    
    <Button Type="ButtonType.Submit">Save Product</Button>
</ValidateForm>

@code {
    private ProductModel model = new ProductModel
    {
        Attributes = new List<ProductAttribute>
        {
            new ProductAttribute()
        }
    };
    
    private void AddAttribute()
    {
        model.Attributes.Add(new ProductAttribute());
    }
    
    private void RemoveAttribute(ProductAttribute attribute)
    {
        model.Attributes.Remove(attribute);
    }
    
    private void HandleValidSubmit()
    {
        // Process the product data
        Console.WriteLine($"Product saved: {model.ProductName}");
        Console.WriteLine($"Category: {model.Category}");
        Console.WriteLine($"Price: {model.Price}");
        Console.WriteLine($"Attributes: {model.Attributes.Count}");
        
        foreach (var attr in model.Attributes)
        {
            Console.WriteLine($"  {attr.Name}: {attr.Value}");
        }
    }
    
    public class ProductModel
    {
        [Required(ErrorMessage = "Product name is required")]
        public string ProductName { get; set; } = "";
        
        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; } = "";
        
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 10000, ErrorMessage = "Price must be between $0.01 and $10,000")]
        public decimal Price { get; set; }
        
        public List<ProductAttribute> Attributes { get; set; } = new List<ProductAttribute>();
    }
    
    public class ProductAttribute
    {
        public string Name { get; set; } = "";
        public string Value { get; set; } = "";
    }
}
```

## Customization Notes

The ValidateForm component can be customized using the following CSS variables:

```css
:root {
    --bb-form-control-border-color: #ced4da;
    --bb-form-control-border-radius: 0.25rem;
    --bb-form-control-padding: 0.375rem 0.75rem;
    --bb-form-control-font-size: 1rem;
    --bb-form-control-line-height: 1.5;
    --bb-form-control-color: #212529;
    --bb-form-control-bg: #fff;
    --bb-form-control-focus-border-color: #86b7fe;
    --bb-form-control-focus-box-shadow: 0 0 0 0.25rem rgba(13, 110, 253, 0.25);
    --bb-form-control-disabled-background: #e9ecef;
    --bb-form-control-disabled-color: #6c757d;
    --bb-form-required-color: #dc3545;
    --bb-form-validation-invalid-color: #dc3545;
    --bb-form-validation-valid-color: #198754;
    --bb-form-validation-message-font-size: 0.875rem;
    --bb-form-validation-message-margin-top: 0.25rem;
}
```

Additionally, you can customize the appearance and behavior of the ValidateForm component by:

1. Using the `IsHorizontal` and `IsInline` properties to control the form layout
2. Using the `LabelWidth` and `LabelAlign` properties to customize label appearance
3. Using the `ShowRequiredMark` property to control the display of required field indicators
4. Using the `ShowValidationSummary` and `ShowValidationMessage` properties to control validation feedback
5. Using the `ShowLoading` and `LoadingText` properties to customize loading behavior
6. Applying custom CSS classes to the component using the `ClassName` property