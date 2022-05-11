using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WaveFunctionCollapse
{
    public class ValuesManager<T>
    {
        int[][] _grid;
        Dictionary<int, IValue<T>> valueIndexDictionary = new Dictionary<int, IValue<T>>();
        int index = 0;

        public ValuesManager(IValue<T>[][] gridOfValues)
        {
            CreateGridOfIndices(gridOfValues);
        }

        private void CreateGridOfIndices(IValue<T>[][] gridOfValues)
        {
            _grid = MyCollectionExtension.CreateJaggedArray<int[][]>(gridOfValues.Length, gridOfValues[0].Length);
            for (int row = 0; row < gridOfValues.Length; row++)
            {
                for (int col = 0; col < gridOfValues[0].Length; col++)
                {
                    SetIndexToGridPosition(gridOfValues, row, col);
                }
            }
        }

        private void SetIndexToGridPosition(IValue<T>[][] gridOfValues, int row, int col)
        {
            if (valueIndexDictionary.ContainsValue(gridOfValues[row][col]))
            {
                var key = valueIndexDictionary.FirstOrDefault(x => x.Value.Equals(gridOfValues[row][col]));
                _grid[row][col] = key.Key;
            }
            else
            {
                _grid[row][col] = index;
                valueIndexDictionary.Add(_grid[row][col], gridOfValues[row][col]);
                index++;
            }
        }

        public int GetGridValue(int x, int y)
			=> x < _grid[0].Length && y < _grid.Length && x >= 0 && y >= 0
				? _grid[x][y]
				: throw new IndexOutOfRangeException($"Grid doesn't contain x: {x} y: {y} value");

        public IValue<T> GetValueFromIndex(int index)
			=> valueIndexDictionary.ContainsKey(index)
				? valueIndexDictionary[index]
				: throw new IndexOutOfRangeException($"Index {index} not found in valueDictionary");

        public int GetGridValuesIncludingOffset(int x, int y)
		{
			int xMax = _grid.Length;
			int yMax = _grid[0].Length;
			if ((x %= xMax) < 0) x += xMax;
			if ((y %= yMax) < 0) y += yMax;
			return GetGridValue(x, y);
		}

        public int[][] GetPatternValuesFromGridAt(int x, int y, int patternSize)
        {
            int[][] arrayToReturn = MyCollectionExtension.CreateJaggedArray<int[][]>(patternSize, patternSize);
            for (int row = 0; row < patternSize; row++)
            {
                for (int col = 0; col < patternSize; col++)
                {
                    arrayToReturn[row][col] = GetGridValuesIncludingOffset(x + col, y + row);
                }
            }

            return arrayToReturn;
        }
    }

}
