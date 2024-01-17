using Kinmatch.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Kinmatch.KeyboardHandler
{
    /// <summary>
    /// Парсинг значений из фраз + маркап кейборды
    /// </summary>
    public static class Keyboard
    {
        public static IReplyMarkup SingleMarkup(string text)
        {
            var replyKeyboardMarkup = new ReplyKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        new KeyboardButton(text)
                    }
                });

            replyKeyboardMarkup.ResizeKeyboard = true;
            replyKeyboardMarkup.OneTimeKeyboard = true;
            //replyKeyboardMarkup.Selective = true;
            return replyKeyboardMarkup;
        }
        public static IReplyMarkup Languages()
        {
            var keyboardButtons = new List<KeyboardButton[]>();
            foreach (var language in Phrases.languageValues)
            {
                keyboardButtons.Add(new[] { new KeyboardButton(language) });
            }

            var replyKeyboardMarkup = new ReplyKeyboardMarkup(keyboardButtons.ToArray())
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };

            return replyKeyboardMarkup;
        }
        public static IReplyMarkup Gender()
        {
            var keyboardButtons = new List<KeyboardButton[]>();

            foreach (var gender in Phrases.genderValues)
            {
                //keyboardButtons.Add(new[] { new KeyboardButton(gender) });
                keyboardButtons.Add(new[] { new KeyboardButton(gender) });
            }

            var replyKeyboardMarkup = new ReplyKeyboardMarkup(keyboardButtons.ToArray())
            {
                ResizeKeyboard = true,
                //InputFieldPlaceholder = "test",
                OneTimeKeyboard = true
            };

            return replyKeyboardMarkup;
        }

    }
}
