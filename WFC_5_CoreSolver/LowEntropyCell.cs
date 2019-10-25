using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaveFunctionCollapse
{
    public class LowEntropyCell : IComparable<LowEntropyCell>, IEqualityComparer<LowEntropyCell>
    {
        public Vector2Int Position { get; set; }
        public float Entropy { get; set; }
        private float smallEntropyNoise;

        public LowEntropyCell(Vector2Int position, float entropy)
        {
            smallEntropyNoise = UnityEngine.Random.Range(0.001f, 0.005f);
            this.Entropy = entropy+smallEntropyNoise;
            this.Position = position;
        }

        public int CompareTo(LowEntropyCell other)
        {
            if (Entropy > other.Entropy) return 1;
            else if (Entropy < other.Entropy) return -1;
            else return 0;
        }

        public bool Equals(LowEntropyCell cell1, LowEntropyCell cell2)
        {
            return cell1.Position.x == cell2.Position.x && cell1.Position.y == cell2.Position.y;
        }

        public int GetHashCode(LowEntropyCell obj)
        {
            return obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }
    }

}
