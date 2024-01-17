using Kinmatch.Controllers;
using Kinmatch.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Kinmatch.Callbacks
{
    /// <summary>
    /// Обработчик коллбеков.
    /// </summary>
    internal static class CallbacksHandler
    {
        private static readonly Dictionary<string, MethodInfo> CallbackMethodMap = new Dictionary<string, MethodInfo>();
        private static List<long> LockedUsers = new List<long>();
        static CallbacksHandler()
        {
            foreach (MethodInfo methodInfo in typeof(CallbacksHandler).GetMethods(BindingFlags.Public | BindingFlags.Static))
            {
                CallbackAttribute? callbackAttribute = methodInfo.GetCustomAttribute<CallbackAttribute>();
                if (callbackAttribute != null)
                {
                    CallbackMethodMap[callbackAttribute.Name] = methodInfo;
                }
            }
        }

        [Callback("start")]
        public static async Task Start(params object[] args)
        {
            ITelegramBotClient? client = args.OfType<ITelegramBotClient>().FirstOrDefault();
            //await client.SendTextMessageAsync();
            Console.WriteLine("Start command invoked");
            await Task.CompletedTask;
        }

        [Callback("stop")]
        public static async Task Stop(params object[] args)
        {
            await Task.CompletedTask;
        }

        [Callback("kick")]
        public static async Task Kick(params object[] args)
        {
            string? conn = args.OfType<string>().FirstOrDefault();
            int? conn1 = args.OfType<int>().FirstOrDefault();
            Console.WriteLine("Kick callback invoked string" + conn);
            Console.WriteLine("Kick callback invoked int " + conn1);
            await Task.CompletedTask;
        }

        public static async Task HandleCallback(string message, long tg_id, params object[] args)
        {
            if (!Program.cachedUsers.Contains(tg_id))
            {
                Profile? profile = await UserController.CreateGetUserProfile(tg_id);
                if (profile == null)
                {
                    await MessageController.SendTextMessage(args.OfType<ITelegramBotClient>().FirstOrDefault(),
                        new MessageData(tg_id, "Error. Try again later.", null, null
                        )
                    );
                }
                else
                {
                    if (!Program.cachedUsers.Contains(tg_id)) Program.cachedUsers.Add(tg_id);
                    // args = args.Append(profile).ToArray();
                    args.OfType<UpdateHandlerBody>().FirstOrDefault().profile = profile;
                }
            }
            if (LockedUsers.Contains(tg_id)) return;
            string callbackName = message.Split(' ')[0];
            
            if (CallbackMethodMap.TryGetValue(callbackName, out MethodInfo methodInfo))
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
                Console.WriteLine($"Callback '{callbackName}' not recognized");
            }
        }
    }
}
