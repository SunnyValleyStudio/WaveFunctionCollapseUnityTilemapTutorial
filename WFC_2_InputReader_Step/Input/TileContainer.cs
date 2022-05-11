using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WaveFunctionCollapse
{
    public class TileContainer
    {
		public TileContainer(TileBase tile, int x, int y)
		{
			Tile = tile;
			X = x;
			Y = y;
		}

		public TileBase Tile { get; }
		public int X { get; }
		public int Y { get; }
    }
}

