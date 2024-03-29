﻿Dictionary<int, IDay> Days = new Dictionary<int, IDay>();

Days.Add(1, new Day1());
Days.Add(2, new Day2());
Days.Add(3, new Day3());
Days.Add(4, new Day4());
Days.Add(5, new Day5());


int DayToRun = Days.Last().Key;

var star1 = Days[DayToRun].Star1();
var output = star1 == -1 ? Days[DayToRun].Output: star1.ToString(); 
Console.WriteLine($"Star 1: { output}");

var star2 = Days[DayToRun].Star2();
output = star2 == -1 ? Days[DayToRun].Output: star2.ToString(); 
Console.WriteLine($"Star 2: { output}");