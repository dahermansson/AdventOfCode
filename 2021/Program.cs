
using AoC.Utils;
using AoC2021;

Dictionary<int, IDay> Days = new Dictionary<int, IDay>();
int DayToRun = 1;

Days.Add(1, new Day1());

var star1 = Days[DayToRun].Star1();
var output = star1 == -1 ? Days[DayToRun].Output: star1.ToString(); 
Console.WriteLine($"Star 1: { output}");

var star2 = Days[DayToRun].Star2();
output = star2 == -1 ? Days[DayToRun].Output: star2.ToString(); 
Console.WriteLine($"Star 2: { output}");