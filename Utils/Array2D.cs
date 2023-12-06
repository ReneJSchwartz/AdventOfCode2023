using Utils;
using System.Drawing;

namespace Utils
{
    public static class Array2D
    {
        public static int[][] Empty2D(int sizeY, int sizeX)
        {
            var arr = new int[sizeY][];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = new int[sizeX];
            }

            return arr;
        }

        public static void Print(int[][] arr)
        {
            if (arr[0].Length != arr.Length)
            {
                Console.WriteLine("array was jagged");
            }

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    Console.Write(arr[i][j] + " ");
                }
                Console.WriteLine();
            }
        }
    }

}