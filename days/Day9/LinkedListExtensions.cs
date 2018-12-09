using System;
using System.Collections.Generic;
using System.Linq;

namespace Day9
{
    public static class LinkedListExtensions
    {
        public static void Rotate<T>(this LinkedList<T> list, int amount)
        {
            if (list.Count <= 1)
                return;

            for (var i = 0; i < Math.Abs(amount); i++)
                list.Rotate(amount > 0);
        }

        private static void Rotate<T>(this LinkedList<T> list, bool right)
        {
            if (right)
            {
                var last = list.Last.Value;
                list.RemoveLast();
                list.AddFirst(last);
            }
            else
            {
                var first = list.First.Value;
                list.RemoveFirst();
                list.AddLast(first);
            }
        }
    }
}