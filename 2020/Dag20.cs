using System.Collections.Generic;
using System.Linq;


namespace AoC2020
{
    public class Dag20 : IDag
    {
        private string[] input = InputReader.GetInputLines("dag20.txt");
        public string Output => _output;
        private string _output = "";
        List<Tile> tiles = new List<Tile>();
        List<Tile> imageTiles = new List<Tile>();
        public int Star1()
        {
            
            while (tiles.Count < (input.Length +1)/12)
            {
                tiles.Add(new Tile(input.Skip(tiles.Count * 12).Take(11).ToArray()));
            }

            imageTiles.Add(tiles.First());

            while(imageTiles.Count < 9)
            {
                GetAllNext(imageTiles.Last(), tiles);
            }


            long sum = 1;
            _output = sum.ToString();
            return -1;
        }

        public void GetAllNext(Tile tile, List<Tile> tiles)
        {
            var tileOnTop = GetTileOnTop(tile, tiles);
            if(tileOnTop != null)
            {
                tile.NextTo.Add(0, tileOnTop.ID);
                tileOnTop.NextTo.Add(2, tile.ID);
                imageTiles.Add(tileOnTop);
            }
            var tileUnder = GetTileUnder(tile, tiles);
            if(tileUnder != null)
            {
                tile.NextTo.Add(2, tileUnder.ID);
                tileUnder.NextTo.Add(0, tile.ID);
                imageTiles.Add(tileUnder);
            }
            var tileToRigth = GetTileToRigth(tile, tiles);
            if(tileToRigth != null)
            {
                tile.NextTo.Add(1, tileToRigth.ID);
                tileToRigth.NextTo.Add(3, tile.ID);
                imageTiles.Add(tileToRigth);
            }
            var tileToLeft = GetTileToLeft(tile, tiles);
            if(tileToLeft != null)
            {
                tile.NextTo.Add(3, tileToLeft.ID);
                tileToLeft.NextTo.Add(1, tile.ID);
                imageTiles.Add(tileToLeft);
            }
        }

        public Tile GetTileOnTop(Tile tile, List<Tile> tiles)
        {
            foreach (var nextTile in tiles.Where(t => t.ID != tile.ID))
                foreach (var r in nextTile.GetRotations())
                    if(tile.CompareTopToBottomEdge(nextTile))
                        return new Tile(r.Image, r.ID);
            return null;
        }

        public Tile GetTileUnder(Tile tile, List<Tile> tiles)
        {
            foreach (var nextTile in tiles.Where(t => t.ID != tile.ID))
                foreach (var r in nextTile.GetRotations())
                    if(tile.CompareBottomToTopEdge(nextTile))
                        return new Tile(r.Image, r.ID);
            return null;
        }
        public Tile GetTileToRigth(Tile tile, List<Tile> tiles)
        {
            foreach (var nextTile in tiles.Where(t => t.ID != tile.ID))
                foreach (var r in nextTile.GetRotations())
                    if(tile.CompareRightToLeftEdge(nextTile))
                        return new Tile(r.Image, r.ID);
            return null;
        }
        public Tile GetTileToLeft(Tile tile, List<Tile> tiles)
        {
            foreach (var nextTile in tiles.Where(t => t.ID != tile.ID))
                foreach (var r in nextTile.GetRotations())
                    if(tile.CompareLeftToRigthEdge(nextTile))
                        return new Tile(r.Image, r.ID);
            return null;
        }

        public int Star2()
        {
            throw new System.NotImplementedException();
        }
    }
}