using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

namespace WaveFunctionCollapse
{
    public class PatternDataResults
    {
		private int[][] PatternIndicesGrid { get; }
        public Dictionary<int, PatternData> PatternIndexDictionary { get; }

		public PatternDataResults(int[][] patternIndicesGrid, Dictionary<int, PatternData> patternIndexDictionary)
		{
			PatternIndicesGrid = patternIndicesGrid;
			PatternIndexDictionary = patternIndexDictionary;
		}

		public int GetGridLengthX() => PatternIndicesGrid[0].Length;

		public int GetGridLengthY() => PatternIndicesGrid.Length;

		public int GetIndexAt(int x, int y) => PatternIndicesGrid[y][x];

		public int GetNeighbourInDirection(int x, int y, Direction dir)
			=> PatternIndicesGrid.CheckJaggedArray2IfIndexIsValid(x, y) == false
				? -1
				: dir switch
				{
					Up => PatternIndicesGrid.CheckJaggedArray2IfIndexIsValid(x, y + 1) ? GetIndexAt(x, y + 1) : -1,
					Down => PatternIndicesGrid.CheckJaggedArray2IfIndexIsValid(x, y - 1) ? GetIndexAt(x, y - 1) : -1,
					Left => PatternIndicesGrid.CheckJaggedArray2IfIndexIsValid(x - 1, y) ? GetIndexAt(x - 1, y) : -1,
					Right => PatternIndicesGrid.CheckJaggedArray2IfIndexIsValid(x + 1, y) ? GetIndexAt(x + 1, y) : -1,
					_ => -1,
				};
	}
}
