using System.IO;

namespace Utils.IO
{
    public static class IO
    {
        public static string[] Lines(string path)
        {
            return File.ReadAllLines(path);
        }

        public static void Print(object message)
        {
            Console.WriteLine(message.ToString());
        }
    }
}