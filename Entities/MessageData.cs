using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Kinmatch.Entities
{
    public class MessageData
    {
        public long tg_id { get; set; }
        public string text { get; set; }
        public List<string>? photosLinks { get; set; }
        public IReplyMarkup? markup { get; set; }
        public MessageData(long tg_id, string text, List<string>? photosLinks, IReplyMarkup? markup)
        {
            this.tg_id = tg_id;
            this.text = text;
            this.photosLinks = photosLinks;
            this.markup = markup;
        }
    }
}
