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

using DSharpPlus.Entities;

namespace DSharpPlus.ModalCommands.Converters;

public class GeneralConverters
{
    public class StringConverter : IModalArgumentConverter<string>
    {
        public Task<Optional<string>> ConvertAsync(string value, ModalContext ctx) => Task.FromResult<Optional<string>>(value);

        public string ConvertToString(string value) => value;
    }

    public class BoolConverter : IModalArgumentConverter<bool>
    {
        public Task<Optional<bool>> ConvertAsync(string value, ModalContext ctx)
        {
            switch (value)
            {
                case "true":
                case "t":
                case "yes":
                case "y":
                case "1":
                    return Task.FromResult(Optional.FromValue(true));
                case "false":
                case "f":
                case "no":
                case "n":
                case "0":
                    return Task.FromResult(Optional.FromValue(false));
                default:
                    return Task.FromResult(Optional.FromNoValue<bool>());
            }
        }

        public string ConvertToString(bool value) => value.ToString();
    }

    public class IntConverter : IModalArgumentConverter<int>
    {
        public Task<Optional<int>> ConvertAsync(string value, ModalContext ctx)
        {
            return Task.FromResult(int.TryParse(value, out var res) ? Optional.FromValue(res) : Optional.FromNoValue<int>());
        }

        public string ConvertToString(int value) => value.ToString();
    }

    public class UintConverter : IModalArgumentConverter<uint>
    {
        public Task<Optional<uint>> ConvertAsync(string value, ModalContext ctx)
        {
            return Task.FromResult(uint.TryParse(value, out var res) ? Optional.FromValue(res) : Optional.FromNoValue<uint>());
        }

        public string ConvertToString(uint value) => value.ToString();
    }

    public class LongConverter : IModalArgumentConverter<long>
    {
        public Task<Optional<long>> ConvertAsync(string value, ModalContext ctx)
        {
            return Task.FromResult(long.TryParse(value, out var res) ? Optional.FromValue(res) : Optional.FromNoValue<long>());
        }

        public string ConvertToString(long value) => value.ToString();
    }

    public class UlongConverter : IModalArgumentConverter<ulong>
    {
        public Task<Optional<ulong>> ConvertAsync(string value, ModalContext ctx)
        {
            return Task.FromResult(ulong.TryParse(value, out var res) ? Optional.FromValue(res) : Optional.FromNoValue<ulong>());
        }

        public string ConvertToString(ulong value) => value.ToString();
    }

    public class FloatConverter : IModalArgumentConverter<float>
    {
        public Task<Optional<float>> ConvertAsync(string value, ModalContext ctx)
        {
            return Task.FromResult(float.TryParse(value, out var res) ? Optional.FromValue(res) : Optional.FromNoValue<float>());
        }

        public string ConvertToString(float value) => value.ToString();
    }

    public class DoubleConverter : IModalArgumentConverter<double>
    {
        public Task<Optional<double>> ConvertAsync(string value, ModalContext ctx)
        {
            return Task.FromResult(double.TryParse(value, out var res) ? Optional.FromValue(res) : Optional.FromNoValue<double>());
        }

        public string ConvertToString(double value) => value.ToString();
    }
}