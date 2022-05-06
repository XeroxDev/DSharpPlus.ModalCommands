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

public class EntityConverters
{
    public class DiscordUserConverter : IModalArgumentConverter<DiscordUser>
    {
        public async Task<Optional<DiscordUser>> ConvertAsync(string value, ModalContext ctx)
        {
            if (!ulong.TryParse(value, out var id)) return Optional.FromNoValue<DiscordUser>();

            var result = await ctx.Client.GetUserAsync(id);
            return result != null ? Optional.FromValue(result) : Optional.FromNoValue<DiscordUser>();
        }

        public string ConvertToString(DiscordUser value) => value.Id.ToString();
    }

    public class DiscordMemberConverter : IModalArgumentConverter<DiscordMember>
    {
        public async Task<Optional<DiscordMember>> ConvertAsync(string value, ModalContext ctx)
        {
            if (!ulong.TryParse(value, out var id)) return Optional.FromNoValue<DiscordMember>();

            var result = await ctx.Guild.GetMemberAsync(id);
            return result != null ? Optional.FromValue(result) : Optional.FromNoValue<DiscordMember>();
        }

        public string ConvertToString(DiscordMember value) => value.Id.ToString();
    }

    public class DiscordRoleConverter : IModalArgumentConverter<DiscordRole>
    {
        public Task<Optional<DiscordRole>> ConvertAsync(string value, ModalContext ctx)
        {
            if (!ulong.TryParse(value, out var id)) return Task.FromResult(Optional.FromNoValue<DiscordRole>());

            var result = ctx.Guild.GetRole(id);
            return Task.FromResult(result != null ? Optional.FromValue(result) : Optional.FromNoValue<DiscordRole>());
        }

        public string ConvertToString(DiscordRole value) => value.Id.ToString();
    }

    public class DiscordChannelConverter : IModalArgumentConverter<DiscordChannel>
    {
        public async Task<Optional<DiscordChannel>> ConvertAsync(string value, ModalContext ctx)
        {
            if (!ulong.TryParse(value, out var id)) return Optional.FromNoValue<DiscordChannel>();

            var result = await ctx.Client.GetChannelAsync(id);
            return result != null ? Optional.FromValue(result) : Optional.FromNoValue<DiscordChannel>();
        }

        public string ConvertToString(DiscordChannel value) => value.Id.ToString();
    }
}