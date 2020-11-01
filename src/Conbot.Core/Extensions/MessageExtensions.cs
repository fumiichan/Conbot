using System.Threading.Tasks;
using Discord;
using Discord.Net;

namespace Conbot.Extensions
{
    public static class MessageExtensions
    {
        public static async Task<bool> TryAddReactionAsync(this IUserMessage message, IEmote emote,
            RequestOptions options = null)
        {
            try
            {
                await message.AddReactionAsync(emote, options).ConfigureAwait(false);
            }
            catch (HttpException)
            {
                return false;
            }
            return true;
        }


        public static async Task<bool> TryRemoveReactionAsync(this IUserMessage message, IEmote emote, IUser user,
            RequestOptions options = null)
        {
            try
            {
                await message.RemoveReactionAsync(emote, user, options).ConfigureAwait(false);
            }
            catch (HttpException)
            {
                return false;
            }
            return true;
        }

        public static async Task<bool> TryRemoveAllReactionsAsync(this IUserMessage message, RequestOptions options = null)
        {
            try
            {
                await message.RemoveAllReactionsAsync(options).ConfigureAwait(false);
            }
            catch (HttpException)
            {
                return false;
            }
            return true;
        }

        public static bool HasMentionPrefix(this IUserMessage message, IUser user, out string output)
        {
            string content = message.Content;
            output = null;

            int endPos = content.IndexOf(' ');
            if (endPos == -1)
                return false;

            string mention = content.Substring(0, endPos);

            if (!MentionUtils.TryParseUser(mention, out ulong userId))
                return false;

            if (userId != user.Id)
                return false;

            output = content.Substring(mention.Length + 1);
            return true;
        }
    }
}
