# Form Component Documentation

## Overview
The Form component in BootstrapBlazor provides a structured container for collecting user input through various form controls. It offers a consistent way to organize form elements, handle form submissions, and manage form validation. This component simplifies the process of creating forms with proper layout, styling, and behavior, making it easier to build user-friendly interfaces for data collection and submission.

## Features
- Flexible layout options (horizontal, vertical, inline)
- Built-in form validation support
- Automatic form control binding
- Customizable label width and alignment
- Form submission handling
- Reset functionality
- Responsive design
- Integration with BootstrapBlazor form controls
- Support for complex nested form structures
- Customizable validation display
- Accessibility features
- Localization support

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Model | object | null | The data model to bind form controls to |
| ValidateAllProperties | bool | false | Whether to validate all properties when the form is submitted |
| LabelAlign | Alignment | Right | The alignment of form labels |
| LabelWidth | int | 120 | The width of form labels in pixels |
| RowType | RowType | Normal | The type of row layout (Normal, Inline, Condensed) |
| IsInline | bool | false | Whether to display the form inline |
| IsHorizontal | bool | true | Whether to display the form horizontally |
| ShowLabel | bool | true | Whether to show labels for form controls |
| ShowUnderLine | bool | false | Whether to show underlines for form controls |
| ShowRequiredMark | bool | true | Whether to show required marks for required fields |
| RequiredMarkText | string | "*" | The text to use for required marks |
| ShowValidateResult | bool | true | Whether to show validation results |
| ShowLoading | bool | false | Whether to show a loading indicator during form submission |
| LoadingText | string | "Loading..." | The text to display during loading |
| ValidateForm | bool | true | Whether to validate the form on submission |
| OnValidSubmit | EventCallback | - | Callback when the form is submitted with valid data |
| OnInvalidSubmit | EventCallback | - | Callback when the form is submitted with invalid data |
| OnSubmit | EventCallback | - | Callback when the form is submitted regardless of validation |
| OnReset | EventCallback | - | Callback when the form is reset |

## Events

| Event | Description |
| --- | --- |
| OnValidSubmit | Triggered when the form is submitted with valid data |
| OnInvalidSubmit | Triggered when the form is submitted with invalid data |
| OnSubmit | Triggered when the form is submitted regardless of validation |
| OnReset | Triggered when the form is reset |

## Usage Examples

### Example 1: Basic Form

```razor
<Form Model="@model" OnValidSubmit="@HandleValidSubmit">
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Name" placeholder="Enter your name" />
    </div>
    
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Email" placeholder="Enter your email" />
    </div>
    
    <div class="mb-3">
        <Checkbox @bind-Value="@model.Subscribe" Text="Subscribe to newsletter" />
    </div>
    
    <Button Type="ButtonType.Submit">Submit</Button>
    <Button Type="ButtonType.Reset">Reset</Button>
</Form>

@code {
    private UserModel model = new UserModel();
    
    private void HandleValidSubmit()
    {
        // Process the form data
        Console.WriteLine($"Name: {model.Name}");
        Console.WriteLine($"Email: {model.Email}");
        Console.WriteLine($"Subscribe: {model.Subscribe}");
    }
    
    public class UserModel
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public bool Subscribe { get; set; } = false;
    }
}
```

### Example 2: Form with Validation

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
    
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.ConfirmPassword" type="password" placeholder="Confirm your password" />
        <ValidationMessage For="@(() => model.ConfirmPassword)" />
    </div>
    
    <Button Type="ButtonType.Submit">Register</Button>
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
        
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = "";
    }
}
```

### Example 3: Horizontal Form Layout

```razor
<ValidateForm Model="@model" OnValidSubmit="@HandleValidSubmit" IsHorizontal="true" LabelWidth="120">
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
    
    <div class="mb-3">
        <div class="offset-sm-3">
            <Button Type="ButtonType.Submit">Submit</Button>
            <Button Type="ButtonType.Reset">Reset</Button>
        </div>
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

### Example 4: Inline Form

```razor
<ValidateForm Model="@model" OnValidSubmit="@HandleValidSubmit" IsInline="true">
    <div class="d-flex align-items-center">
        <div class="me-2">
            <BootstrapInput @bind-Value="@model.SearchTerm" placeholder="Search term" />
        </div>
        
        <div class="me-2">
            <Select @bind-Value="@model.Category" PlaceHolder="Category">
                <Options>
                    <SelectOption Text="All" Value="all" />
                    <SelectOption Text="Products" Value="products" />
                    <SelectOption Text="Articles" Value="articles" />
                    <SelectOption Text="Users" Value="users" />
                </Options>
            </Select>
        </div>
        
        <Button Type="ButtonType.Submit">Search</Button>
    </div>
</ValidateForm>

@code {
    private SearchModel model = new SearchModel();
    
    private void HandleValidSubmit()
    {
        // Process the search
        Console.WriteLine($"Searching for '{model.SearchTerm}' in category '{model.Category}'");
    }
    
    public class SearchModel
    {
        public string SearchTerm { get; set; } = "";
        public string Category { get; set; } = "all";
    }
}
```

### Example 5: Form with Custom Validation

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
                    <BootstrapInput @bind-Value="@addressInfo.Street" ShowLabel="true" DisplayText="Street" />
                    <ValidationMessage For="@(() => addressInfo.Street)" />
                </div>
                
                <div class="mb-3">
                    <BootstrapInput @bind-Value="@addressInfo.City" ShowLabel="true" DisplayText="City" />
                    <ValidationMessage For="@(() => addressInfo.City)" />
                </div>
                
                <div class="mb-3">
                    <BootstrapInput @bind-Value="@addressInfo.State" ShowLabel="true" DisplayText="State/Province" />
                    <ValidationMessage For="@(() => addressInfo.State)" />
                </div>
                
                <div class="mb-3">
                    <BootstrapInput @bind-Value="@addressInfo.ZipCode" ShowLabel="true" DisplayText="Zip/Postal Code" />
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
                <Button Type="ButtonType.Submit">Submit</Button>
            </ValidateForm>
        }
    </div>
</div>

@if (isSubmitted)
{
    <div class="alert alert-success mt-3">
        <h5>Form Submitted Successfully!</h5>
        <p>Thank you for your submission, @personalInfo.FirstName @personalInfo.LastName.</p>
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
        // Process the complete form data
        Console.WriteLine("Form submitted successfully");
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
        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; } = "";
        
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = "";
        
        [Required(ErrorMessage = "State/Province is required")]
        public string State { get; set; } = "";
        
        [Required(ErrorMessage = "Zip/Postal code is required")]
        public string ZipCode { get; set; } = "";
    }
    
    public class PaymentInfoModel
    {
        [Required(ErrorMessage = "Card number is required")]
        [CreditCard(ErrorMessage = "Invalid credit card number")]
        public string CardNumber { get; set; } = "";
        
        [Required(ErrorMessage = "Expiry date is required")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/([0-9]{2})$", ErrorMessage = "Expiry date must be in MM/YY format")]
        public string ExpiryDate { get; set; } = "";
        
        [Required(ErrorMessage = "CVV is required")]
        [RegularExpression(@"^[0-9]{3,4}$", ErrorMessage = "CVV must be 3 or 4 digits")]
        public string CVV { get; set; } = "";
        
        [Required(ErrorMessage = "Cardholder name is required")]
        public string CardholderName { get; set; } = "";
    }
}
```

### Example 7: Dynamic Form

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
        Console.WriteLine($"Price: {model.Price:C2}");
        Console.WriteLine("Attributes:");
        
        foreach (var attribute in model.Attributes)
        {
            Console.WriteLine($"- {attribute.Name}: {attribute.Value}");
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
        public decimal Price { get; set; } = 0;
        
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

The Form component can be customized using the following CSS variables:

```css
:root {
    --bb-form-label-width: 120px;
    --bb-form-label-align: right;
    --bb-form-label-color: #212529;
    --bb-form-label-font-weight: 400;
    --bb-form-control-width: auto;
    --bb-form-control-border-color: #ced4da;
    --bb-form-control-border-radius: 0.25rem;
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

Additionally, you can customize the appearance and behavior of the Form component by:

1. Using the `IsHorizontal` and `IsInline` properties to control the form layout
2. Using the `LabelWidth` and `LabelAlign` properties to customize label appearance
3. Using the `ShowLabel`, `ShowUnderLine`, and `ShowRequiredMark` properties to control visual elements
4. Using the `ShowValidateResult` property to control validation display
5. Using the `ShowLoading` and `LoadingText` properties to customize loading behavior
6. Applying custom CSS classes to the component using the `ClassName` property