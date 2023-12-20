using Utils.IO;
using Utils.Parser;
using System.Linq;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Diagnostics;

public class Problem07
{
    string _testDataP1 = @"C:\Git\AdventOfCode2023\Day07\ExampleData.txt";
    string _testDataP2 = @"C:\Git\AdventOfCode2023\Day07\Part2ExampleData.txt";
    string _realData = @"C:\Git\AdventOfCode2023\Day07\Data.txt";

    private bool debug = true;


    // Todo T = 10 + kirjainten vertailu, ei auta sortedset tässä ehkä ellei laita custom instructioneja
    public void Part1()
    {
        var dataStr = debug ? File.ReadAllText(_testDataP1) : File.ReadAllText(_realData);

        /*
        // 250056953 
        // 250083635 (too low)
        // test data is right
        // dataStr = dataStr.Replace("A", "A");
        dataStr = dataStr.Replace("K", "B");
        dataStr = dataStr.Replace("Q", "C");
        dataStr = dataStr.Replace("J", "D");
        dataStr = dataStr.Replace("T", "E");
        */


        // test data 6833 (should be 6440)
        // 249849009 (too low)
        /* 
        dataStr = dataStr.Replace("A", "E");
        dataStr = dataStr.Replace("K", "D");
        dataStr = dataStr.Replace("Q", "C");
        dataStr = dataStr.Replace("J", "B");
        dataStr = dataStr.Replace("T", "A");
        */

        dataStr = dataStr.Replace("T", ":");
        dataStr = dataStr.Replace("J", ";");
        dataStr = dataStr.Replace("Q", "<");
        dataStr = dataStr.Replace("K", "=");
        dataStr = dataStr.Replace("A", ">");


        // var data = IO.Lines(dataStr);
        var data = dataStr.Split("\n");

        var sortedData = new SortedSet<string>(data);

        Func<string, bool>[] cardTypes = { IsFiveOfAKind, IsFourOfAKind, IsFullHouse, IsThreeOfAKind, IsTwoPair, IsOnePair, IsHighCard }; // { IsHighCard, IsOnePair, IsTwoPair, IsThreeOfAKind, IsFullHouse, IsFourOfAKind, IsFiveOfAKind };

        if (debug)
        {
            // IO.Print(sortedData.Count);
            TestCards();
        }

        var totalSum = 0;
        // pseudocode:
        // linq, if elses, ordering
        var rank = sortedData.Count; // most valuable card first

        for (int i = 0; i < cardTypes.Length; i++)
        {

            var sortedType = new SortedSet<string>();
            var compareMethod = cardTypes[i];

            IO.Print(compareMethod.Method.Name + ", unsorted cards remaining count: " + sortedData.Count);

            foreach (var item in sortedData)
            {
                if (compareMethod(item[..5]))
                    sortedType.Add(item);
            }
            foreach (var item in sortedType)
            {
                totalSum += rank * Parser.GetNumbersInt(item[6..]).First();
                rank--;

                if (debug)
                {
                    IO.Print(item[6..] + " * " + (rank));
                }
            }
            foreach (var item in sortedType)
            {
                sortedData.Remove(item); // should make next searches faster
            }
        }

        IO.Print(totalSum);
    }

    private void TestCards()
    {
        Debug.Assert(IsFiveOfAKind("AAAAA"), "IsFiveOfAKind fail");
        Debug.Assert(IsFiveOfAKind("22222"), "IsFiveOfAKind fail");
        Debug.Assert(!IsFiveOfAKind("AAAAB"), "IsFiveOfAKind fail");
        Debug.Assert(!IsFiveOfAKind("123BC"), "IsFiveOfAKind fail");

        Debug.Assert(IsFourOfAKind("AAAAB"), "IsFourOfAKind fail");
        Debug.Assert(IsFourOfAKind("32333"), "IsFourOfAKind fail");
        Debug.Assert(!IsFourOfAKind("33333"), "IsFourOfAKind fail");
        Debug.Assert(!IsFourOfAKind("32351"), "IsFourOfAKind fail");

        Debug.Assert(IsFullHouse("11222"), "IsFullHouse fail");
        Debug.Assert(IsFullHouse("A222A"), "IsFullHouse fail");
        Debug.Assert(!IsFullHouse("A232A"), "IsFullHouse fail");
        Debug.Assert(!IsFullHouse("A312A"), "IsFullHouse fail");

        Debug.Assert(IsThreeOfAKind("AAA12"));
        Debug.Assert(IsThreeOfAKind("AABBB"));
        Debug.Assert(!IsThreeOfAKind("AAAAA"));
        Debug.Assert(!IsThreeOfAKind("12344"));

        Debug.Assert(IsTwoPair("12312"));
        Debug.Assert(IsTwoPair("1122A"));
        Debug.Assert(!IsTwoPair("AAABB")); // technically a fullhouse so failing
        Debug.Assert(!IsTwoPair("12345"));
        Debug.Assert(!IsTwoPair("1136A"));
        Debug.Assert(!IsTwoPair("AAAAB"));
        Debug.Assert(!IsTwoPair("15312"));

        Debug.Assert(IsOnePair("15312"));
        Debug.Assert(!IsOnePair("1AAA1"));
        Debug.Assert(!IsOnePair("12345"));

        Debug.Assert(IsHighCard("lorem ipsum")); // everything goes
    }

    bool IsFiveOfAKind(string item)
    {
        int count = 0;
        char card = item[0];
        foreach (char c in item)
            if (c == card)
                count++;

        if (count == 5)
            IO.Print(item);

        return count == 5;
    }

    bool IsFourOfAKind(string item)
    {
        int count = 0;

        for (int i = 0; i < 2; i++)
        {
            count = 0;
            char card = item[i];

            foreach (char c in item)
                if (c == card)
                    count++;

            if (count == 4)
                return true;
        }

        return false;
    }

    // where three cards have the same label, and the remaining two cards share a different label: 23332
    bool IsFullHouse(string item)
    {
        if (IsThreeOfAKind(item))
        {
            // Could also use HashSet
            var foundCards = new Dictionary<char, int>();
            foreach (var character in item)
            {
                if (foundCards.TryAdd(character, 1) == false)
                {
                    foundCards[character] += 1;
                }
            }
            return foundCards.Count == 2;
        }
        return false;
    }

    bool IsThreeOfAKind(string item)
    {
        int count = 0;

        for (int i = 0; i < 3; i++)
        {
            count = 0;
            char card = item[i];

            foreach (char c in item)
                if (c == card)
                    count++;

            if (count == 3)
                return true;
        }

        return false;
    }

    bool IsTwoPair(string item)
    {
        var foundCards = new Dictionary<char, int>();
        foreach (var character in item)
        {
            if (foundCards.TryAdd(character, 1) == false)
            {
                foundCards[character] += 1;
            }
        }
        if (foundCards.Count == 3)
        {
            foreach (var card in foundCards)
            {
                if (card.Value > 2)
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }

    bool IsOnePair(string item)
    {
        var foundCards = new Dictionary<char, int>();
        foreach (var character in item)
        {
            if (foundCards.TryAdd(character, 1) == false)
            {
                foundCards[character] += 1;
            }
        }

        if (foundCards.Count == 4)
        {
            return true;
        }
        return false;
    }

    bool IsHighCard(string item)
    {
        // var foundCards = new Dictionary<char, int>();
        // foreach (var character in item)
        // {
        //     if (foundCards.TryAdd(character, 1) == false)
        //     {
        //         foundCards[character] += 1;
        //     }
        // }

        // if (foundCards.Count == 5)
        // {
        //     return true;
        // }
        // return false;
        return true; // always true since every hand is also a high card hand
    }


    public void Part2()
    {

    }
}