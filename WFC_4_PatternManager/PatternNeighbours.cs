using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaveFunctionCollapse;

namespace WaveFunctionCollapse
{
    public class PatternNeighbours
    {
        public Dictionary<Direction, HashSet<int>> directionPatternNeighbourDictionary = new Dictionary<Direction, HashSet<int>>();

        public void AddPatternToDictionary(Direction dir, int patternIndex)
        {
            if (directionPatternNeighbourDictionary.ContainsKey(dir))
            {
                directionPatternNeighbourDictionary[dir].Add(patternIndex);
            }
            else
            {
                directionPatternNeighbourDictionary.Add(dir, new HashSet<int>() { patternIndex });
            }
        }

        internal HashSet<int> GetNeighboursInDirection(Direction dir)
        {
            if (directionPatternNeighbourDictionary.ContainsKey(dir))
            {
                return directionPatternNeighbourDictionary[dir];
            }
            return new HashSet<int>();
        }

        public void AddNeighbour(PatternNeighbours neighbours)
        {
            foreach (var item in neighbours.directionPatternNeighbourDictionary)
            {
                if (directionPatternNeighbourDictionary.ContainsKey(item.Key) == false)
                {
                    directionPatternNeighbourDictionary.Add(item.Key, new HashSet<int>());
                }
                directionPatternNeighbourDictionary[item.Key].UnionWith(item.Value);
            }
        }
    }
}

