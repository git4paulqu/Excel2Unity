using System;

namespace Excel2Unity.Source.Common
{
    public partial class Utility
    {
        public class Asserter
        {
            public static void Assert(bool condition, string error)
            {
                if (!condition)
                    throw new Exception(error);
            }

            public static void Assert(bool condition, string error, params object[] args)
            {
                if (!condition)
                    throw new Exception(string.Format(error, args));
            }

            public static void AssertStringNotNull(string str, string error)
            {
                Assert(!string.IsNullOrEmpty(str), error);
            }
        }
    }
}
