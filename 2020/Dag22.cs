using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020
{
    public class Dag22 : IDag
    {
        public string Output => throw new NotImplementedException();
        private int[] input = InputReader.GetInputLines("dag22.txt").Where(t => int.TryParse(t, out int i)).Select(int.Parse).ToArray();
        public int Star1() => PlayGame(input);
        public int Star2()
        {
            throw new System.NotImplementedException();
        }

        private int PlayGame(int[] deck)
        {
            var yourStack = new Queue<int>(deck.Take(25));
            var crabStack = new Queue<int>(deck.Skip(25).Take(25));
            while(yourStack.Count > 0 && crabStack.Count > 0)
                if(yourStack.Peek() > crabStack.Peek())
                {
                    yourStack.Enqueue(yourStack.Dequeue());
                    yourStack.Enqueue(crabStack.Dequeue());
                }
                else
                {
                    crabStack.Enqueue(crabStack.Dequeue());
                    crabStack.Enqueue(yourStack.Dequeue());
                }
            int k = yourStack.Count + crabStack.Count;
            return yourStack.Sum(s => s * k--) + crabStack.Sum(s => s * k--);
        }
    }
}