using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;

namespace dag8
{
    class Program
    {
        static void Main(string[] args)
        {
            Star1();
            Star2();
        }
        private static void Star2()
        {
            var input = File.ReadAllText("input.txt");
            var layerSize = 25*6;
            var layers = Enumerable.Range(0, input.Length/layerSize)
            .Select(s => input.Substring(s * layerSize, layerSize).Select(t => (int)char.GetNumericValue(t))).Reverse().ToList();

            int[,] image = new int[6,25];
            var bitmap = new Bitmap(26, 6);
            foreach (var layer in layers)
            {
                var rows = new List<int[]>();
                for (int i = 0; i < 6; i++)
                    rows.Add(layer.Skip(i*25).Take(25).ToArray());
                
                for (int r = 0; r < rows.Count; r++)
                    for (int c = 0; c < rows[r].Length; c++)
                    {
                        var layerPixel = rows[r][c];
                        if(layerPixel == 2)
                            continue;
                        image[r,c] = layerPixel;
                    }
            }
            
            for (int r = 0; r < 6; r++)
                for (int c = 0; c < 25; c++)
                    bitmap.SetPixel(c, r, image[r,c] == 0 ? Color.Black : Color.White);
            bitmap.Save("dag8Star2.png");
        }

        private static void Star1()
        {
            var input = File.ReadAllText("input.txt");
            var layerSize = 25*6;
            var layers = Enumerable.Range(0, input.Length/layerSize)
            .Select(s => input.Substring(s * layerSize, layerSize).Select(t => char.GetNumericValue(t)));
            var fewestZeroesLayer = layers.GroupBy(t => t.Count(c => c == 0)).OrderBy(t => t.Key).First();
            Console.WriteLine($"Star 1: {fewestZeroesLayer.ElementAt(0).Count(t => t == 1) * fewestZeroesLayer.ElementAt(0).Count(t => t == 2)}");
        }
    }
}
