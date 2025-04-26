// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Implementation class for minimum number of options validation
/// </summary>
public class MinValidator : MaxValidator
{
    /// <summary>
    /// Validation method returns true when count is greater than or equal to Value
    /// </summary>
    protected override bool Validate(int count) => count >= Value;

    /// <summary>
    /// Gets the ErrorMessage method
    /// </summary>
    /// <returns></returns>
    protected override string GetErrorMessage() => ErrorMessage ?? "Select at least {0} items";
}
