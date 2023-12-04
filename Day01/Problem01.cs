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
            var firstAndLastLetter = rowNumbers.First().ToString() + rowNumbers.Last().ToString();
            sum += int.Parse(firstAndLastLetter);
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
                // Look for a word and if found, increment i by word len - 2 to take overlapping into account
                if (trieNums.FindLongestNumberWordFromStringBeginning(line[i..], out int trieWordLength, out int trieFoundTextNumber))
                {
                    i += trieWordLength - 2;
                    rowNums.Add(trieFoundTextNumber);
                }
                // If word is not found, proceed to the next letter
            }

            var firstAndLastLetter = rowNums.First().ToString() + rowNums.Last().ToString();
            sumOfAllRows += int.Parse(firstAndLastLetter);
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
            TrieNode currentNode = _root;
            int lastIndex = word.Length - 1;
            for (int i = 0; i < word.Length; i++)
            {
                if (currentNode.Children.ContainsKey(word[i]))
                {
                    currentNode = currentNode.Children[word[i]];
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

                    currentNode.Children.Add(word[i], newTrie);
                    currentNode = newTrie;
                }
            }
        }

        public bool FindLongestNumberWordFromStringBeginning(string text, out int len, out int num)
        {
            len = 0;
            num = 0;

            TrieNode currentNode = _root;

            for (int i = 0; i < text.Length; i++)
            {
                if (currentNode.Children.ContainsKey(text[i]))
                {
                    currentNode = currentNode.Children[text[i]];

                    if (currentNode.EndOfWord == true)
                    {
                        len = i;
                        num = currentNode.Number;
                        if (currentNode.Children.Count == 0)
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

    // Contains additional information for custom StringNumberTrieNode type: Number, WordLength
    private class TrieNode
    {
        public Dictionary<char, TrieNode> Children = new();
        public bool EndOfWord;
        public int Number, WordLength;
    }
}