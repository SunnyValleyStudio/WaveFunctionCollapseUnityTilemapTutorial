using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WaveFunctionCollapse
{
    public class PatternManager
    {
        Dictionary<int, PatternData> patternDataIndexDictionary;
        Dictionary<int, PatternNeighbours> patternPossibleNeighboursDictionary;
        int _patternSize = -1;

        public PatternManager(int patternSize)
        {
            _patternSize = patternSize;
        }

        public void ProcessGrid<T>(ValuesManager<T> valueManager, bool equalWeights, string strategyName = null)
        {
            NeighbourStrategyFactory strategyFactory = new();
            IFindNeighbourStrategy strategy = strategyFactory.CreateInstance(strategyName ?? _patternSize.ToString());
            CreatePatterns(valueManager, strategy, equalWeights);
        }

        private void CreatePatterns<T>(ValuesManager<T> valueManager, IFindNeighbourStrategy strategy, bool equalWeights)
        {
            var patternFinderResult = PatternFinder.GetPatternDataFromGrid(valueManager, _patternSize, equalWeights);
            //StringBuilder builder = null;
            //List<string> list = new List<string>();
            //for (int row = 0; row < patternFinderResult.GetGridLengthY(); row++)
            //{
            //    builder = new StringBuilder();
            //    for (int col = 0; col < patternFinderResult.GetGridLengthX(); col++)
            //    {
            //        builder.Append(patternFinderResult.GetIndexAt(col,row) + " ");
            //    }
            //    list.Add(builder.ToString());
            //}
            //list.Reverse();
            //foreach (var item in list)
            //{
            //    Debug.Log(item);
            //}
            patternDataIndexDictionary = patternFinderResult.PatternIndexDictionary;
            GetPatternNeighbours(patternFinderResult, strategy);
        }

        private void GetPatternNeighbours(PatternDataResults patternFinderResult, IFindNeighbourStrategy strategy)
        {
            patternPossibleNeighboursDictionary = PatternFinder.FindPossibleNeighboursForAllPatterns(strategy, patternFinderResult);
        }

        public PatternData GetPatternDataFromIndex(int index)
        {
            return patternDataIndexDictionary[index];
        }

        public HashSet<int> GetPossibleNeighboursForPatternInDirection(int patternIndex, Direction dir)
        {
            return patternPossibleNeighboursDictionary[patternIndex].GetNeighboursInDirection(dir);
        }

        public float GetPatternFrequency(int index)
        {
            return GetPatternDataFromIndex(index).FrequencyRelative;
        }

        public float GetPatternFrequencyLog2(int index)
        {
            return GetPatternDataFromIndex(index).FrequencyRelativeLog2;
        }

        public int GetNuberOfPatterns()
        {
            return patternDataIndexDictionary.Count;
        }
    }

}
