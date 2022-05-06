// This file is part of the DSharpPlus.ModalCommands project.
// 
// Copyright (c) 2022 Dominic Ris
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Reflection;
using DSharpPlus.ModalCommands.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace DSharpPlus.ModalCommands;

internal static class InstanceCreator
{
    internal static object CreateInstance(Type t, IServiceProvider services)
    {
        var ti = t.GetTypeInfo();

        var constructors = ti.DeclaredConstructors
            .Where(xm => xm.IsPublic)
            .ToArray();

        if (constructors.Length != 1)
        {
            constructors = constructors
                .Where(xm => xm.GetCustomAttribute<ModuleConstructorAttribute>() != null)
                .ToArray();

            if (constructors.Length != 1)
            {
                throw new ArgumentException("Unable to select a constructor for the specific constructor.");
            }
        }

        var constructorArguments = constructors[0].GetParameters();

        if (constructorArguments.Length != 0 && services == null)
        {
            throw new InvalidOperationException("Service collection needs to contain all necessary values for constructor injection.");
        }

        var arguments = new object[constructorArguments.Length];

        for (var i = 0; i < arguments.Length; i++)
        {
            arguments[i] = services.GetRequiredService(constructorArguments[i].ParameterType);
        }

        var moduleInstance = Activator.CreateInstance(t, arguments);

        var properties = t.GetRuntimeProperties().Where(xm => xm.CanWrite && xm.SetMethod != null
                                                                          && !xm.SetMethod.IsStatic && xm.SetMethod.IsPublic).ToArray();

        foreach (var property in properties)
        {
            if (property.GetCustomAttribute<DontInjectAttribute>() != null)
            {
                continue;
            }

            var service = services.GetService(property.PropertyType);

            if (service == null)
            {
                continue;
            }

            property.SetValue(moduleInstance, service);
        }

        var fields = t.GetRuntimeFields().Where(xm => !xm.IsInitOnly && !xm.IsStatic && xm.IsPublic).ToArray();

        foreach (var field in fields)
        {
            if (field.GetCustomAttribute<DontInjectAttribute>() != null)
            {
                continue;
            }

            var service = services.GetService(field.FieldType);

            if (service == null)
            {
                continue;
            }

            field.SetValue(moduleInstance, service);
        }

        return moduleInstance;
    }
}