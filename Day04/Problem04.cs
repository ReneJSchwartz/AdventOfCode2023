using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.InteropServices;
using Utils.IO;
using Utils.Parser;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Utils;

public class Problem04
{
    private readonly string _testData = @"C:\Git\AdventOfCode2023\Day04\ExampleData.txt";
    private readonly string _realData = @"C:\Git\AdventOfCode2023\Day04\Data.txt";

    private readonly int _exampleTicketsThreshold = 6;
    private readonly int _dataTicketsThreshold = 11;

    private readonly int[] _pointsFromRow = new int[] { 0, 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192 };


    public void Part1()
    {
        var totalSum = 0;
        var threshold = _dataTicketsThreshold;
        var data = _realData;
        bool debug = false;
        foreach (var line in IO.Lines(_realData))
        {
            var nums = Parser.GetNumbersInt(line);
            var winningNums = nums.Take<int>(threshold).Skip(1);
            var potentialMatches = nums.TakeLast(nums.Count - threshold);


            var matches = 0;

            foreach (var item in potentialMatches)
            {
                foreach (var winNum in winningNums)
                {
                    if (item == winNum)
                    {
                        matches++;
                    }
                }
            }
            if (matches > 0)
            {
                totalSum += _pointsFromRow[matches];
            }
            if (debug)
            {
                IO.Print(string.Join(" ", winningNums));
                IO.Print(string.Join(" ", potentialMatches));
                IO.Print("matches: " + matches + ", points: " + _pointsFromRow[matches]);
            }
        }
        IO.Print(totalSum);
    }

    public void Part2()
    {
        var accruedTickets = new Dictionary<int, int>();
        for (int i = 1; i < 300; i++)
        {
            accruedTickets.Add(i, 1);
        }

        bool debug = false;
        var threshold = debug ? _exampleTicketsThreshold : _dataTicketsThreshold;
        var data = debug ? _testData : _realData;
        var cardNum = 1;

        foreach (var line in IO.Lines(data))
        {
            var nums = Parser.GetNumbersInt(line);
            var winningNums = nums.Take<int>(threshold).Skip(1);
            var potentialMatches = nums.TakeLast(nums.Count - threshold);
            var matches = 0;
            
            foreach (var item in potentialMatches)
            {
                foreach (var winNum in winningNums)
                {
                    if (item == winNum)
                    {
                        matches++;
                    }
                }
            }

            for (int i = 1; i < matches + 1; i++)
            {
                accruedTickets[cardNum + i] += accruedTickets[cardNum];
            }

            if (debug)
            {
                IO.Print(string.Join(" ", winningNums));
                IO.Print(string.Join(" ", potentialMatches));
                IO.Print("matches: " + matches + ", points: " + _pointsFromRow[matches]);
            }
            cardNum++;
        }

        var totalTicketAmount = 0;
        if (debug)
        {
            for (int i = 1; i <= 6; i++)
            {
                totalTicketAmount += accruedTickets[i];

                if (debug)
                    IO.Print(accruedTickets[i]);
            }
        }
        else
        {
            for (int i = 1; i <= 212; i++)
            {
                totalTicketAmount += accruedTickets[i];
            }
        }
        IO.Print(totalTicketAmount);
    }
}