using System.Collections.Generic;
using System.Linq;
using AoC.Utils;

namespace AoC2020
{
    public class Dag23 : IDay
    {
        public string Output => _output;
        private string _output;
        
        public int Star1()
        {
            LinkedList<int> cups = new LinkedList<int>(new int[]{3,2,7,4,6,5,1,8,9});
            var cupsLut = new Dictionary<int, LinkedListNode<int>>();
            var cupsIn = new Dictionary<int, bool>();
            var cup = cups.First;
            while(cup != null)
            {
                cupsLut.Add(cup.Value, cup);
                cupsIn[cup.Value] = true;
                cup = cup.Next;
            }
            var currentCup = cups.First;
            for (int i = 0; i < 100; i++)
                currentCup = DoMove(cups, currentCup, cupsIn, cupsLut);
            
            var one = cupsLut[1];
            for (int i = 0; i < cups.Count -1; i++)
            {
                one = one.Next ?? cups.First;
                _output += one.Value;
            }
            return -1;
        }
        
        public int Star2()
        {
            LinkedList<int> cups = new LinkedList<int>(new int[]{3,2,7,4,6,5,1,8,9});
            var cupsLut = new Dictionary<int, LinkedListNode<int>>();
            var cupsIn = new Dictionary<int, bool>();
            var cup = cups.First;
            while(cup != null)
            {
                cupsLut.Add(cup.Value, cup);
                cupsIn[cup.Value] = true;
                cup = cup.Next;
            }
            for (int i = 10; i <= 1000000; i++)
            {
                cups.AddLast(i);
                cupsLut[cups.Last.Value] = cups.Last;
                cupsIn[cups.Last.Value] = true;
            }

            var currentCup = cups.First;
            for (int i = 0; i < 10000000; i++)
                currentCup = DoMove(cups, currentCup, cupsIn, cupsLut);
            
            var nextToOne = cupsLut[1].Next;
            var nextNextToOne =nextToOne.Next;
            long res =((long)nextToOne.Value * (long)nextNextToOne.Value); 
            _output = res.ToString();
            return -1;
        }

        private LinkedListNode<int> DoMove(LinkedList<int> cups, LinkedListNode<int> currentCup, Dictionary<int, bool> cupsIn, Dictionary<int, LinkedListNode<int>> cupsLut)
        {
            var cupsToMove = new Stack<LinkedListNode<int>>();
            for (int i = 0; i < 3; i++)
            {
                var cup = currentCup.Next ?? cups.First;
                cupsToMove.Push(cup);
                cupsIn[cup.Value] = false;
                cups.Remove(cup);
            }
            var destinatCupValue = currentCup.Value == 1 ? cups.Max() : currentCup.Value - 1;
            LinkedListNode<int> destinationCup = null;
            if(cupsIn[destinatCupValue])
                destinationCup = cupsLut[destinatCupValue];
            else
            {
                while(!cupsIn[destinatCupValue])
                {
                    if(destinatCupValue <= 1)
                        destinatCupValue= cups.Max();
                    else
                        destinatCupValue--;
                }
                destinationCup = cupsLut[destinatCupValue];
            }
                
            for (int i = 2; i > -1; i--)
            {
                cupsIn[cupsToMove.Peek().Value] = true;
                cups.AddAfter(destinationCup, cupsToMove.Pop());
            }
            return currentCup.Next ?? cups.First;
        }
    }
}