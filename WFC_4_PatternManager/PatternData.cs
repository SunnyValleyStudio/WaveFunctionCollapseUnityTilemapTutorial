using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaveFunctionCollapse
{
    public class PatternData
    {
        private float _frequency;

        public float FrequencyRelative { get; private set; }
        public Pattern Pattern { get; private set; }
        public float FrequencyRelativeLog2 { get; private set; }

        public PatternData(Pattern pattern)
        {
            Pattern = pattern;
        }

        public void AddToFrequency()
			=> Frequency++;

        public void CalculateRelativeFrequency(int total)
        {
            FrequencyRelative = _frequency / total;
            FrequencyRelativeLog2 = Mathf.Log(FrequencyRelative, 2);
        }

        public bool CompareGrid(Direction dir, PatternData data)
			=> Pattern.ComparePatternToAnotherPattern(dir, data.Pattern);
    }
}
