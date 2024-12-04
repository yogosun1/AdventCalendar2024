using System;
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

        public static List<List<T>> TransposeMatrix<T>(List<List<T>> matrix)
        {
            List<List<T>> transposedMatrix = new List<List<T>>();
            for (int i = 0; i < matrix.First().Count; i++)
            {
                List<T> newRow = new List<T>();
                for (int j = 0; j < matrix.Count; j++)
                    newRow.Add(matrix[j][i]);
                transposedMatrix.Add(newRow);
            }
            return transposedMatrix;
        }

        public static List<List<T>> Rotate2DArray<T>(List<List<T>> matrix)
        {
            List<List<T>> transposedMatrix = new List<List<T>>();
            for (int i = 0; i < matrix.First().Count; i++)
            {
                List<T> newRow = new List<T>();
                for (int j = matrix.Count - 1; j >= 0; j--)
                    newRow.Add(matrix[j][i]);
                transposedMatrix.Add(newRow);
            }
            return transposedMatrix;
        }

        public static T[,] Rotate2DArray<T>(T[,] matrix)
        {
            T[,] transposedMatrix = new T[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(1); i++)
                for (int j = matrix.GetLength(0) - 1; j >= 0; j--)
                    transposedMatrix[i, j] = matrix[j, i];
            return transposedMatrix;
        }

        public static char[,] Rotate45Degrees(char[,] array)
        {
            int n = array.GetLength(0);
            int m = array.GetLength(1);
            int newSize = n + m - 1;
            char[,] rotatedArray = new char[newSize, newSize];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    int newX = i + j;
                    int newY = (n - 1 - i) + j;
                    rotatedArray[newX, newY] = array[i, j];
                }
            }
            return rotatedArray;
        }

        public static int[,] RotateMatrix(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            int[,] rotatedMatrix = new int[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    rotatedMatrix[j, n - 1 - i] = matrix[i, j];
            return rotatedMatrix;
        }
    }
}
