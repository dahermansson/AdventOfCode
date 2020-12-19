using System.Linq;
using System;
using System.Collections.Generic;

namespace AoC2020
{
    public class Dag15 : IDag
    {
        
        List<int> input = InputReader.GetIntArrayFromSingleLine("dag15.txt").ToList();
        
        public string Output => throw new System.NotImplementedException();
        public int Star1() => PlayGame(2020);
        public int Star2() => PlayGame(30000000);

        private int PlayGame(int turns)
        {
            var game = new Dictionary<int, int>();
            int spoken = 1;
            input.ForEach(t => game.Add(input[spoken - 1], spoken++));
            var recentSpoken = input.Last();
            var lastTime = 0;
            var beSpoken = 0;
            while(spoken <= turns)
            {
                if(lastTime == 0)
                {
                    beSpoken = 0;
                    game.TryGetValue(beSpoken, out lastTime);
                    game[beSpoken] = spoken++;
                    recentSpoken = beSpoken;
                }
                else
                {
                    beSpoken = game[recentSpoken] - lastTime;
                    game.TryGetValue(beSpoken, out lastTime);
                    game[beSpoken] = spoken++;
                    recentSpoken = beSpoken;
                }
            }
            return recentSpoken;
        }

        
    }
}