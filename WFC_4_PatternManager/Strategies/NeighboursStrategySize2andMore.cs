using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaveFunctionCollapse
{
    public class NeighboursStrategySize2andMore : IFindNeighbourStrategy
    {
        public Dictionary<int, PatternNeighbours> FindNeighbours(PatternDataResults patternFinderResult)
        {
            Dictionary<int, PatternNeighbours> result = new Dictionary<int, PatternNeighbours>();
            foreach (var patternDataToCheck in patternFinderResult.PatternIndexDictionary)
            {
                foreach (var possibleNeighbourForPattern in patternFinderResult.PatternIndexDictionary)
                {
                    FindNeighboursInAllDirections(result, patternDataToCheck, possibleNeighbourForPattern);
                }
            }
            return result;
        }

        private void FindNeighboursInAllDirections(Dictionary<int, PatternNeighbours> result, KeyValuePair<int, PatternData> patternDataToCheck, KeyValuePair<int, PatternData> possibleNeighbourForPattern)
        {
            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                if (patternDataToCheck.Value.CompareGrid(dir, possibleNeighbourForPattern.Value)){
                    if (result.ContainsKey(patternDataToCheck.Key) == false)
                    {
                        result.Add(patternDataToCheck.Key, new PatternNeighbours());
                    }
                    result[patternDataToCheck.Key].AddPatternToDictionary(dir, possibleNeighbourForPattern.Key);
                }
            }
        }
    }
}

