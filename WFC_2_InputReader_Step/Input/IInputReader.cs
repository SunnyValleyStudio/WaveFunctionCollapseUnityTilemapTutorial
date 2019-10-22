using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaveFunctionCollapse
{
    public interface IInputReader<T>
    {
        IValue<T>[][] ReadInputToGrid();
    }
}


