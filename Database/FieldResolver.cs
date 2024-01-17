using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinmatch.Database
{
    internal static class FieldResolver
    {
        public static async Task<string> test()
        {
            Console.WriteLine("test started");
            await Task.Delay(100000);
            Console.WriteLine("test endeed");
            return "";
        }
    }
}
