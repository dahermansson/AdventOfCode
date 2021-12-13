using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace AoC.Utils
{
    public static class ImageHandler
    {
        public static void CreatImageFromMatrix<T>(Matrix<T> matrix, string fileName, T colorValue)
        {
            using(var image = new Image<Rgba32>(matrix.Columns, matrix.Rows)) 
            {
                foreach (var pos in matrix.GetAllPositions())
                    image[pos.Column, pos.Row] = pos.Value != null && pos.Value.Equals(colorValue) ? Rgba32.ParseHex("FFFFFF"): Rgba32.ParseHex("000000");
                image.SaveAsJpeg(fileName);
            } 
        }
    }
}