using Kinmatch.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Kinmatch.Controllers
{
    internal static class MessageController
    {
        internal static async Task<bool> SendTextMessage(ITelegramBotClient? client, MessageData? messageData, string t)
        {
            if (string.IsNullOrEmpty(messageData?.text)) return false;
            if (client == null) return false;
            await client.SendTextMessageAsync(messageData.tg_id, messageData.text);
            return true;
        }
        internal static async Task<bool> SendTextMessage(ITelegramBotClient? client, MessageData messageData)
        {
            if (string.IsNullOrEmpty(messageData.text)) return false;
            if (client == null) return false;
            if (messageData.markup != null) await client.SendTextMessageAsync(messageData.tg_id, 
                messageData.text, 
                replyMarkup: messageData.markup);
            else await client.SendTextMessageAsync(messageData.tg_id, messageData.text);
            return true;
        }
    }
}
