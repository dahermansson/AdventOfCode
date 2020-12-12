using System;
using System.Linq;
namespace AoC2020
{
    public class Dag2 : IDag
    {
        private static string[] InputLines = InputReader.GetInputLines("dag2.txt");
        public int Star1() => InputLines.Select(t => new PolicyPassword(t.Split(' '))).Count(t => t.IsValidToSledRentalPlaceDownTheStreet());

        public int Star2() => InputLines.Select(t => new PolicyPassword(t.Split(' '))).Count(t => t.IsValidToOfficialTobogganCorporate());

        private class PolicyPassword
        {
            public PolicyPassword(string[] linepParts)
            {
                Least = int.Parse(linepParts[0].Split('-')[0]);
                Most = int.Parse(linepParts[0].Split('-')[1]);
                Char = linepParts[1][0];
                Password = linepParts[2];
            }

            public int Least { get; private set; }
            public int Most { get; private set; }
            public char Char { get; private set; }
            public string Password { get; private set; }

            public bool IsValidToSledRentalPlaceDownTheStreet()
            {
                return Enumerable.Range(Least, Most - Least +1).Contains(Password.Count(c => c == Char));
            }

            public bool IsValidToOfficialTobogganCorporate()
            {
                return (Password[Least-1] == Char && Password[Most-1] != Char) ||
                    (Password[Least-1] != Char && Password[Most-1] == Char);
            }
            
        }
        public string Output => throw new System.NotImplementedException();
    }
}