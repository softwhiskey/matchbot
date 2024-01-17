using Kinmatch.Controllers;
using Kinmatch.Entities;
using Kinmatch.States;
using System.Reflection;
using Telegram.Bot;

namespace Kinmatch.KeyboardHandler
{
    /// <summary>
    /// Обработчик ключевых фраз без комманд (/).
    /// </summary>
    internal static class KeyboardHandler
    {
        private static readonly Dictionary<string, MethodInfo> KeyboardMethodMap = new Dictionary<string, MethodInfo>();
        private static List<long> LockedUsers = new List<long>();
        static KeyboardHandler()
        {
            foreach (MethodInfo methodInfo in typeof(KeyboardHandler).GetMethods(BindingFlags.Public | BindingFlags.Static))
            {
                foreach (KeyboardAttribute keyboardAttribute in methodInfo.GetCustomAttributes<KeyboardAttribute>())
                {
                    KeyboardMethodMap[keyboardAttribute.Keyboard] = methodInfo;
                }
            }
        }

        [Keyboard("like")]
        [Keyboard("dislike")]
        public static async Task Start(params object[] args)
        {
            ITelegramBotClient? client = args.OfType<ITelegramBotClient>().FirstOrDefault();
            //await client.SendTextMessageAsync();
            Console.WriteLine("Start keyboard invoked");
            await Task.CompletedTask;
        }

        [Keyboard("stop")]
        public static async Task Stop(params object[] args)
        {
            await Task.CompletedTask;
        }

        [Keyboard("test")]
        [Keyboard("kick")]
        [Keyboard("❤️")]
        public static async Task Kick(params object[] args)
        {
            string? conn = args.OfType<string>().FirstOrDefault();
            long? conn1 = args.OfType<long>().FirstOrDefault();
            Entities.UpdateHandlerBody? updateHandler = args.OfType<Entities.UpdateHandlerBody>().FirstOrDefault();
            Console.WriteLine("Kick text invoked string" + conn);
            Console.WriteLine("Kick text invoked int " + conn1);
            await updateHandler.client.SendTextMessageAsync(conn1, "[inline mention of a user](tg://user?id=91889230278)",
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
            await Task.CompletedTask;
        }
        [Keyboard("xyu")]
        public static async Task xyu(params object[] args)
        {
            UpdateHandlerBody? update = args.OfType<UpdateHandlerBody>().FirstOrDefault();
            Profile? profile = update.profile;
            //MessageData? messageData = update.messageData;
            ITelegramBotClient client = update.client;
            long tg_id = update.tg_id;
            await client.SendTextMessageAsync(tg_id, "test");
        }
        [Keyboard("default")]
        public static async Task Default(params object[] args)
        {
            Console.WriteLine("default");
            UpdateHandlerBody? update = args.OfType<UpdateHandlerBody>().FirstOrDefault();
            Profile? profile = update.profile;
            //MessageData? messageData = update.messageData;
            ITelegramBotClient client = update.client;
            long tg_id = update.tg_id;
            Console.WriteLine(profile.action);
            Console.WriteLine(profile.language);
            StateOutput stateOutput = (await StateHandler.HandleState(profile.action, update));
            profile.action = stateOutput.nextStateName;
            await UserController.UpdateProfileColumn("action", profile.action, profile?.tg_id);
            // keyboard
            await MessageController.SendTextMessage(client,
                        new MessageData(tg_id, stateOutput.message, null, stateOutput.markup));
            Console.WriteLine("action 2 " + profile.action);
        }
        public static async Task HandleKeyboard(MessageData messageData, long tg_id, params object[] args)
        {
            //if (!Program.cachedUsers.Contains(tg_id)){
            Profile? profile = await UserController.CreateGetUserProfile(tg_id);
            if (profile == null)
            {
                await MessageController.SendTextMessage(args.OfType<ITelegramBotClient>().FirstOrDefault(),
                    new MessageData(tg_id, "Error. Try again later.", null, null));
                return;
            }
            else
            {
                if (!Program.cachedUsers.Contains(tg_id)) Program.cachedUsers.Add(tg_id);
                // args = args.Append(profile).ToArray();
                args.OfType<UpdateHandlerBody>().FirstOrDefault().profile = profile;
            }
            //}
            if (LockedUsers.Contains(tg_id)) return;
            string keyboardName = messageData.text.Split(' ')[0];
            args.OfType<UpdateHandlerBody>().FirstOrDefault().messageData = messageData;
            if (KeyboardMethodMap.TryGetValue(keyboardName, out MethodInfo? methodInfo))
            {
                LockedUsers.Add(tg_id);
                try
                {
                    await (Task)methodInfo.Invoke(null, new object[] { args });
                }
                catch { LockedUsers.Remove(tg_id); return; }
                LockedUsers.Remove(tg_id);
                Console.WriteLine("ended");
            }
            else
            {
                Console.WriteLine($"Keyboard '{keyboardName}' not recognized");
                // refactor with lastaction name

                if (KeyboardMethodMap.TryGetValue("default", out MethodInfo defaultMethodInfo))
                {
                    await (Task)defaultMethodInfo.Invoke(null, new object[] { args });
                }
                else
                {
                    Console.WriteLine($"Keyboard '{keyboardName}' not recognized and no default task found");
                }
            }
        }

    }
}