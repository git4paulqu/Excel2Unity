using System;
using System.Reflection;

namespace Excel2Unity.Source.Common
{
    public partial class Utility
    {
        public class Reflection
        {

            public static object CreateInstance(Type type)
            {
                return Activator.CreateInstance(type);
            }

            public static void SetValue(Type t, string name, object instance, object value, BindingFlags flag)
            {
                PropertyInfo pi = t.GetProperty(name, flag);
                if (null != pi)
                {
                    pi.SetValue(instance, value);
                }
            }
        }
    }
}
