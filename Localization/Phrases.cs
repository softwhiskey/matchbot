using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kinmatch.Localization
{
    public static class Phrases
    {
        private static ConcurrentDictionary<(string, string), string> _phrases = new ConcurrentDictionary<(string, string), string>();

        public static List<string> languageKeys = new List<string> { "ru", "en" };
        public static List<string> languageValues = new List<string> { "🇷🇺 Russian", "🇬🇧 English" };

        public static List<int> genderKeys = new List<int> { 0, 1 };
        public static List<string> genderValues = new List<string> { "👨", "👩" };

        //♂️ - ♀️ - 

        static Phrases()
        {
            //LoadPhrases();
        }

        public static async Task<string> GetPhraseAsync(string key, string? lang)
        {
            if (string.IsNullOrEmpty(lang)) lang = "ru";
            if (_phrases.TryGetValue((key, lang), out string phrase))
            {
                return phrase;
            }
            return key;
        }

        internal static async Task LoadPhrases()
        {
            using (StreamReader r = new StreamReader("localization/phrases.json"))
            {
                string json = await r.ReadToEndAsync();
                Dictionary<string, Dictionary<string, string>>? phrases = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);
                foreach (var phrase in phrases)
                {
                    foreach (var translation in phrase.Value)
                    {
                        _phrases.TryAdd((phrase.Key, translation.Key), translation.Value);
                    }
                }
            }
        }
    }
}
