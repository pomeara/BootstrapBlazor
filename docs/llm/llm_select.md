# Select Component Documentation

## Overview
The Select component in BootstrapBlazor provides an enhanced dropdown selection control for choosing one or multiple options from a list. It extends the standard HTML select element with additional features such as search functionality, multi-select capability, and custom option rendering. This component is essential for forms and interfaces where users need to make selections from predefined options in a space-efficient manner.

## Features
- Single and multiple selection modes
- Two-way data binding
- Search/filter functionality
- Option grouping
- Custom option templates
- Placeholder text support
- Clear button option
- Virtualization for large option lists
- Disabled and readonly states
- Size variants (small, medium, large)
- Form validation integration
- Remote data loading
- Keyboard navigation support
- Localization support

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Value | TValue | default(TValue) | Gets or sets the selected value |
| ValueChanged | EventCallback<TValue> | - | Callback when the selected value changes |
| ValueExpression | Expression<Func<TValue>> | - | Expression for the bound value |
| Items | IEnumerable<TItem> | null | Collection of items to display in the dropdown |
| IsMultiple | bool | false | Whether multiple selection is allowed |
| ShowSearch | bool | false | Whether to show search functionality |
| ShowClear | bool | false | Whether to show a clear button |
| Placeholder | string | "" | Placeholder text when no value is selected |
| IsDisabled | bool | false | Whether the select is disabled |
| IsReadOnly | bool | false | Whether the select is read-only |
| Size | Size | Medium | The size of the select (Small, Medium, Large) |
| ShowLabel | bool | true | Whether to show the label |
| DisplayText | string | null | The text to display as the label |
| ShowRequiredMark | bool | true | Whether to show a required mark for required fields |
| RequiredMarkText | string | "*" | The text to use for the required mark |
| MaxTagCount | int? | null | Maximum number of tags to display for multiple selection |
| MaxItemCount | int? | null | Maximum number of items to display in the dropdown |
| NoDataText | string | "No Data" | Text to display when there are no options |
| LoadingText | string | "Loading..." | Text to display during data loading |
| OnLoadItems | Func<Task<IEnumerable<TItem>>> | null | Callback to load items asynchronously |
| OnSearchTextChanged | Func<string, Task> | null | Callback when the search text changes |

## Events

| Event | Description |
| --- | --- |
| OnValueChanged | Triggered when the selected value changes |
| OnSelectedItemChanged | Triggered when the selected item changes |
| OnDropdownVisibleChanged | Triggered when the dropdown visibility changes |
| OnClear | Triggered when the clear button is clicked |
| OnSearchTextChanged | Triggered when the search text changes |

## Usage Examples

### Example 1: Basic Select

```razor
<Select @bind-Value="@selectedValue" Placeholder="Select an option">
    <Options>
        <SelectOption Text="Option 1" Value="1" />
        <SelectOption Text="Option 2" Value="2" />
        <SelectOption Text="Option 3" Value="3" />
    </Options>
</Select>

<div class="mt-3">
    Selected value: @selectedValue
</div>

@code {
    private int selectedValue;
}
```

### Example 2: Select with Data Binding

```razor
<Select @bind-Value="@selectedCountry" 
        TValue="string" 
        Items="@countries" 
        ShowLabel="true" 
        DisplayText="Country" 
        Placeholder="Select a country">
    <DisplayTemplate Context="country">
        @country
    </DisplayTemplate>
</Select>

<div class="mt-3">
    Selected country: @selectedCountry
</div>

@code {
    private string selectedCountry;
    private List<string> countries = new List<string>
    {
        "United States",
        "Canada",
        "United Kingdom",
        "Australia",
        "Germany",
        "France",
        "Japan",
        "China",
        "Brazil",
        "India"
    };
}
```

### Example 3: Multiple Select

```razor
<Select @bind-Value="@selectedFruits" 
        TValue="List<string>" 
        Items="@fruits" 
        IsMultiple="true" 
        ShowSearch="true" 
        ShowClear="true" 
        ShowLabel="true" 
        DisplayText="Fruits" 
        Placeholder="Select fruits">
    <DisplayTemplate Context="fruit">
        @fruit
    </DisplayTemplate>
</Select>

<div class="mt-3">
    <h5>Selected fruits:</h5>
    @if (selectedFruits != null && selectedFruits.Any())
    {
        <ul>
            @foreach (var fruit in selectedFruits)
            {
                <li>@fruit</li>
            }
        </ul>
    }
    else
    {
        <p>No fruits selected</p>
    }
</div>

@code {
    private List<string> selectedFruits = new List<string>();
    private List<string> fruits = new List<string>
    {
        "Apple",
        "Banana",
        "Cherry",
        "Durian",
        "Elderberry",
        "Fig",
        "Grape",
        "Honeydew",
        "Kiwi",
        "Lemon",
        "Mango",
        "Orange",
        "Papaya",
        "Raspberry",
        "Strawberry",
        "Watermelon"
    };
}
```

### Example 4: Select with Option Groups

```razor
<Select @bind-Value="@selectedAnimal" 
        TValue="string" 
        ShowLabel="true" 
        DisplayText="Animal" 
        Placeholder="Select an animal">
    <Options>
        <SelectOptionGroup Text="Mammals">
            <SelectOption Text="Lion" Value="lion" />
            <SelectOption Text="Elephant" Value="elephant" />
            <SelectOption Text="Monkey" Value="monkey" />
            <SelectOption Text="Dolphin" Value="dolphin" />
        </SelectOptionGroup>
        <SelectOptionGroup Text="Birds">
            <SelectOption Text="Eagle" Value="eagle" />
            <SelectOption Text="Penguin" Value="penguin" />
            <SelectOption Text="Parrot" Value="parrot" />
            <SelectOption Text="Owl" Value="owl" />
        </SelectOptionGroup>
        <SelectOptionGroup Text="Reptiles">
            <SelectOption Text="Crocodile" Value="crocodile" />
            <SelectOption Text="Snake" Value="snake" />
            <SelectOption Text="Turtle" Value="turtle" />
            <SelectOption Text="Lizard" Value="lizard" />
        </SelectOptionGroup>
    </Options>
</Select>

<div class="mt-3">
    Selected animal: @selectedAnimal
</div>

@code {
    private string selectedAnimal;
}
```

### Example 5: Select with Form Validation

```razor
<ValidateForm Model="@model" OnValidSubmit="@HandleValidSubmit">
    <div class="mb-3">
        <Select @bind-Value="@model.Country" 
                TValue="string" 
                Items="@countries" 
                ShowLabel="true" 
                DisplayText="Country" 
                Placeholder="Select your country">
            <DisplayTemplate Context="country">
                @country
            </DisplayTemplate>
        </Select>
        <ValidationMessage For="@(() => model.Country)" />
    </div>
    
    <div class="mb-3">
        <Select @bind-Value="@model.Languages" 
                TValue="List<string>" 
                Items="@languages" 
                IsMultiple="true" 
                ShowSearch="true" 
                ShowLabel="true" 
                DisplayText="Languages" 
                Placeholder="Select languages you speak">
            <DisplayTemplate Context="language">
                @language
            </DisplayTemplate>
        </Select>
        <ValidationMessage For="@(() => model.Languages)" />
    </div>
    
    <Button Type="ButtonType.Submit">Submit</Button>
</ValidateForm>

@code {
    private ProfileModel model = new ProfileModel();
    
    private List<string> countries = new List<string>
    {
        "United States",
        "Canada",
        "United Kingdom",
        "Australia",
        "Germany",
        "France",
        "Japan",
        "China",
        "Brazil",
        "India"
    };
    
    private List<string> languages = new List<string>
    {
        "English",
        "Spanish",
        "French",
        "German",
        "Chinese",
        "Japanese",
        "Russian",
        "Arabic",
        "Portuguese",
        "Hindi"
    };
    
    private void HandleValidSubmit()
    {
        // Process the form data
        Console.WriteLine($"Country: {model.Country}");
        Console.WriteLine("Languages:");
        if (model.Languages != null)
        {
            foreach (var language in model.Languages)
            {
                Console.WriteLine($"- {language}");
            }
        }
    }
    
    public class ProfileModel
    {
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
        
        [Required(ErrorMessage = "At least one language is required")]
        [MinLength(1, ErrorMessage = "Please select at least one language")]
        public List<string> Languages { get; set; } = new List<string>();
    }
}
```

### Example 6: Select with Custom Option Templates

```razor
<Select @bind-Value="@selectedUser" 
        TValue="string" 
        TItem="UserInfo" 
        Items="@users" 
        ShowSearch="true" 
        ShowLabel="true" 
        DisplayText="User" 
        Placeholder="Select a user">
    <DisplayTemplate Context="user">
        <div class="d-flex align-items-center">
            <div class="avatar mr-2" style="background-color: @user.AvatarColor; width: 24px; height: 24px; border-radius: 50%; display: flex; align-items: center; justify-content: center; color: white; font-weight: bold;">
                @user.Name[0]
            </div>
            <div>
                <div>@user.Name</div>
                <small class="text-muted">@user.Email</small>
            </div>
        </div>
    </DisplayTemplate>
    <ItemTemplate Context="user">
        <div class="d-flex align-items-center p-2">
            <div class="avatar mr-3" style="background-color: @user.AvatarColor; width: 32px; height: 32px; border-radius: 50%; display: flex; align-items: center; justify-content: center; color: white; font-weight: bold;">
                @user.Name[0]
            </div>
            <div>
                <div><strong>@user.Name</strong></div>
                <div class="text-muted">@user.Email</div>
                <div class="small">@user.Role</div>
            </div>
        </div>
    </ItemTemplate>
</Select>

<div class="mt-3">
    @if (!string.IsNullOrEmpty(selectedUser))
    {
        var user = users.FirstOrDefault(u => u.Id == selectedUser);
        if (user != null)
        {
            <div class="card">
                <div class="card-header">Selected User</div>
                <div class="card-body">
                    <h5>@user.Name</h5>
                    <p>Email: @user.Email</p>
                    <p>Role: @user.Role</p>
                </div>
            </div>
        }
    }
</div>

@code {
    private string selectedUser;
    private List<UserInfo> users = new List<UserInfo>
    {
        new UserInfo { Id = "1", Name = "John Doe", Email = "john.doe@example.com", Role = "Administrator", AvatarColor = "#4A6BF5" },
        new UserInfo { Id = "2", Name = "Jane Smith", Email = "jane.smith@example.com", Role = "Manager", AvatarColor = "#F54A6B" },
        new UserInfo { Id = "3", Name = "Bob Johnson", Email = "bob.johnson@example.com", Role = "Developer", AvatarColor = "#6BF54A" },
        new UserInfo { Id = "4", Name = "Alice Williams", Email = "alice.williams@example.com", Role = "Designer", AvatarColor = "#F5A64A" },
        new UserInfo { Id = "5", Name = "Charlie Brown", Email = "charlie.brown@example.com", Role = "Support", AvatarColor = "#4AF5A6" }
    };
    
    public class UserInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string AvatarColor { get; set; }
    }
}
```

### Example 7: Select with Remote Data Loading

```razor
@inject HttpClient Http

<div class="mb-3">
    <Select @bind-Value="@selectedCountry" 
            TValue="string" 
            TItem="Country" 
            OnLoadItems="@LoadCountries" 
            ShowSearch="true" 
            OnSearchTextChanged="@OnCountrySearch" 
            ShowLabel="true" 
            DisplayText="Country" 
            Placeholder="Select a country">
        <DisplayTemplate Context="country">
            @if (country != null)
            {
                <span>@country.Name</span>
            }
        </DisplayTemplate>
        <ItemTemplate Context="country">
            <div class="d-flex align-items-center">
                <img src="@country.FlagUrl" alt="@country.Name flag" style="width: 24px; margin-right: 8px;" />
                <div>
                    <div><strong>@country.Name</strong></div>
                    <small class="text-muted">@country.Capital</small>
                </div>
            </div>
        </ItemTemplate>
    </Select>
</div>

<div class="mb-3">
    <Select @bind-Value="@selectedCity" 
            TValue="string" 
            TItem="City" 
            OnLoadItems="@LoadCities" 
            IsDisabled="@string.IsNullOrEmpty(selectedCountry)" 
            ShowSearch="true" 
            ShowLabel="true" 
            DisplayText="City" 
            Placeholder="@(string.IsNullOrEmpty(selectedCountry) ? "Select a country first" : "Select a city")">
        <DisplayTemplate Context="city">
            @if (city != null)
            {
                <span>@city.Name</span>
            }
        </DisplayTemplate>
    </Select>
</div>

@code {
    private string selectedCountry;
    private string selectedCity;
    private string searchText;
    
    private async Task<IEnumerable<Country>> LoadCountries()
    {
        // In a real application, this would be an API call
        await Task.Delay(500); // Simulate network delay
        
        var countries = new List<Country>
        {
            new Country { Code = "US", Name = "United States", Capital = "Washington D.C.", FlagUrl = "/images/flags/us.png" },
            new Country { Code = "CA", Name = "Canada", Capital = "Ottawa", FlagUrl = "/images/flags/ca.png" },
            new Country { Code = "UK", Name = "United Kingdom", Capital = "London", FlagUrl = "/images/flags/uk.png" },
            new Country { Code = "AU", Name = "Australia", Capital = "Canberra", FlagUrl = "/images/flags/au.png" },
            new Country { Code = "DE", Name = "Germany", Capital = "Berlin", FlagUrl = "/images/flags/de.png" },
            new Country { Code = "FR", Name = "France", Capital = "Paris", FlagUrl = "/images/flags/fr.png" },
            new Country { Code = "JP", Name = "Japan", Capital = "Tokyo", FlagUrl = "/images/flags/jp.png" },
            new Country { Code = "CN", Name = "China", Capital = "Beijing", FlagUrl = "/images/flags/cn.png" },
            new Country { Code = "BR", Name = "Brazil", Capital = "Brasília", FlagUrl = "/images/flags/br.png" },
            new Country { Code = "IN", Name = "India", Capital = "New Delhi", FlagUrl = "/images/flags/in.png" }
        };
        
        if (!string.IsNullOrEmpty(searchText))
        {
            countries = countries
                .Where(c => c.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) || 
                       c.Capital.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        
        return countries;
    }
    
    private async Task OnCountrySearch(string text)
    {
        searchText = text;
    }
    
    private async Task<IEnumerable<City>> LoadCities()
    {
        if (string.IsNullOrEmpty(selectedCountry))
        {
            return new List<City>();
        }
        
        // In a real application, this would be an API call with the selected country as a parameter
        await Task.Delay(500); // Simulate network delay
        
        var cities = new Dictionary<string, List<City>>
        {
            ["US"] = new List<City>
            {
                new City { Id = "NY", Name = "New York" },
                new City { Id = "LA", Name = "Los Angeles" },
                new City { Id = "CH", Name = "Chicago" },
                new City { Id = "HO", Name = "Houston" },
                new City { Id = "PH", Name = "Philadelphia" }
            },
            ["CA"] = new List<City>
            {
                new City { Id = "TO", Name = "Toronto" },
                new City { Id = "MO", Name = "Montreal" },
                new City { Id = "VA", Name = "Vancouver" },
                new City { Id = "CA", Name = "Calgary" },
                new City { Id = "OT", Name = "Ottawa" }
            },
            ["UK"] = new List<City>
            {
                new City { Id = "LO", Name = "London" },
                new City { Id = "MA", Name = "Manchester" },
                new City { Id = "BI", Name = "Birmingham" },
                new City { Id = "GL", Name = "Glasgow" },
                new City { Id = "LI", Name = "Liverpool" }
            }
            // Add more countries and cities as needed
        };
        
        return cities.ContainsKey(selectedCountry) ? cities[selectedCountry] : new List<City>();
    }
    
    public class Country
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }
        public string FlagUrl { get; set; }
    }
    
    public class City
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
```

## Customization Notes

The Select component can be customized using the following CSS variables:

```css
:root {
    --bb-select-height: calc(1.5em + 0.75rem + 2px);
    --bb-select-padding-y: 0.375rem;
    --bb-select-padding-x: 0.75rem;
    --bb-select-font-size: 1rem;
    --bb-select-line-height: 1.5;
    --bb-select-color: #212529;
    --bb-select-bg: #fff;
    --bb-select-border-color: #ced4da;
    --bb-select-border-width: 1px;
    --bb-select-border-radius: 0.25rem;
    --bb-select-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
    --bb-select-focus-bg: #fff;
    --bb-select-focus-border-color: #86b7fe;
    --bb-select-focus-color: #212529;
    --bb-select-focus-box-shadow: 0 0 0 0.25rem rgba(13, 110, 253, 0.25);
    --bb-select-placeholder-color: #6c757d;
    --bb-select-disabled-bg: #e9ecef;
    --bb-select-disabled-color: #6c757d;
    --bb-select-readonly-bg: #e9ecef;
    --bb-select-dropdown-bg: #fff;
    --bb-select-dropdown-border-color: rgba(0, 0, 0, 0.15);
    --bb-select-dropdown-border-radius: 0.25rem;
    --bb-select-dropdown-box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.175);
    --bb-select-dropdown-item-padding-y: 0.25rem;
    --bb-select-dropdown-item-padding-x: 1rem;
    --bb-select-dropdown-item-hover-bg: #f8f9fa;
    --bb-select-dropdown-item-active-bg: #0d6efd;
    --bb-select-dropdown-item-active-color: #fff;
    --bb-select-dropdown-item-disabled-color: #6c757d;
    --bb-select-dropdown-header-color: #6c757d;
    --bb-select-dropdown-header-font-size: 0.875rem;
    --bb-select-dropdown-max-height: 300px;
    --bb-select-search-height: 2rem;
    --bb-select-search-padding-y: 0.25rem;
    --bb-select-search-padding-x: 0.5rem;
    --bb-select-search-font-size: 0.875rem;
    --bb-select-search-border-color: #ced4da;
    --bb-select-tag-bg: #e9ecef;
    --bb-select-tag-color: #212529;
    --bb-select-tag-border-radius: 0.25rem;
    --bb-select-tag-margin: 0.125rem;
    --bb-select-tag-padding-y: 0.125rem;
    --bb-select-tag-padding-x: 0.5rem;
    --bb-select-tag-font-size: 0.75rem;
    --bb-select-tag-close-color: #6c757d;
    --bb-select-tag-close-hover-color: #212529;
}
```

Additionally, you can customize the appearance and behavior of the Select component by:

1. Using the `IsMultiple` property to enable multiple selection
2. Using the `ShowSearch` property to enable search functionality
3. Using the `Size` property to adjust the select size
4. Using the `ShowLabel`, `DisplayText`, and `ShowRequiredMark` properties to customize the label
5. Using the `DisplayTemplate` and `ItemTemplate` to customize the appearance of selected values and dropdown items
6. Using the `MaxTagCount` property to control the display of multiple selected values
7. Using the `OnLoadItems` and `OnSearchTextChanged` properties for dynamic data loading
8. Applying custom CSS classes to the component using the `ClassName` property