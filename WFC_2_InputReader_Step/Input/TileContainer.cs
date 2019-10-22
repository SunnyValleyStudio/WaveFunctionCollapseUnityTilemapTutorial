using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WaveFunctionCollapse
{
    public class TileContainer
    {
        public TileBase Tile { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public TileContainer(TileBase tile, int X, int Y)
        {
            this.Tile = tile;
            this.X = X;
            this.Y = Y;
        }
    }
}

