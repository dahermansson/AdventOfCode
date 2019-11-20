using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace star1
{
    class Program
    {
        static void Main(string[] args)
        {
            var records = new List<Record>();
            foreach (var line in File.ReadAllLines("input.txt"))
                records.Add(new Record(line));
            var orderdRecords = records.OrderBy(t => t.Date).ToList();

            List<Guard> guards = new List<Guard>();

            Guard currentGuard = null;
            for (int i = 0; i < orderdRecords.Count; i++)
            {
                var record = orderdRecords[i];
                if(record.Line.IndexOf("Guard") > -1)
                {
                    var newGuard = new Guard(record.Line);
                    if(guards.Any(t => t.ID == newGuard.ID))
                    {
                        currentGuard = guards.Single(t => t.ID == newGuard.ID);
                    }
                    else
                    {
                        currentGuard = newGuard;
                        guards.Add(currentGuard);
                    }
                }
                else
                {
                    var sleep = orderdRecords[i].Line;
                    var awake = orderdRecords[i+1].Line;
                    int startSleep = int.Parse(sleep.Substring(15, 2));
                    int endSleep = int.Parse(awake.Substring(15, 2));

                    currentGuard.TotalSleepTime += endSleep - startSleep;

                    for (int c = startSleep; c < endSleep; c++)
                    {
                        currentGuard.Sleep[c]++;
                    }
                    i++; //Jump two!!!
                }   
            }

            int max = 0;
            Guard maxSleepingGuard = null;
            foreach (var guard in guards)
            {
                if(guard.TotalSleepTime > max)
                {
                    max = guard.TotalSleepTime;
                    maxSleepingGuard = guard;
                }
            }

            Guard mostFrequentSleepingGuard = null;
            int freq = 0;
            foreach (var guard in guards)
            {
                if(guard.Sleep.Max() > freq)
                {
                    freq = guard.Sleep.Max();
                    mostFrequentSleepingGuard = guard;
                }
            }

            var sleepMinute = maxSleepingGuard.Sleep.ToList().IndexOf(maxSleepingGuard.Sleep.Max());
            var freqSleepMinute = mostFrequentSleepingGuard.Sleep.ToList().IndexOf(freq);

            Console.WriteLine($"Id: {maxSleepingGuard.ID} sleeps: {sleepMinute} id * sleep = {maxSleepingGuard.ID * sleepMinute}");
            Console.WriteLine($"Id: {mostFrequentSleepingGuard.ID} sleeps: {freqSleepMinute} id * sleep = {mostFrequentSleepingGuard.ID * freqSleepMinute}");
        }
    }

    internal class Record
    {
        public DateTime Date { get; set; }
        public string Line { get; set; }
        public Record(string line)
        {
            var split = line.Split(" ");
            Date = DateTime.Parse(split[0].TrimStart('[') +" "+ split[1].TrimEnd(']'));
            Line = line;
        }
    }

    internal class Guard
    {
        public int ID { get; set; }
        public int[] Sleep { get; set; }
        public int TotalSleepTime { get; set; }
        public Guard(string guard)
        {
            ID = int.Parse(guard.Split(" ")[3].Trim('#'));
            Sleep = new int[60];
        }
    }
}
