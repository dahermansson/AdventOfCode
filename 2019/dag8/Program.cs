using System;
using System.IO;
using System.Linq;
using System.Drawing;
using AdventofCode.Utils;

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
            int width = 25;
            int height = 6;
            var input = File.ReadAllText("input.txt").ToIntArray();
            var layerSize = width*height;
            var layers = Utils.Split(input, layerSize).Reverse();

            int[,] image = new int[height,width];
            var bitmap = new Bitmap(width, height);
            foreach (var layer in layers)
            {
                var rows = Utils.Split(layer, width).ToArray();
                for (int h = 0; h < rows.Length; h++)
                    for (int w = 0; w < rows[h].Length; w++)
                    {
                        var layerPixel = rows[h][w];
                        if(layerPixel == 2)
                            continue;
                        image[h,w] = layerPixel;
                    }
            }
            
            for (int h = 0; h < height; h++)
                for (int w = 0; w < width; w++)
                    bitmap.SetPixel(w, h, image[h,w] == 0 ? Color.Black : Color.White);
            bitmap.Save("dag8Star2.png");
        }

        private static void Star1()
        {
            var input = File.ReadAllText("input.txt").ToIntArray();
            var layerSize = 25*6;
            var layers = Utils.Split(input, layerSize);
            var fewestZeroesLayer = layers.GroupBy(t => t.Count(c => c == 0)).OrderBy(t => t.Key).First();
            Console.WriteLine($"Star 1: {fewestZeroesLayer.ElementAt(0).Count(t => t == 1) * fewestZeroesLayer.ElementAt(0).Count(t => t == 2)}");
        }
    }
}
