using Kinmatch.Controllers;
using Kinmatch.Entities;
using Kinmatch.Localization;
using System.Reflection;
using Telegram.Bot;
using Kinmatch.KeyboardHandler;

namespace Kinmatch.States
{
    /// <summary>
    /// Обработчик всех доступных стейтов
    /// </summary>
    internal static class StateHandler
    {
        private static readonly Dictionary<string, MethodInfo> stateMethodMap = new Dictionary<string, MethodInfo>();

        static StateHandler()
        {
            foreach (MethodInfo methodInfo in typeof(StateHandler).GetMethods(BindingFlags.Public | BindingFlags.Static))
            {
                foreach (StateAttribute keyboardAttribute in methodInfo.GetCustomAttributes<StateAttribute>())
                {
                    stateMethodMap[keyboardAttribute.Key] = methodInfo;
                }
            }
        }
        [State("start")]
        public static async Task<StateOutput> start(params object[] args)
        {
            UpdateHandlerBody? update = args.OfType<UpdateHandlerBody>().FirstOrDefault();
            return new StateOutput("choose_lang",
                await Phrases.GetPhraseAsync("choose_lang", update.profile?.language), Keyboard.Languages());
        }
        /// <summary>
        /// При вводе /start, выбор языка
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [State("choose_lang")]
        public static async Task<StateOutput> choose_lang(params object[] args)
        {
            UpdateHandlerBody? update = args.OfType<UpdateHandlerBody>().FirstOrDefault();
            MessageData? messageData = update?.messageData;
            Profile? profile = update?.profile;
            if (messageData?.text == null) return new StateOutput("choose_lang", 
                await Phrases.GetPhraseAsync("wrong_option", profile?.language));

            string text = messageData.text;

            if (Phrases.languageValues.IndexOf(text) is int index)
            {
                if (Phrases.languageKeys[index] is string key)
                {
                    await UserController.UpdateProfileColumn("language", key, profile?.tg_id);
                    return new StateOutput("your_age",
                        await Phrases.GetPhraseAsync("your_age", key));
                }
            }
            return new StateOutput("choose_lang",
                await Phrases.GetPhraseAsync("wrong_option", profile?.language));
        }

        [State("your_age")]
        public static async Task<StateOutput?> your_age(params object[] args)
        {
            UpdateHandlerBody? update = args.OfType<UpdateHandlerBody>().FirstOrDefault();
            MessageData? messageData = update.messageData;
            Profile? profile = update?.profile;
            if (messageData?.text == null) return new StateOutput("your_age",
                await Phrases.GetPhraseAsync("ex_your_age", profile?.language));
            string text = messageData.text;
            Int32.TryParse(text, out int age);
            if (age != 0 && (age > 11 && age < 90))
            {
                await UserController.UpdateProfileColumn("age", age, profile?.tg_id);
                return new StateOutput("your_gender",
                    await Phrases.GetPhraseAsync("your_gender", profile?.language), Keyboard.Gender());
            }
            return new StateOutput("your_age", 
                await Phrases.GetPhraseAsync("ex_your_age", profile?.language));
        }
        [State("your_gender")]
        public static async Task<StateOutput?> your_gender(params object[] args)
        {
            UpdateHandlerBody? update = args.OfType<UpdateHandlerBody>().FirstOrDefault();
            MessageData? messageData = update.messageData;
            Profile? profile = update?.profile;
            if (messageData?.text == null) return new StateOutput("your_gender",
                await Phrases.GetPhraseAsync("wrong_option", profile?.language));

            string text = messageData.text;

            if (Phrases.genderValues.IndexOf(text) is int index)
            {
                if (Phrases.genderKeys[index] is int key)
                {
                    await UserController.UpdateProfileColumn("gender", key, profile?.tg_id);
                    return new StateOutput("looking_for",
                        await Phrases.GetPhraseAsync("looking_for", profile?.language), Keyboard.Languages());
                }
            }
            return new StateOutput("your_gender",
                await Phrases.GetPhraseAsync("wrong_option", profile?.language));
        }
        [State("search_gender")]
        public static async Task<StateOutput?> search_gender(params object[] args)
        {
            UpdateHandlerBody? update = args.OfType<UpdateHandlerBody>().FirstOrDefault();
            MessageData? messageData = update?.messageData;
            Profile? profile = update?.profile;
            if (messageData?.text == null) return new StateOutput("your_gender",
                await Phrases.GetPhraseAsync("wrong_option", profile?.language));

            string text = messageData.text;

            if (Phrases.genderValues.IndexOf(text) is int index)
            {
                if (Phrases.genderKeys[index] is int key)
                {
                    await UserController.UpdateProfileColumn("gender", key, profile?.tg_id);
                    return new StateOutput("choose_lang",
                        await Phrases.GetPhraseAsync("choose_lang", profile?.language), Keyboard.Languages());
                }
            }
            return new StateOutput("your_gender",
                await Phrases.GetPhraseAsync("wrong_option", profile?.language));
        }
        [State("teststate1")]
        public static async Task<StateOutput?> test3(params object[] args)
        {
            Console.WriteLine("teststate3 State invoked");
            return new StateOutput("teststate1", "");
        }
        public static async Task<StateOutput> HandleState(string stateName, params object[] args)
        {
            string currentStateName = stateName;

            if (stateMethodMap.TryGetValue(currentStateName, out MethodInfo methodInfo))
            {
                Task<StateOutput> methodTask = (Task<StateOutput>)methodInfo.Invoke(null, new object[] { args });
                return await methodTask;
            }
            else
            {
                Console.WriteLine($"State '{currentStateName}' not recognized");
                return new StateOutput("", "");
            }
        }

    }
}
