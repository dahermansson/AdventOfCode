using System;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace AoC2020
{
    public class Tile
    {
        public int ID { get; set; }
        public Image<Rgba32> Image { get; set; }
        public int Test { get; set; }
        public Dictionary<int,int> NextTo { get; set; }
        public Tile(Image<Rgba32> image, int id)
        {
            ID = id;
            Image = image;
            NextTo = new Dictionary<int, int>();
        }

        public Tile(string[] input)
        {
            ID = int.Parse(input[0].Split(" ")[1].Replace(":", ""));
            Image = new Image<Rgba32>(input[1].Length, input.Length -1);
            for (int i = 0; i < Image.Height; i++)
                for (int r = 0; r < Image.Width; r++)
                    Image.GetPixelRowSpan(i)[r] = input[i+1][r] == '#' ? Color.Red : Color.White;
            NextTo = new Dictionary<int, int>();
        }

        public IEnumerable<Tile> GetRotations()
        {
            for (int i = 1; i < 4; i++)
            {
                Image.Mutate(t => t.Rotate(i * 90));
                yield return new Tile(Image, this.ID); 
                
            }
            Image.Mutate(t => t.Rotate(90));
            Image.Mutate(t => t.Flip(FlipMode.Horizontal));
            yield return new Tile(Image, this.ID);

            Image.Mutate(t => t.Flip(FlipMode.Horizontal));
            Image.Mutate(t => t.Flip(FlipMode.Vertical));
            yield return new Tile(Image, this.ID);
        }

        public bool CompareRightToLeftEdge(Tile right)
        {
            var thisRigthEdge = this.GetEdge(false);
            var rightLeftEdge = right.GetEdge(true);

            for (int i = 0; i < thisRigthEdge.Length; i++)
                if(thisRigthEdge[i] != rightLeftEdge[i])
                    return false;
            return true;
        }

        public bool CompareLeftToRigthEdge(Tile left)
        {
            var thisLeftEdge = this.GetEdge(true);
            var leftRigthEdge = left.GetEdge(false);

            for (int i = 0; i < thisLeftEdge.Length; i++)
                if(thisLeftEdge[i] != leftRigthEdge[i])
                    return false;
            return true;
        }

        public bool CompareBottomToTopEdge(Tile under)
        {
            var thisBottomEdge = this.GetTopBottomEdge(false);
            var underTopEdge = under.GetTopBottomEdge(true);

            for (int i = 0; i < thisBottomEdge.Length; i++)
                if(thisBottomEdge[i] != underTopEdge[i])
                    return false;
            return true;
        }

        public bool CompareTopToBottomEdge(Tile top)
        {
            var thisTopEdge = this.GetTopBottomEdge(true);
            var topBottomEdge = top.GetTopBottomEdge(false);

            for (int i = 0; i < thisTopEdge.Length; i++)
                if(thisTopEdge[i] != topBottomEdge[i])
                    return false;
            return true;
        }

        private Rgba32[] GetEdge(bool left)
        {
            var res = new Rgba32[Image.Height];
            var column = left ? 0 : Image.Width -1;
            for (int i = 0; i < Image.Height; i++)
            {
                res[i] = Image[column, i];
            }
            return res;
        }
        private Rgba32[] GetTopBottomEdge(bool top)
        {
            var row = top ? 0 : Image.Width -1;
            return Image.GetPixelRowSpan(row).ToArray();
        }
    }
}