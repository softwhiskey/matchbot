using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Kinmatch.States
{
    internal class StateOutput
    {
        public string nextStateName { get; set; }
        //make it messagedata
        public string message { get; set; }
        public IReplyMarkup markup { get; set; }
        public StateOutput(string nextStateName, string message, IReplyMarkup? markup = null)
        {
            this.nextStateName = nextStateName;
            this.message = message;
            this.markup = markup;
        }
    }
}
