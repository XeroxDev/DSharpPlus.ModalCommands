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

namespace DSharpPlus.ModalCommands;

public static class ModalBuilder
{
    public static DiscordInteractionResponseBuilder Create(string id, string[]? args = null)
    {
        var joinedArgs = args is null ? string.Empty : string.Join(ModalCommandsExtension.Config.Seperator, args);
        if (!string.IsNullOrWhiteSpace(joinedArgs)) joinedArgs = $"{ModalCommandsExtension.Config.Seperator}{joinedArgs}";
        return new DiscordInteractionResponseBuilder().WithCustomId($"{ModalCommandsExtension.Config.Prefix}{id}{joinedArgs}");
        // Much lazy, much wow! And yes. ModalCommandsExtension.Config was not static before. I just made it static for this!
    }
}