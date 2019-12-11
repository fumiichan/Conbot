using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.WebSocket;
using System.Threading;
using Discord;
using Conbot.Extensions;
using System.Linq;

namespace Conbot.InteractiveMessages
{
    public class InteractiveMessageBuilder
    {
        public Func<IUser, Task<bool>> Precondition { get; set; }
        public int Timeout { get; set; } = 60000;
        public Dictionary<string, ReactionCallbackBuilder> ReactionCallbacks { get; set; }
            = new Dictionary<string, ReactionCallbackBuilder>();

        public List<MessageCallbackBuilder> MessageCallbacks { get; set; }
            = new List<MessageCallbackBuilder>();

        public bool AutoReactEmotes { get; set; } = true;

        public InteractiveMessageBuilder WithAutoReactEmotes(bool @value = true)
        {
            AutoReactEmotes = @value;
            return this;
        }

        public InteractiveMessageBuilder WithPrecondition(Func<IUser, Task<bool>> precondition)
        {
            Precondition = precondition;
            return this;
        }

        public InteractiveMessageBuilder WithPrecondition(Func<IUser, bool> precondition)
        {
            Precondition = x => Task.FromResult(precondition(x));
            return this;
        }

        public InteractiveMessageBuilder WithTimeout(int ms)
        {
            Timeout = ms;
            return this;
        }

        public InteractiveMessageBuilder AddReactionCallback(ReactionCallbackBuilder reactionCallback)
        {
            ReactionCallbacks.Add(reactionCallback.Emote.ToString(), reactionCallback);
            return this;
        }

        public InteractiveMessageBuilder AddReactionCallback(
            Func<ReactionCallbackBuilder, ReactionCallbackBuilder> reactionCallbackFunc)
        {
            var reactionCallback = reactionCallbackFunc(new ReactionCallbackBuilder());
            ReactionCallbacks.Add(reactionCallback.Emote.ToString(), reactionCallback);
            return this;
        }

        public InteractiveMessageBuilder AddMessageCallback(MessageCallbackBuilder messageCallback)
        {
            MessageCallbacks.Add(messageCallback);
            return this;
        }

        public InteractiveMessageBuilder AddMessageCallback(
            Func<MessageCallbackBuilder, MessageCallbackBuilder> messageCallbackFunc)
        {
            var messageCallback = messageCallbackFunc(new MessageCallbackBuilder());
            MessageCallbacks.Add(messageCallback);
            return this;
        }

        public InteractiveMessage Build()
            => new InteractiveMessage(Precondition, Timeout,
                ReactionCallbacks.ToDictionary(k => k.Key, v => v.Value.Build()),
                MessageCallbacks.Select(x => x.Build()).ToList(), AutoReactEmotes);
    }
}
