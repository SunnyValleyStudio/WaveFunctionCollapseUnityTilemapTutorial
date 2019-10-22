using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using WaveFunctionCollapse;

public class TEst : MonoBehaviour
{
    public Tilemap input;
    // Start is called before the first frame update
    void Start()
    {
        InputReader reader = new InputReader(input);
        var grid = reader.ReadInputToGrid();
        for (int row = 0; row < grid.Length; row++)
        {
            for (int col = 0; col < grid[0].Length; col++)
            {
                Debug.Log("Row: " + row + " Col: " + col + " tile name " + grid[row][col].Value.name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
