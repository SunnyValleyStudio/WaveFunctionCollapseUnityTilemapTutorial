using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaveFunctionCollapse;

namespace WaveFunctionCollapse
{
    public static class PatternFinder
    {
        
        internal static PatternDataResults GetPatternDataFromGrid<T>(ValuesManager<T> valueManager, int patternSize, bool equalWeights)
        {
            Dictionary<string, PatternData> patternHashcodeDictionary = new Dictionary<string, PatternData>();
            Dictionary<int, PatternData> patternIndexDictionary = new Dictionary<int, PatternData>();
            Vector2 sizeOfGrid = valueManager.GetGridSize();
            int patternGridSizeX = 0, patternGridSizeY = 0;
            int rowMin = -1, colMin = -1, rowMax = -1, colMax = -1;
            if (patternSize < 3)
            {
                patternGridSizeX = (int)sizeOfGrid.x + 3 - patternSize;
                patternGridSizeY = (int)sizeOfGrid.y + 3 - patternSize;
                rowMax = patternGridSizeY - 1;
                colMax = patternGridSizeX - 1;
            }
            else
            {
                patternGridSizeX = (int)sizeOfGrid.x + patternSize - 1;
                patternGridSizeY = (int)sizeOfGrid.y + patternSize - 1;
                rowMin = 1 - patternSize;
                colMax = 1 - patternSize;
                rowMax = (int)sizeOfGrid.y;
                colMax = (int)sizeOfGrid.x;
            }

            int[][] patternIndicesGrid = MyCollectionExtension.CreateJaggedArray<int[][]>(patternGridSizeY, patternGridSizeX);
            int totalFrequency = 0, patternIndex = 0;
            for (int row = rowMin; row < rowMax; row++)
            {
                for (int col = colMin; col < colMax; col++)
                {
                    int[][] gridValues = valueManager.GetPatternValuesFromGridAt(col, row, patternSize);
                    string hashValue = HashCodeCalculator.CalculateHashCode(gridValues);

                    if (patternHashcodeDictionary.ContainsKey(hashValue) == false)
                    {
                        Pattern pattern = new Pattern(gridValues, hashValue, patternIndex);
                        patternIndex++;
                        AddNewPattern(patternHashcodeDictionary, patternIndexDictionary, hashValue, pattern);
                    }
                    else
                    {
                        if (equalWeights == false)
                        {
                            patternIndexDictionary[patternHashcodeDictionary[hashValue].Pattern.Index].AddToFrequency();
                        }
                    }
                    totalFrequency++;
                    if (patternSize < 3)
                    {
                        patternIndicesGrid[row + 1][col + 1] = patternHashcodeDictionary[hashValue].Pattern.Index;
                    }
                    else
                    {
                        patternIndicesGrid[row+patternSize-1][col+patternSize-1] = patternHashcodeDictionary[hashValue].Pattern.Index;
                    }
                }
                
            }

            CalculateRelativeFrequency(patternIndexDictionary, totalFrequency);
            return new PatternDataResults(patternIndicesGrid, patternIndexDictionary);

        }

        private static void CalculateRelativeFrequency(Dictionary<int, PatternData> patternIndexDictionary, int totalFrequency)
        {
            foreach (var item in patternIndexDictionary.Values)
            {
                item.CalculateRelativeFrequency(totalFrequency);
            }
        }

        private static void AddNewPattern(Dictionary<string, PatternData> patternHashcodeDictionary, Dictionary<int, PatternData> patternIndexDictionary, string hashValue, Pattern pattern)
        {
            PatternData data = new PatternData(pattern);
            patternHashcodeDictionary.Add(hashValue, data);
            patternIndexDictionary.Add(pattern.Index, data);
        }

        internal static Dictionary<int, PatternNeighbours> FindPossibleNeighboursForAllPatterns(IFindNeighbourStrategy strategy, PatternDataResults patternFinderResult)
        {
            return strategy.FindNeighbours(patternFinderResult);
        }

        public static PatternNeighbours CheckNeighboursInEachDirection(int x, int y, PatternDataResults patternDataResults)
        {
            PatternNeighbours patternNeighbours = new PatternNeighbours();
            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                int possiblePatternIndex = patternDataResults.GetNeighbourInDirection(x, y, dir);
                if(possiblePatternIndex >= 0)
                {
                    patternNeighbours.AddPatternToDictionary(dir, possiblePatternIndex);
                }
            }
            return patternNeighbours;

        }

        public static void AddNeighboursToDictionary(Dictionary<int, PatternNeighbours> dictionary, int patternIndex, PatternNeighbours neighbours)
        {
            if (dictionary.ContainsKey(patternIndex) == false)
            {
                dictionary.Add(patternIndex, neighbours);
            }
            dictionary[patternIndex].AddNeighbour(neighbours);
        }


    }
}

