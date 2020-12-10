using System.Collections.Generic;
using System.Linq;
namespace AoC2020
{
    public class Dag7 : IDag
    {
        private string[] Input = InputReader.GetInputLines("dag7.txt");
        public int Star1()
        {
            string yourBag = "shiny gold";
            var rules = Input.Select(t => CreateBag(t)).ToList();

            return -1;
        }

        private Bag CreateBag(string s)
        {
            var bag = new Bag();
            bag.Outside = s.Replace(" bags ", "").Split("contain")[0];
            bag.Contains = new List<string>();
            var containPart = s.Split("contain")[1];
            if(!containPart.Contains("no other bags"))
            {
                var colors = containPart.Split(",");
                foreach (var c in colors)
                {
                    var temp = c.Split(" ");
                    bag.Contains.Add(string.Join(" ", temp[2], temp[3]));
                }
            }
            return bag;

        }


        public int Star2()
        {
            throw new System.NotImplementedException();
        }

        public string Output()
        {
            throw new System.NotImplementedException();
        }

        public class Bag
        {
            public string Outside { get; set; }
            public List<string> Contains { get; set; }
        }
    }
}