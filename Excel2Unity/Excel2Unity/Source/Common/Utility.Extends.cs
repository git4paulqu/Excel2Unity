﻿using System.Collections.Generic;

namespace Excel2Unity.Source.Common
{
    public static class Extends
    {
        public static T[] ToArray<T>(this List<T> theList)
        {
            int count = theList.Count;
            T[] array = new T[count];
            for (int i = 0; i < count; i++)
            {
                array[i] = theList[i];
            }
            return array;
        }

        public static List<T> ToList<T>(this T[] array)
        {
            List<T> lst = new List<T>(array);
            return lst;
        }
    }
}
