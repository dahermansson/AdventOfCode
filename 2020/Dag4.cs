using System.Linq;

using System.Collections.Generic;
using System.Drawing;
using AoC.Utils;

namespace AoC2020
{
    public class Dag4 : IDay
    {
        private static string Input = InputReader.GetInput("dag4.txt");
        public int Star1() => Input.Split(Utils.DNL).Count(p => Valid(p));

        private bool Valid(string passport)
        {
            return passport.Contains("byr") && 
            passport.Contains("iyr") &&
            passport.Contains("eyr") &&
            passport.Contains("hgt") &&
            passport.Contains("hcl") &&
            passport.Contains("ecl") &&
            passport.Contains("pid");  
        }

        public int Star2() => Input.Split(Utils.DNL).Where(p => Valid(p)).Count(t => new Passport(t).IsValid());

        public string Output => throw new System.NotImplementedException();
    }

    public class Passport
        {
            public Passport(string p)
            {
                var temp = p.Replace(Utils.NL, " ");
                foreach (var s in temp.Split(" "))
                {
                    if (s.StartsWith("byr"))
                        byr = s.Remove(0, 4);
                    if (s.StartsWith("iyr"))
                        iyr = s.Remove(0, 4);
                    if (s.StartsWith("eyr"))
                        eyr = s.Remove(0, 4);
                    if (s.StartsWith("hgt"))
                        hgt = s.Remove(0, 4);
                    if (s.StartsWith("hcl"))
                        hcl = s.Remove(0, 4);
                    if (s.StartsWith("ecl"))
                        ecl = s.Remove(0, 4);
                    if (s.StartsWith("pid"))
                        pid = s.Remove(0, 4);
                }
            }
            public string byr { get; set; }
            public string iyr { get; set; }
            public string eyr { get; set; }
            public string hgt { get; set; }
            public string hcl { get; set; } 
            public string ecl { get; set; }
            public string pid { get; set; }

            public bool IsValid()
            {
                if(!int.TryParse(byr, out int iByr))
                    return false;
                if(iByr > 2002 || iByr < 1920)
                    return false;
                if(!int.TryParse(iyr, out int iIyr))
                    return false;
                if(iIyr > 2020 || iIyr < 2010)
                    return false;
                if(!int.TryParse(eyr, out int iEyr))
                    return false;
                if(iEyr > 2030 || iEyr < 2020)
                    return false;
                if(hgt.EndsWith("cm"))
                {
                    if(!int.TryParse(hgt.Replace("cm", ""), out int iHgt))
                        return false;
                    if(iHgt < 150 || iHgt > 193)
                        return false;
                }
                else if(hgt.EndsWith("in"))
                {
                    if(!int.TryParse(hgt.Replace("in", ""), out int iHgt))
                        return false;
                    if(iHgt < 59 || iHgt > 76)
                        return false;
                }
                else
                {
                    return false;
                }
                if(hcl[0] != '#')
                    return false;
                var hclTemp = hcl.Remove(0,1);
                if(hclTemp.Length != 6)
                    return false;

                string[] hclOki = new string[] {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f"}; 

                foreach (string s in hclTemp.ToCharArray().Select(t => t.ToString()))
                {
                    if(!hclOki.Contains(s))
                        return false;    
                }

                string[] eye = {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
                if(!eye.Contains(ecl))
                    return false;

                if(pid.Length != 9)
                    return false;

                string[] pidOki = Enumerable.Range(0, 10).Select(i => i.ToString()).ToArray();

                foreach (string s in pid.ToCharArray().Select(t => t.ToString()))
                {
                    if(!pidOki.Contains(s))
                        return false;                    
                }

                return true;
            } 
        }

}