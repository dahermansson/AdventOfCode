
using AoC.Utils;
using AoC2021;

Dictionary<int, IDay> Days = new Dictionary<int, IDay>();


Days.Add(1, new Day1());
Days.Add(2, new Day2());
Days.Add(3, new Day3());
Days.Add(4, new Day4());
Days.Add(5, new Day5());
Days.Add(6, new Day6());
Days.Add(7, new Day7());
Days.Add(8, new Day8());
Days.Add(9, new Day9());
Days.Add(10, new Day10());
Days.Add(11, new Day11());
Days.Add(12, new Day12());
Days.Add(13, new Day13());
Days.Add(14, new Day14());
Days.Add(15, new Day15());
Days.Add(16, new Day16());
Days.Add(17, new Day17());


int dayToRun = Days.Last().Key;
var star1 = Days[dayToRun].Star1();
var output = star1 == -1 ? Days[dayToRun].Output: star1.ToString(); 
Console.WriteLine($"Star 1: { output}");

var star2 = Days[dayToRun].Star2();
output = star2 == -1 ? Days[dayToRun].Output: star2.ToString(); 
Console.WriteLine($"Star 2: { output}");