using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using MySql.Data.MySqlClient;
using Kinmatch.Entities;
using Kinmatch.States;
using Kinmatch.Controllers;
using Kinmatch.Database;
using Kinmatch.KeyboardHandler;

namespace Kinmatch.Commands
{
    /// <summary>
    /// Обработчик комманд
    /// </summary>
    internal static class CommandsHandler
    {
        private static readonly Dictionary<string, MethodInfo> CommandMethodMap = new Dictionary<string, MethodInfo>();
        private static List<long> LockedUsers = new List<long>();
        static CommandsHandler()
        {
            foreach (MethodInfo methodInfo in typeof(CommandsHandler).GetMethods(BindingFlags.Public | BindingFlags.Static))
            {
                CommandAttribute? commandAttribute = methodInfo.GetCustomAttribute<CommandAttribute>();
                if (commandAttribute != null)
                {
                    CommandMethodMap[commandAttribute.Name] = methodInfo;
                }
            }
        }
        [Command("/debug")]
        public static async Task Debug(params object[] args)
        {
            Entities.UpdateHandlerBody? updateHandler = args.OfType<Entities.UpdateHandlerBody>().FirstOrDefault();
            await MessageController.SendTextMessage(
                updateHandler?.client,
                new MessageData(updateHandler.tg_id, "test debug", null, null));
        }
        [Command("/start")]
        public static async Task Start(params object[] args)
        {
            // only lang choose if profile existed
            UpdateHandlerBody? update = args.OfType<UpdateHandlerBody>().FirstOrDefault();
            ITelegramBotClient client = update.client;
            long tg_id = update.tg_id;
            Profile? profile = update.profile;
            // Profile? profile = await UserController.CreateGetUserProfile(tg_id);
            if (profile == null)
            {
                await MessageController.SendTextMessage(client,
                        new MessageData(tg_id, "Error. Try again later.", null, null));
                return;
            }
            StateOutput stateOutput = (await StateHandler.HandleState("start", update, profile));
            profile.action = stateOutput.nextStateName;
            await UserController.UpdateProfileColumn("action", profile.action, profile?.tg_id);
            await MessageController.SendTextMessage(client, 
                    new MessageData(tg_id, stateOutput.message, null, stateOutput.markup));
        }

        [Command("/stop")]
        public static async Task Stop(params object[] args)
        {
            await FieldResolver.test();
            Console.WriteLine("Stop command invoked");
            await Task.CompletedTask;
        }
        private static string currentState1 = "teststate1";
        [Command("/state")]
        public static async Task state(params object[] args)
        {
            string? conn = args?.OfType<string>().FirstOrDefault();
            currentState1 = (await StateHandler.HandleState(currentState1)).nextStateName;
        }

        [Command("/kick")]
        public static async Task Kick(params object[] args)
        {
            string? conn = args?.OfType<string>().FirstOrDefault();
            int? conn1 = args?.OfType<int>().FirstOrDefault();
            Console.WriteLine("Kick command invoked string" + conn);
            Console.WriteLine("Kick command invoked int " + conn1);
            //Console.WriteLine(await Localization.Phrases.GetPhraseAsync("greeting", "en"));
            await Task.CompletedTask;
        }

        public static async Task HandleCommand(MessageData messageData, long tg_id, params object[] args)
        {
            //string tmp = await new Database.DB().test();
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
            string commandName = messageData.text.Split(' ')[0];
            args.OfType<UpdateHandlerBody>().FirstOrDefault().messageData = messageData;
            if (CommandMethodMap.TryGetValue(commandName, out MethodInfo? methodInfo))
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
                Console.WriteLine($"Command '{commandName}' not recognized");
            }
        }
    }
}
