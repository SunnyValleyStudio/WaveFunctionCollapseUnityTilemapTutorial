using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WaveFunctionCollapse
{
    public class TileBaseValue : IValue<TileBase>
    {
        public TileBaseValue(TileBase tileBase)
        {
            Value = tileBase;
        }

        public TileBase Value { get; }

        public bool Equals(IValue<TileBase> x, IValue<TileBase> y)
			=> x == y;

        public bool Equals(IValue<TileBase> other)
			=> other.Value == Value;

        public int GetHashCode(IValue<TileBase> obj)
			=> obj.GetHashCode();

        public override int GetHashCode()
			=> Value.GetHashCode();
    }

}
