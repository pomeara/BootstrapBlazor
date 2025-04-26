// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Base class for asynchronous IValidator implementation
/// </summary>
public abstract class ValidatorAsyncBase : IValidatorAsync
{
    [ExcludeFromCodeCoverage]
    void IValidator.Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
    {

    }

    /// <summary>
    /// Data validation method
    /// </summary>
    /// <param name="propertyValue"></param>
    /// <param name="context"></param>
    /// <param name="results"></param>
    public abstract Task ValidateAsync(object? propertyValue, ValidationContext context, List<ValidationResult> results);
}
