using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaveFunctionCollapse
{

    public class NeighboursStrategySize1Default : IFindNeighbourStrategy
    {
        public Dictionary<int, PatternNeighbours> FindNeighbours(PatternDataResults patternFinderResult)
        {
            Dictionary<int, PatternNeighbours> result = new Dictionary<int, PatternNeighbours>();
            FindNeighboursForEachPattern(patternFinderResult, result);
            return result;
        }

        private void FindNeighboursForEachPattern(PatternDataResults patternFinderResult, Dictionary<int, PatternNeighbours> result)
        {
            for (int row = 0; row < patternFinderResult.GetGridLengthY(); row++)
            {
                for (int col = 0; col < patternFinderResult.GetGridLengthX(); col++)
                {
                    PatternNeighbours neighbours = PatternFinder.CheckNeighboursInEachDirection(col, row, patternFinderResult);
                    PatternFinder.AddNeighboursToDictionary(result, patternFinderResult.GetIndexAt(col, row), neighbours);
                }
            }
        }
    }

}
