using System;
using System.Collections.Generic;
using AoC.Utils;
using AoC2020;

Dictionary<int, IDay> Days = new Dictionary<int, IDay>();

int DayToRun = 24;
      
Days.Add(1, new Dag1());
Days.Add(2, new Dag2());
Days.Add(3, new Dag3());
Days.Add(4, new Dag4());
Days.Add(5, new Dag5());
Days.Add(6, new Dag6());
Days.Add(8, new Dag8());
Days.Add(9, new Dag9());
Days.Add(10, new Dag10());
Days.Add(11, new Dag11());
Days.Add(12, new Dag12());
Days.Add(13, new Dag13());
Days.Add(14, new Dag14());
Days.Add(15, new Dag15());
Days.Add(20, new Dag20());
Days.Add(22, new Dag22());
Days.Add(23, new Dag23());
Days.Add(24, new Dag24());

var star1 = Days[DayToRun].Star1();
var output = star1 == -1 ? Days[DayToRun].Output: star1.ToString(); 
Console.WriteLine($"Star 1: { output}");

var star2 = Days[DayToRun].Star2();
output = star2 == -1 ? Days[DayToRun].Output: star2.ToString(); 
Console.WriteLine($"Star 2: { output}");
