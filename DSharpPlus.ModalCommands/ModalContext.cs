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

public class ModalContext
{
    public ModalContext(DiscordInteraction interaction, DiscordClient client, DiscordGuild guild, DiscordChannel channel, DiscordUser user, ModalCommandsExtension modalCommandsExtension, string[] values)
    {
        Interaction = interaction;
        Client = client;
        Guild = guild;
        Channel = channel;
        User = user;
        ModalCommandsExtension = modalCommandsExtension;
        Values = values;
    }

    public DiscordInteraction Interaction { get; }
    public DiscordClient Client { get; }
    public DiscordGuild Guild { get; }
    public DiscordChannel Channel { get; }
    public DiscordUser User { get; }
    public DiscordMember? Member => User is DiscordMember member ? member : null;
    public ModalCommandsExtension ModalCommandsExtension { get; }
    public string[] Values { get; }

    /// <summary>
    /// Creates a response to this interaction.
    /// <para>You must create a response within 3 seconds of this interaction being executed; if the command has the potential to take more than 3 seconds, use <see cref="M:DSharpPlus.ModalCommands.ModalContext.DeferAsync(System.Boolean)" /> at the start, and edit the response later.</para>
    /// </summary>
    /// <param name="type">The type of the response.</param>
    /// <param name="builder">The data to be sent, if any.</param>
    public Task CreateResponseAsync(
        InteractionResponseType type,
        DiscordInteractionResponseBuilder? builder = null)
    {
        return Interaction.CreateResponseAsync(type, builder);
    }

    /// <inheritdoc cref="M:DSharpPlus.ModalCommands.ModalContext.CreateResponseAsync(DSharpPlus.InteractionResponseType,DSharpPlus.Entities.DiscordInteractionResponseBuilder)" />
    public Task CreateResponseAsync(DiscordInteractionResponseBuilder? builder) => CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, builder);

    /// <summary>
    /// Creates a response to this interaction.
    /// <para>You must create a response within 3 seconds of this interaction being executed; if the command has the potential to take more than 3 seconds, use <see cref="M:DSharpPlus.ModalCommands.ModalContext.DeferAsync(System.Boolean)" /> at the start, and edit the response later.</para>
    /// </summary>
    /// <param name="content">Content to send in the response.</param>
    /// <param name="embed">Embed to send in the response.</param>
    /// <param name="ephemeral">Whether the response should be ephemeral.</param>
    public Task CreateResponseAsync(string content, DiscordEmbed embed, bool ephemeral = false) =>
        CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent(content).AddEmbed(embed).AsEphemeral(ephemeral));

    /// <inheritdoc cref="M:DSharpPlus.ModalCommands.ModalContext.CreateResponseAsync(System.String,DSharpPlus.Entities.DiscordEmbed,System.Boolean)" />
    public Task CreateResponseAsync(string content, bool ephemeral = false) => CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent(content).AsEphemeral(ephemeral));

    /// <inheritdoc cref="M:DSharpPlus.ModalCommands.ModalContext.CreateResponseAsync(System.String,DSharpPlus.Entities.DiscordEmbed,System.Boolean)" />
    public Task CreateResponseAsync(DiscordEmbed embed, bool ephemeral = false) => CreateResponseAsync(new DiscordInteractionResponseBuilder().AddEmbed(embed).AsEphemeral(ephemeral));

    /// <summary>Creates a deferred response to this interaction.</summary>
    /// <param name="ephemeral">Whether the response should be ephemeral.</param>
    public Task DeferAsync(bool ephemeral = false) =>
        CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource, new DiscordInteractionResponseBuilder().AsEphemeral(ephemeral));

    /// <summary>Edits the interaction response.</summary>
    /// <param name="builder">The data to edit the response with.</param>
    /// <param name="attachments">Attached files to keep.</param>
    /// <returns></returns>
    public Task<DiscordMessage> EditResponseAsync(
        DiscordWebhookBuilder builder,
        IEnumerable<DiscordAttachment>? attachments = null)
    {
        return Interaction.EditOriginalResponseAsync(builder, attachments);
    }

    /// <summary>Deletes the interaction response.</summary>
    /// <returns></returns>
    public Task DeleteResponseAsync() => Interaction.DeleteOriginalResponseAsync();

    /// <summary>Creates a follow up message to the interaction.</summary>
    /// <param name="builder">The message to be sent, in the form of a webhook.</param>
    /// <returns>The created message.</returns>
    public Task<DiscordMessage> FollowUpAsync(
        DiscordFollowupMessageBuilder builder)
    {
        return Interaction.CreateFollowupMessageAsync(builder);
    }

    /// <summary>Edits a followup message.</summary>
    /// <param name="followupMessageId">The id of the followup message to edit.</param>
    /// <param name="builder">The webhook builder.</param>
    /// <param name="attachments">Attached files to keep.</param>
    /// <returns></returns>
    public Task<DiscordMessage> EditFollowupAsync(
        ulong followupMessageId,
        DiscordWebhookBuilder builder,
        IEnumerable<DiscordAttachment>? attachments = null)
    {
        return Interaction.EditFollowupMessageAsync(followupMessageId, builder, attachments);
    }

    /// <summary>Deletes a followup message.</summary>
    /// <param name="followupMessageId">The id of the followup message to delete.</param>
    /// <returns></returns>
    public Task DeleteFollowupAsync(ulong followupMessageId) => Interaction.DeleteFollowupMessageAsync(followupMessageId);

    /// <summary>Gets the original interaction response.</summary>
    /// <returns>The original interaction response.</returns>
    public Task<DiscordMessage> GetOriginalResponseAsync() => Interaction.GetOriginalResponseAsync();
}