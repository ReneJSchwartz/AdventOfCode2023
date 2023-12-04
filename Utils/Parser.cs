using System.Text.RegularExpressions;

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
    }
}
