// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Custom validation class
/// </summary>
/// <param name="attribute"></param>
public class FormItemValidator(ValidationAttribute attribute) : ValidatorBase
{
    /// <summary>
    /// Validation method
    /// </summary>
    /// <param name="propertyValue">Value to validate</param>
    /// <param name="context">ValidateContext instance</param>
    /// <param name="results">ValidateResult collection instance</param>
    public override void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
    {
        var result = attribute.GetValidationResult(propertyValue, context);
        if (result != null)
        {
            results.Add(result);
        }
    }

    /// <summary>
    /// Whether it is a RequiredAttribute tag feature
    /// </summary>
    public bool IsRequired => attribute is RequiredAttribute;
}
