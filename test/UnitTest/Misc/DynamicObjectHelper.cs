// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest;

internal static class DynamicObjectHelper
{
    public static Type CreateDynamicType(string typeName = "Test")
    {
        var cols = new InternalTableColumn[]
        {
            new("Id", typeof(int)),
            new("Name", typeof(string))
        };

        // Create dynamic type with base class DynamicObject
        var instanceType = EmitHelper.CreateTypeByName(typeName, cols, typeof(DynamicObject));
        return instanceType!;
    }

    public static object CreateDynamicObject(string typeName = "Test")
    {
        var instanceType = CreateDynamicType(typeName);

        // Create dynamic object instance
        return Activator.CreateInstance(instanceType)!;
    }
}
