
using AoC.Utils;
using AoC2017;

Dictionary<int, IDay> Days = new Dictionary<int, IDay>();

Days.Add(1, new Day1());

int dayToRun = Days.Last().Key;

var star1 = Days[dayToRun].Star1();
var output = star1 == -1 ? Days[dayToRun].Output: star1.ToString(); 
Console.WriteLine($"Star 1: { output}");

var star2 = Days[dayToRun].Star2();
output = star2 == -1 ? Days[dayToRun].Output: star2.ToString(); 
Console.WriteLine($"Star 2: { output}");