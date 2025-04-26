// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Collections;
using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Implementation class for required field validation
/// </summary>
public class RequiredValidator : ValidatorBase
{
    /// <summary>
    /// Gets/Sets the error description message, defaults to null and needs to be assigned
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Gets/Sets whether empty strings are allowed, defaults to false (not allowed)
    /// </summary>
    public bool AllowEmptyString { get; set; }

    /// <summary>
    /// Gets/Sets the IStringLocalizerFactory injection service instance, defaults to null
    /// </summary>
    public IStringLocalizerFactory? LocalizerFactory { get; set; }

    /// <summary>
    /// Gets/Sets the Json resource file configuration, defaults to null
    /// </summary>
    public JsonLocalizationOptions? Options { get; set; }

    /// <summary>
    /// Validation method
    /// </summary>
    /// <param name="propertyValue">Value to validate</param>
    /// <param name="context">ValidateContext instance</param>
    /// <param name="results">ValidateResult collection instance</param>
    public override void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
    {
        if (string.IsNullOrEmpty(ErrorMessage))
        {
            var localizer = context.GetRequiredService<IStringLocalizer<ValidateBase<string>>>();
            var l = localizer["DefaultRequiredErrorMessage"];
            if (!l.ResourceNotFound)
            {
                ErrorMessage = l.Value;
            }
        }
        if (propertyValue == null)
        {
            results.Add(GetValidationResult(context));
        }
        else if (propertyValue is string val)
        {
            if (!AllowEmptyString && val == string.Empty)
            {
                results.Add(GetValidationResult(context));
            }
        }
        else if (propertyValue is IEnumerable v)
        {
            var enumerator = v.GetEnumerator();
            var valid = enumerator.MoveNext();
            if (!valid)
            {
                results.Add(GetValidationResult(context));
            }
        }
        else if (propertyValue is DateTimeRangeValue dv && dv is { NullStart: null, NullEnd: null })
        {
            results.Add(GetValidationResult(context));
        }
    }

    private ValidationResult GetValidationResult(ValidationContext context)
    {
        var errorMessage = GetLocalizerErrorMessage(context, LocalizerFactory, Options);
        return context.GetValidationResult(errorMessage);
    }

    /// <summary>
    /// Gets the Key format in the current validation rule resource file
    /// </summary>
    /// <returns></returns>
    protected virtual string GetRuleKey() => GetType().Name.Split(".").Last().Replace("Validator", "");

    /// <summary>
    /// Method to get ErrorMessage through resource files
    /// </summary>
    /// <param name="context"></param>
    /// <param name="localizerFactory"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    protected virtual string? GetLocalizerErrorMessage(ValidationContext context, IStringLocalizerFactory? localizerFactory = null, JsonLocalizationOptions? options = null)
    {
        var errorMessage = ErrorMessage;
        if (!string.IsNullOrEmpty(context.MemberName) && !string.IsNullOrEmpty(errorMessage))
        {
            // Find ErrorMessage in resx resource files
            var memberName = context.MemberName;

            if (localizerFactory != null)
            {
                // Find Microsoft format resx resource files
                var isResx = false;
                if (options is { ResourceManagerStringLocalizerType: not null })
                {
                    var localizer = localizerFactory.Create(options.ResourceManagerStringLocalizerType);
                    if (localizer.TryGetLocalizerString(errorMessage, out var resx))
                    {
                        errorMessage = resx;
                        isResx = true;
                    }
                }

                // Find json format resource files
                if (!isResx && localizerFactory.Create(context.ObjectType).TryGetLocalizerString($"{memberName}.{GetRuleKey()}", out var msg))
                {
                    errorMessage = msg;
                }
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                var displayName = new FieldIdentifier(context.ObjectInstance, context.MemberName).GetDisplayName();
                errorMessage = string.Format(CultureInfo.CurrentCulture, errorMessage, displayName);
            }
        }
        return errorMessage;
    }
}
