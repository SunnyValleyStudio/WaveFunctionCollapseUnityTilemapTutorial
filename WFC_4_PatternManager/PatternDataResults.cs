using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

namespace WaveFunctionCollapse
{
    public class PatternDataResults
    {
        private int[][] patternIndicesGrid;
        public Dictionary<int, PatternData> PatternIndexDictionary { get; private set; }

        public PatternDataResults(int[][] patternIndicesGrid, Dictionary<int, PatternData> patternIndexDictionary)
        {
            this.patternIndicesGrid = patternIndicesGrid;
            PatternIndexDictionary = patternIndexDictionary;
        }

        public int GetGridLengthX()
        {
            return patternIndicesGrid[0].Length;
        }

        public int GetGridLengthY()
        {
            return patternIndicesGrid.Length;
        }

        public int GetIndexAt(int x, int y)
        {
            return patternIndicesGrid[y][x];
        }

        public int GetNeighbourInDirection(int x, int y, Direction dir)
        {
            if (patternIndicesGrid.CheckJaggedArray2IfIndexIsValid(x, y) == false)
            {
                return -1;
            }
            switch (dir)
            {
                case Direction.Up:
                    if (patternIndicesGrid.CheckJaggedArray2IfIndexIsValid(x, y + 1))
                    {
                        return GetIndexAt(x, y + 1);
                    }
                    return -1;
                case Direction.Down:
                    if (patternIndicesGrid.CheckJaggedArray2IfIndexIsValid(x, y - 1))
                    {
                        return GetIndexAt(x, y- 1);
                    }
                    return -1;
                case Direction.Left:
                    if (patternIndicesGrid.CheckJaggedArray2IfIndexIsValid(x-1, y))
                    {
                        return GetIndexAt(x-1, y);
                    }
                    return -1;
                case Direction.Right:
                    if (patternIndicesGrid.CheckJaggedArray2IfIndexIsValid(x+1, y))
                    {
                        return GetIndexAt(x+1, y);
                    }
                    return -1;
                default:
                    return -1;
            }
        }
    }

}
