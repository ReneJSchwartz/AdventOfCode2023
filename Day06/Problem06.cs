using Utils.IO;
using Utils.Parser;

/// <summary> Boat races (Wait For It) </summary>
public class Problem06
{
    public void Part1()
    {
        var waysToBreakOriginalRecord = 0;
        var raceResultsMultiplied = 0;
        var debug = false;
        var times = debug ? new int[] { 7, 15, 30 } : new int[] { 48, 93, 84, 66 };
        var distances = debug ? new int[] { 9, 40, 200 } : new int[] { 261, 1192, 1019, 1063 };
        var originalRecord = 0;



        for (int i = 0; i < times.Length; i++)
        {
            waysToBreakOriginalRecord = 0;
            originalRecord = distances[i];

            for (int raceSpeed = 0; raceSpeed < times[i]; raceSpeed++)
            {
                var result = RaceResult(raceSpeed, times[i]);
                if (result > originalRecord)
                {
                    waysToBreakOriginalRecord++;
                }
            }

            if (i == 0)
            {
                raceResultsMultiplied = waysToBreakOriginalRecord;
                continue;
            }
            else
            {
                raceResultsMultiplied *= waysToBreakOriginalRecord;
            }
        }

        IO.Print(raceResultsMultiplied);

        int RaceResult(int timeToHold, int raceLength)
        {
            return (raceLength - timeToHold) * timeToHold;
        }
    }



    public void Part2()
    {
        long waysToBreakOriginalRecord = 0;
        var debug = false;
        long time = debug ? 71530 : 48938466;
        var originalRecord = debug ? 940200 : 261119210191063;
        long firstRecord = 0;
        long lastRecord = 0;


        for (long i = 0; i < time; i++)
        {
            if (RaceResult(i, time) > originalRecord)
            {
                firstRecord = i;
                IO.Print(nameof(firstRecord) + firstRecord);
                break;
            }
        }

        for (long i = time; i > -1; i--)
        {
            if (RaceResult(i, time) > originalRecord)
            {
                lastRecord = i;
                IO.Print(nameof(lastRecord) + lastRecord);
                break;
            }   
        }

        waysToBreakOriginalRecord = lastRecord - firstRecord + 1;
        IO.Print(waysToBreakOriginalRecord);

        /* 
        // calculates every step and counts the ones that break the record
        for (int raceSpeed = 0; raceSpeed < time; raceSpeed++)
        {
            var result = RaceResult(raceSpeed, time);
            if (result > originalRecord)
            {
                waysToBreakOriginalRecord++;
            }
        }
        */

        long RaceResult(long timeToHold, long raceLength)
        {
            return (raceLength - timeToHold) * timeToHold;
        }
    }
}