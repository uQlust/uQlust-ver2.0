using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uQlustCore
{
    class DebugClass
    {
        static bool DEBUG = false;
        public static void WriteMessage(string message)
        {
            if(DEBUG)
                Console.WriteLine(message);
        }
    }
}
