# Table of content

<!-- toc -->

- [1. Badges](#1-badges)
- [2. What is this package](#2-what-is-this-package)
- [3. Example](#3-example)
  * [3.1 Registration](#31-registration)
  * [3.2 Creating a modal](#32-creating-a-modal)
  * [3.3 Listen for modal submit](#33-listen-for-modal-submit)
- [4. Custom arguments converter](#4-custom-arguments-converter)
- [5. Credits](#5-credits)

<!-- tocstop -->

# 1. Badges

[![Forks](https://img.shields.io/github/forks/XeroxDev/DSharpPlus.ModuleCommands?color=blue&style=for-the-badge)](https://github.com/XeroxDev/DSharpPlus.ModuleCommands/network/members)  [![Stars](https://img.shields.io/github/stars/XeroxDev/DSharpPlus.ModuleCommands?color=yellow&style=for-the-badge)](https://github.com/XeroxDev/DSharpPlus.ModuleCommands/stargazers) [![Watchers](https://img.shields.io/github/watchers/XeroxDev/DSharpPlus.ModuleCommands?color=lightgray&style=for-the-badge)](https://github.com/XeroxDev/DSharpPlus.ModuleCommands/watchers) [![Contributors](https://img.shields.io/github/contributors/XeroxDev/DSharpPlus.ModuleCommands?color=green&style=for-the-badge)](https://github.com/XeroxDev/DSharpPlus.ModuleCommands/graphs/contributors)

[![Issues](https://img.shields.io/github/issues/XeroxDev/DSharpPlus.ModuleCommands?color=yellow&style=for-the-badge)](https://github.com/XeroxDev/DSharpPlus.ModuleCommands/issues) [![Issues closed](https://img.shields.io/github/issues-closed/XeroxDev/DSharpPlus.ModuleCommands?color=yellow&style=for-the-badge)](https://github.com/XeroxDev/DSharpPlus.ModuleCommands/issues?q=is%3Aissue+is%3Aclosed)

[![Issues-pr](https://img.shields.io/github/issues-pr/XeroxDev/DSharpPlus.ModuleCommands?color=yellow&style=for-the-badge)](https://github.com/XeroxDev/DSharpPlus.ModuleCommands/pulls) [![Issues-pr closed](https://img.shields.io/github/issues-pr-closed/XeroxDev/DSharpPlus.ModuleCommands?color=yellow&style=for-the-badge)](https://github.com/XeroxDev/DSharpPlus.ModuleCommands/pulls?q=is%3Apr+is%3Aclosed) [![PRs welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=for-the-badge)](https://github.com/XeroxDev/DSharpPlus.ModuleCommands/compare)

![Version](https://img.shields.io/nuget/v/XeroxDev.DSharpPlus.ModalCommands?style=for-the-badge) ![Downloads](https://img.shields.io/nuget/dt/XeroxDev.DSharpPlus.ModalCommands?style=for-the-badge)

[![Awesome Badges](https://img.shields.io/badge/badges-awesome-green?style=for-the-badge)](https://shields.io)

# 2. What is this package

This is an extension for DSharpPlus to use modals like commands from CommandsNext.

# 3. Example

## 3.1 Registration

```csharp
var modalCommands = _client.UseModalCommands(new ModalCommandsConfiguration()
{
    Services = _sp
});
modalCommands.RegisterModals(Assembly.GetExecutingAssembly());
```

## 3.2 Creating a modal

```csharp
var modal = ModalBuilder.Create("food")
    .WithTitle("Super cool modal!")
    .AddComponents(new TextInputComponent("Favorite food", "fav-food", "Pizza, Icecream, etc", max_length: 30))
    .AddComponents(new TextInputComponent("Why?", "why-fav", "Because it tastes good", required: false, style: TextInputStyle.Paragraph));
```

You can also create a modal in the normal way, but remember to add the `Prefix` set in `ModalCommandsConfiguration` (
Default `>`) with `WithCustomId`.

## 3.3 Listen for modal submit

```csharp
public class FoodModal : ModalCommandModule
{
    [ModalCommand("food")]
    public async Task GetFavFoodAsync(ModalContext ctx, string food, string reason)
    {
        await ctx.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
            new DiscordInteractionResponseBuilder().WithContent($"Your favorite food is: {food}. Reason: {reason}").AsEphemeral());
    }
}
```

# 4. Custom arguments converter

By default, only the following arguments are supported:

- string
- bool
- int
- uint
- long
- ulong
- float
- double
- DiscordUser
- DiscordMember
- DiscordRole
- DiscordChannel

If you want to add more arguments, you can create a new argument converter like so:

```csharp
    public class IntConverter : IModalArgumentConverter<int>
    {
        // This method will convert the string to an int.
        public Task<Optional<int>> ConvertAsync(string value, ModalContext ctx)
        {
            return Task.FromResult(int.TryParse(value, out var res) ? Optional.FromValue(res) : Optional.FromNoValue<int>());
        }

        // This method will convert the int to a string.
        public string ConvertToString(int value) => value.ToString();
    }
```

After writing your argument converter, you can register it in your ModalCommandsExtension like so:
```csharp
buttonCommands.RegisterConverter(new ShortConverter());
```

# 5. Credits
I stole nearly all of this from Kuylars [DSharpPlus.ButtonCommands extension](https://gitlab.com/kuylar/DSharpPlus.ButtonCommands)

And because of this: If something doesn't work, blame Kuylar. I'm a lazy person :P