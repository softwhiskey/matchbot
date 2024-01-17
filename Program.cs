using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MySql.Data.MySqlClient;
using Kinmatch.Database;

namespace Kinmatch
{
    internal class Program
    {
        //internal static ITelegramBotClient client = null;
        static void Main(string[] args) => new Program().RunBotAsync(args).GetAwaiter().GetResult();
        private async Task RunBotAsync(string[] args)
        {
            //using (var dbContext = new Context())
            //{
            //    var db = new DB1();

            //    var profile = await db.GetProfile(dbContext, 846494886);
            //    var aa = 1;
            //}
            await Initialization.Init();
            ITelegramBotClient client = new TelegramBotClient(Config.TelegramAPI.token);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new UpdateType[2] 
                {
                    UpdateType.Message,
                    UpdateType.CallbackQuery
                }
            };
            client.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            await Task.Delay(-1);
        }
        static InlineKeyboardMarkup markup()
        {
            return new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Управление обращением", "https://t.me/kinmatch_bot?start=4497"),
                    },
                }
            );
        }
        internal static List<long> cachedUsers = new List<long>();
        private static async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken arg3)
        {
            /*
                var test = await _telegramClient.GetFileAsync(message.Photo[message.Photo.Count() - 1].FileId);

                var image = Bitmap.FromStream(test.FileStream);

                image.Save(@"C:\\Users\xxx\Desktop\test.png");
             */
            if (!(update is { } u)) return;
            if (u.ChannelPost != null) return;
            if (u.Message?.Chat?.Id == null) return;
            long tg_id = u.Message.Chat.Id;

            if (u.Type == UpdateType.Message && u.Message?.Text is string messageText)
            {
                if (messageText.StartsWith("/"))
                {
                    // pass username and attachments
                    _ = Commands.CommandsHandler
                        .HandleCommand(new Entities.MessageData(tg_id, messageText, null, null), tg_id, 
                            new Entities.UpdateHandlerBody(client, tg_id, null, null));
                }
                else
                {
                    if (messageText == "default") return;
                    try
                    {
                        _ = KeyboardHandler.KeyboardHandler
                            .HandleKeyboard(new Entities.MessageData(tg_id, messageText, null, null), tg_id, 
                                new Entities.UpdateHandlerBody(client, tg_id, null, null));
                    }
                    catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                }
            }
            else if (u.Type == UpdateType.CallbackQuery && u.CallbackQuery?.Data is string callbackData)
            {
                await client.AnswerCallbackQueryAsync(u.CallbackQuery.Id);
                if (callbackData.StartsWith("!"))
                {
                    _ = Callbacks.CallbacksHandler.HandleCallback(callbackData, tg_id, new Entities.UpdateHandlerBody(client, 1, null, null));
                }
                else
                {
                    // non listed callb
                }
            }
        }
        private async Task HandleErrorAsync(ITelegramBotClient client, Exception update, CancellationToken arg3)
        {
            
            await Task.CompletedTask;
        }
    }
}