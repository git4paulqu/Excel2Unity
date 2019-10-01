using System;

namespace Excel2Unity.Source.Common
{
    public partial class Utility
    {
        public class Logger
        {
            public static void Log(string log)
            {
                Console.WriteLine(log);
            }

            public static void Log(string log, params object[] args)
            {
                Console.WriteLine(string.Format(log, args));
            }
        }
    }
}