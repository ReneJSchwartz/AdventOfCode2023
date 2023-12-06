using System.Text.RegularExpressions;
using System.Linq;

namespace Utils.Parser
{
    public static class Parser
    {
        public static List<int> GetSingleNumbersInt(string text)
        {
            List<int> numbers = new();
            int parsedInt = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (int.TryParse(text[i].ToString(), out parsedInt))
                {
                    numbers.Add(parsedInt);
                }
            }
            return numbers;
        }

        public static List<int> GetNumbersInt(string text)
        {
            Regex numbersRegex = new Regex("\\d+");
            return numbersRegex.Matches(text)
                .Cast<Match>()
                .Select(m => int.Parse(m.Value))
                .ToList();
        }

        public static List<long> GetNumbersLong(string text)
        {
            Regex numbersRegex = new Regex("\\d+");
            return numbersRegex.Matches(text)
                .Cast<Match>()
                .Select(m => long.Parse(m.Value))
                .ToList();
        }
    }
}
