using System.IO;

namespace Utils.IO
{
    public static class IO
    {
        public static string[] Lines(string path)
        {
            return File.ReadAllLines(path);
        }

        public static void Print(object message) => Console.WriteLine(message);

        public static void Write(object message) => Console.Write(message);
        public static void Write(int message) => Console.Write(message);
    }
}