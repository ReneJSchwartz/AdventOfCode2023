using Utils.Parser;
using Utils.IO;

public class Problem01
{
    private readonly string _testDataP1 = @"C:\Git\AdventOfCode2023\Day01\ExampleData.txt";
    private readonly string _testDataP2 = @"C:\Git\AdventOfCode2023\Day01\Part2ExampleData.txt";
    private readonly string _realData = @"C:\Git\AdventOfCode2023\Day01\Data.txt";

    public void Part1()
    {
        var sum = 0;
        foreach (var item in IO.Lines(_realData))
        {
            List<int> rowNumbers = Parser.GetSingleNumbersInt(item);
            var firstAndLast = rowNumbers.First().ToString() + rowNumbers.Last().ToString();
            sum += int.Parse(firstAndLast);
        }
        IO.Print(sum);
    }


    public void Part2()
    {
        // Follow a trie tree with strings. Note: numbers can overlap.
        Trie trieNums = new();
        trieNums.AddNumberWord("one", 1);
        trieNums.AddNumberWord("two", 2);
        trieNums.AddNumberWord("three", 3);
        trieNums.AddNumberWord("four", 4);
        trieNums.AddNumberWord("five", 5);
        trieNums.AddNumberWord("six", 6);
        trieNums.AddNumberWord("seven", 7);
        trieNums.AddNumberWord("eight", 8);
        trieNums.AddNumberWord("nine", 9);

        var sumOfAllRows = 0;
        var trieWordLength = 0;
        var trieFoundTextNumber = 0;

        List<int> rowNums = new();
        foreach (var line in IO.Lines(_realData))
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] < 58 && line[i] > 47)
                {
                    rowNums.Add(int.Parse(line[i].ToString()));
                    continue;
                }
                // look for a word and if found, increment i by word len - 2 to take overlapping into account
                if (trieNums.FindLongestNumberWordFromStringBeginning(line[i..], out trieWordLength, out trieFoundTextNumber))
                {
                    i += trieWordLength - 2;
                    rowNums.Add(trieFoundTextNumber);
                }
                // if not found, proceed to next letter
            }

            var firstAndLast = rowNums.First().ToString() + rowNums.Last().ToString();
            sumOfAllRows += int.Parse(firstAndLast);
            rowNums.Clear();
        }
        IO.Print(sumOfAllRows);
    }

    // For part 2. Trie is a fast search algorithm for words.
    private class Trie
    {
        readonly TrieNode _root;

        public Trie()
        {
            _root = new TrieNode();
        }

        public void AddNumberWord(string word, int value)
        {
            char [] chars = word.ToCharArray();
            TrieNode node = _root;
            int lastIndex = chars.Length - 1;
            for (int i = 0; i < chars.Length; i++)
            {
                if (node.Children.ContainsKey(chars[i]))
                {
                    node = node.Children[chars[i]];
                }
                else
                {
                    TrieNode newTrie = new();

                    if (lastIndex == i)
                    {
                        newTrie.EndOfWord = true;
                        newTrie.Number = value;
                        newTrie.WordLength = i + 1;
                    }

                    node.Children.Add(chars[i], newTrie);
                    node = newTrie;
                }
            }
        }

        public bool FindLongestNumberWordFromStringBeginning(string text, out int len, out int num)
        {
            char [] chars = text.ToCharArray();
            len = 0;
            num = 0;

            TrieNode node = _root;

            for (int i = 0; i < chars.Length; i++)
            {
                if (node.Children.ContainsKey(chars[i]))
                {
                    node = node.Children[chars[i]];

                    if (node.EndOfWord == true)
                    {
                        len = i;
                        num = node.Number;
                        if (node.Children.Count == 0)
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
    }

    // Contains additional information for StringNumberTrieNode type: Number, WordLength
    private class TrieNode
    {
        public Dictionary<char, TrieNode> Children = new();
        public bool EndOfWord;
        public int Number, WordLength;
    }
}