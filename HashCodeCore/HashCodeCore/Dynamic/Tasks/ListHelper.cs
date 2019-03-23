using System;
using System.Collections.Generic;

namespace HashCodeCore.Dynamic.Tasks
{
    public static class ListHelper
    {
        public static void Shuffle<T>(this IList<T> list, Random rnd)
        {
            for(var i=0; i < list.Count - 1; i++)
                list.Swap(i, rnd.Next(i, list.Count));
        }

        public static void RandomSwap<T>(this IList<T> list, Random rnd)
        {
            int i = rnd.Next(0, list.Count), j = i;
            while (j == i)
            {
                j = rnd.Next(0, list.Count);
            }
            list.Swap(i,j);
        }

        public static void Swap<T>(this IList<T> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}