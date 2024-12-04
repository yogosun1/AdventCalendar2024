using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCalendar2024
{
    public static class Extensions
    {
        public static string ReplaceAt(this string input, int index, char newChar)
        {
            char[] chars = input.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }

        public static T[,] ToMultiArray<T>(this List<List<T>> arrays)
        {
            var length = arrays[0].Count;
            var result = new T[arrays.Count, length];
            for (var i = 0; i < arrays.Count; i++)
                for (var j = 0; j < length; j++)
                    result[i, j] = arrays[i][j];
            return result;
        }
    }
}
