﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventCalendar2024
{
    public static class MathHelpers
    {
        public static T GreatestCommonDivisor<T>(T a, T b) where T : INumber<T>
        {
            while (b != T.Zero)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public static T LeastCommonMultiple<T>(T a, T b) where T : INumber<T>
            => a / GreatestCommonDivisor(a, b) * b;

        public static T LeastCommonMultiple<T>(this IEnumerable<T> values) where T : INumber<T>
            => values.Aggregate(LeastCommonMultiple);
    }
}
