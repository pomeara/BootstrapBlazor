// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Implementation class for minimum number of options validation
/// </summary>
public class MaxValidator : ValidatorBase
{
    /// <summary>
    /// Gets/Sets the error description message
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Gets/Sets the value
    /// </summary>
    public int Value { get; set; }

    /// <summary>
    /// Gets/Sets the split callback method, defaults to using ',' as separator
    /// </summary>
    public Func<string, int> SplitCallback { get; set; } = value => value.Split(',', StringSplitOptions.RemoveEmptyEntries).Length;

    /// <summary>
    /// Gets the ErrorMessage method
    /// </summary>
    protected virtual string GetErrorMessage() => ErrorMessage ?? "At most {0} items can be selected";

    /// <summary>
    /// Validation method
    /// </summary>
    /// <param name="propertyValue">Value to validate</param>
    /// <param name="context">ValidateContext instance</param>
    /// <param name="results">ValidateResult collection instance</param>
    public override void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
    {
        if (!Validate(propertyValue))
        {
            var errorMessage = string.Format(CultureInfo.CurrentCulture, GetErrorMessage(), Value);
            results.Add(new ValidationResult(errorMessage, new string[] { context.MemberName ?? context.DisplayName }));
        }
    }

    /// <summary>
    /// Checks if Value is valid, returns true if valid
    /// </summary>
    /// <param name="propertyValue"></param>
    protected virtual bool Validate(object? propertyValue)
    {
        var ret = true;
        if (propertyValue != null)
        {
            var type = propertyValue.GetType();
            if (propertyValue is string value)
            {
                var count = SplitCallback(value);
                ret = Validate(count);
            }
            else if (type.IsGenericType || type.IsArray)
            {
                ret = Validate(LambdaExtensions.ElementCount(propertyValue));
            }
        }
        else
        {
            ret = false;
        }
        return ret;
    }

    /// <summary>
    /// Validation method returns true when count is greater than or equal to Value
    /// </summary>
    protected virtual bool Validate(int count) => count <= Value;
}
