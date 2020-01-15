using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using Helpers;

namespace WaveFunctionCollaps
{
    public static class PatternFinder
    {
        public static PatternDataResults GetPatternDataFromGrid<T>(ValuesManager<T> valuesManager, int patternSize, bool equalWeights)
        {
            Dictionary<string, PatternData> patternHashcodeDictionary = new Dictionary<string, PatternData>();
            Dictionary<int, PatternData> patternIndexDictionary = new Dictionary<int, PatternData>();
            Vector2 sizeOfGrid = valuesManager.GetGridSize();
            int patternGridSizeX = 0;
            int patternGridSizeY = 0;
            int rowMin = -1, colMin = -1, rowMax =-1, colMax =-1;
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
                colMin = 1 - patternSize;
                rowMax = (int)sizeOfGrid.y;
                colMax = (int)sizeOfGrid.x;
            }

            int[][] patternIndicesGrid = MyCollectionExtension.CreateJaggedArray<int[][]>(patternGridSizeY, patternGridSizeX);
            int totalFrequency = 0;

            //we loop with offset -1 / +1 to get patterns. At the same time we have to account for patten size.
            //If pattern is of size 2 we will be reaching x+1 and y+1 to check all 4 values. Need visual aid.

            int patternIndex = 0;
            for (int row = rowMin; row < rowMax; row++)
            {
                for (int col = colMin; col < colMax; col++)
                {
                    int[][] gridValues = valuesManager.GetPatternValuesFromGridAt(col, row, patternSize);
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
                            patternIndexDictionary[patternHashcodeDictionary[hashValue].Pattern.Index].AddToFrequency();


                    }
                    //if (patternSize > colMin || row >= rowMin && row < rowMax-1 && col >= colMin && col < colMax-1)
                    //{

                    //    totalFrequency++;

                    //}
                    totalFrequency++;
                    if (patternSize<3)
                        patternIndicesGrid[row + 1][col + 1] = patternHashcodeDictionary[hashValue].Pattern.Index;
                    else
                        patternIndicesGrid[row + patternSize - 1][col + patternSize - 1] = patternHashcodeDictionary[hashValue].Pattern.Index;
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

        public static Dictionary<int, PatternNeighbours> FindPossibleNeighbursForAllPatterns(IFindNeighboutStrategy patternFinder, PatternDataResults patterndataResults)
        {

            return patternFinder.FIndNeighbours(patterndataResults);
        }

        public static PatternNeighbours CheckNeighboursInEachDirection(int x, int y, PatternDataResults patterndataResults)
        {
            PatternNeighbours neighbours = new PatternNeighbours();
            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                int possiblePatternIndex = patterndataResults.GetNeighbourInDirection(x, y, dir);
                if (possiblePatternIndex >= 0)
                {
                    neighbours.AddPatternToDirection(dir, possiblePatternIndex);
                }
            }
            return neighbours;
        }

        public static void AddNeighboursToDictionary(Dictionary<int, PatternNeighbours> dictionary, int patternIndex, PatternNeighbours neighbours)
        {
            if (dictionary.ContainsKey(patternIndex) == false)
            {

                dictionary.Add(patternIndex, neighbours);

            }
            dictionary[patternIndex].AddNeighbours(neighbours);

        }

        private static void AddNewPattern(Dictionary<string, PatternData> patternHashcodeDictionary, Dictionary<int, PatternData> patternIndexDictionary, string hashValue, Pattern pattern)
        {

            PatternData patternData = new PatternData(pattern);
            patternHashcodeDictionary.Add(hashValue, patternData);
            patternIndexDictionary.Add(pattern.Index, patternData);
        }

        public static bool AreArraysTheSame(int[][] arr1, int[][] arr2)
        {
            string arr1hash = HashCodeCalculator.CalculateHashCode(arr1);
            string arr2hash = HashCodeCalculator.CalculateHashCode(arr2);
            return arr1hash.Equals(arr2hash);

        }


    }





}

