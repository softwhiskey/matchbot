using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinmatch
{
    internal static class Initialization
    {
        internal static async Task Init()
        {
            await Localization.Phrases.LoadPhrases();
        }
    }
}
