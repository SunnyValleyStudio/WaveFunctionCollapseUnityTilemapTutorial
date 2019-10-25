using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WaveFunctionCollapse
{
    public class OutputGrid
    {
        Dictionary<int, HashSet<int>> indexPossiblePatternDictionary = new Dictionary<int, HashSet<int>>();
        public int width { get; }
        public int height { get; }
        private int maxNumberOfPatterns = 0;

        public OutputGrid(int width, int height, int numberOfPatterns)
        {
            this.width = width;
            this.height = height;
            this.maxNumberOfPatterns = numberOfPatterns;
            ResetAllPossibilities();
        }

        public void ResetAllPossibilities()
        {
            HashSet<int> allPossiblePatternList = new HashSet<int>();
            allPossiblePatternList.UnionWith(Enumerable.Range(0, this.maxNumberOfPatterns).ToList());

            indexPossiblePatternDictionary.Clear();
            for (int i = 0; i < height * width; i++)
            {
                indexPossiblePatternDictionary.Add(i, new HashSet<int>(allPossiblePatternList));
            }

        }

        public bool CheckCellExists(Vector2Int position)
        {
            int index = GetIndexFromCoordinates(position);
            return indexPossiblePatternDictionary.ContainsKey(index);
        }

        private int GetIndexFromCoordinates(Vector2Int position)
        {
            return position.x + width * position.y;
        }

        public bool CheckIfCellIsCollapsed(Vector2Int position)
        {
            return GetPossibleValueForPossition(position).Count <= 1;
        }

        public HashSet<int> GetPossibleValueForPossition(Vector2Int position)
        {
            int index = GetIndexFromCoordinates(position);
            if (indexPossiblePatternDictionary.ContainsKey(index))
            {
                return indexPossiblePatternDictionary[index];
            }
            return new HashSet<int>();
        }

        internal void PrintResultsToConsol()
        {
            StringBuilder builder = null;
            List<string> list = new List<string>();
            for (int row = 0; row < this.height; row++)
            {
                builder = new StringBuilder();
                for (int col = 0; col < this.width; col++)
                {
                    var result = GetPossibleValueForPossition(new Vector2Int(col, row));
                    if (result.Count == 1)
                    {
                        builder.Append(result.First() + " ");
                    }
                    else
                    {
                        string newString = "";
                        foreach (var item in result)
                        {
                            newString += item + ",";
                        }
                        builder.Append(newString);
                    }
                }
                list.Add(builder.ToString());
            }
            list.Reverse();
            foreach (var item in list)
            {
                Debug.Log(item);
            }
            Debug.Log("---");
        }

        public bool CheckIfGridIsSolved()
        {
            return !indexPossiblePatternDictionary.Any(x => x.Value.Count > 1);
        }

        internal bool CheckIfValidPosition(Vector2Int position)
        {
            return MyCollectionExtension.ValidateCoordinates(position.x, position.y, width, height);
        }

        public Vector2Int GetRandomCell()
        {
            int randmIndex = UnityEngine.Random.Range(0, indexPossiblePatternDictionary.Count);
            return GetCoordsFromIndex(randmIndex);
        }

        private Vector2Int GetCoordsFromIndex(int randmIndex)
        {
            Vector2Int coords = Vector2Int.zero;
            coords.x = randmIndex / this.width;
            coords.y = randmIndex % this.height;
            return coords;
        }

        public void SetPatternOnPosition(int x, int y, int patternIndex)
        {
            int index = GetIndexFromCoordinates(new Vector2Int(x, y));
            indexPossiblePatternDictionary[index] = new HashSet<int>() { patternIndex };
        }

        public int[][] GetSolvedOutputGrid()
        {
            int[][] returnGrid = MyCollectionExtension.CreateJaggedArray<int[][]>(this.height, this.width);
            if (CheckIfGridIsSolved() == false)
            {
                return MyCollectionExtension.CreateJaggedArray<int[][]>(0, 0);
            }
            for (int row = 0; row < this.height; row++)
            {
                for (int col = 0; col < this.width; col++)
                {
                    int index = GetIndexFromCoordinates(new Vector2Int(col, row));
                    returnGrid[row][col] = indexPossiblePatternDictionary[index].First();
                }
            }
            return returnGrid;
        }
    }

}
