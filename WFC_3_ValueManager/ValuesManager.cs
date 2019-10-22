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
        {
            if (x >= _grid[0].Length || y >= _grid.Length || x < 0 || y < 0)
            {
                throw new System.IndexOutOfRangeException("Grid doeant contain x: " + x + " y: " + y + " value");
            }
            return _grid[y][x];
        }

        public IValue<T> GetValueFromIndex(int index)
        {
            if (valueIndexDictionary.ContainsKey(index))
            {
                return valueIndexDictionary[index];
            }
            throw new System.Exception("No index " + index + " in valueDictionary");
        }

        public int GetGridValuesIncludingOffset(int x, int y)
        {
            int yMax = _grid.Length;
            int xMax = _grid[0].Length;
            if(x<0 && y < 0)
            {
                return GetGridValue(xMax + x, yMax + y);
            }
            if(x<0 && y >= yMax)
            {
                return GetGridValue(xMax + x, y - yMax);
            }
            if(x>=xMax && y < 0)
            {
                return GetGridValue(x - xMax, yMax + y);
            }
            if(x >=xMax && y >= yMax)
            {
                return GetGridValue(x - xMax, y - yMax);
            }

            if (x < 0)
            {
                return GetGridValue(xMax + x, y);
            }
            if (x >= xMax)
            {
                return GetGridValue(x - xMax, y);
            }
            if (y < 0)
            {
                return GetGridValue(x, yMax + y);
            }

            if (y >= yMax)
            {
                return GetGridValue(x, y - yMax);
            }
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
