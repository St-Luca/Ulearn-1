using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete
{
    public class RightBorderTask
    {
        /// <returns>
        /// Возвращает индекс правой границы. 
        /// То есть индекс минимального элемента, который не начинается с prefix и большего prefix.
        /// Если такого нет, то возвращает items.Length
        /// </returns>
        /// <remarks>
        /// Функция должна быть НЕ рекурсивной
        /// и работать за O(log(items.Length)*L), где L — ограничение сверху на длину фразы
        /// </remarks>
        public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            // IReadOnlyList похож на List, но у него нет методов модификации списка.
            // Этот код решает задачу, но слишком неэффективно. Замените его на бинарный поиск!
            for (int i = left; right - left > 1; i++)
            {
                var m = (left + right) / 2;
                if (string.Compare(prefix, phrases[m], StringComparison.OrdinalIgnoreCase) >= 0
                    || phrases[m].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    left = m;
                }
                else
                {
                    right = m;
                }
            }
            return right;
        }
    }
}