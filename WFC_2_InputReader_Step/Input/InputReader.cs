using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WaveFunctionCollapse
{
    public class InputReader : IInputReader<TileBase>
    {
		public static IValue<TileBase>[][] ReadInputToGrid(Tilemap tilemap)
		{
			InputImageParameters imageParameters = new(tilemap);
			TileBase[][] grid = MyCollectionExtension.CreateJaggedArray<TileBase[][]>(imageParameters.Tilemap.cellBounds.max.x, imageParameters.Tilemap.cellBounds.max.y);

			for (int row = 0; row < imageParameters.Tilemap.cellBounds.max.x; row++)
			{
				for (int col = 0; col < imageParameters.Tilemap.cellBounds.max.y; col++)
				{
					grid[row][col] = imageParameters.StackOfTiles.Dequeue().Tile;
				}
			}

			TileBaseValue[][] gridOfValues = null;
			if (grid != null)
			{
				gridOfValues = MyCollectionExtension.CreateJaggedArray<TileBaseValue[][]>(grid.Length, grid[0].Length);

				for (int row = 0; row < grid.Length; row++)
				{
					for (int col = 0; col < grid[0].Length; col++)
					{
						gridOfValues[row][col] = new TileBaseValue(grid[row][col]);

					}
				}
			}

			return gridOfValues;
		}
    }
}

