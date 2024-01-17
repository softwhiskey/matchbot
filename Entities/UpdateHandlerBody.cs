using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace Kinmatch.Entities
{
    public class UpdateHandlerBody
    {
        public ITelegramBotClient client { get; set; }
        public long tg_id { get; set; }
        public Profile? profile { get; set; }
        public MessageData? messageData { get; set; }
        public UpdateHandlerBody(ITelegramBotClient client, long msgId, Profile? profile, MessageData? messageData)
        {
            this.client = client;
            this.tg_id = msgId;
            this.profile = profile;
            this.messageData = messageData;
        }
    }
}
