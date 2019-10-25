using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaveFunctionCollapse{
    public class WFCCore
    {
        OutputGrid outputGrid;
        PatternManager patternManager;

        private int maxIterations = 0;

        public WFCCore(int outputWidth, int outputHeight, int maxIterations, PatternManager patternManage)
        {
            this.outputGrid = new OutputGrid(outputWidth, outputHeight, patternManage.GetNuberOfPatterns());
            this.patternManager = patternManage;
            this.maxIterations = maxIterations;
        }

        public int[][] CreateOputputGrid()
        {
            int iteration = 0;
            while(iteration < this.maxIterations)
            {
                CoreSolver solver = new CoreSolver(this.outputGrid, this.patternManager);
                int innerIteration = 100;
                while(!solver.CheckForConflicts() && !solver.CheckIfSolved())
                {
                    Vector2Int position = solver.GetLowestEntropyCell();
                    solver.CollapseCell(position);
                    solver.Propagate();
                    innerIteration--;
                    if(innerIteration <= 0)
                    {
                        Debug.Log("Propagation is taking too long");
                        return new int[0][];
                    }
                }
                if (solver.CheckForConflicts())
                {
                    Debug.Log("\n Conflict occured. Iteration: " + iteration);
                    iteration++;
                    outputGrid.ResetAllPossibilities();
                    solver = new CoreSolver(this.outputGrid, this.patternManager);
                }
                else
                {
                    Debug.Log("Solved on: " + iteration);
                    this.outputGrid.PrintResultsToConsol();
                    break;
                }
            }
            if(iteration>= this.maxIterations)
            {
                Debug.Log("Coulnd solve the tilemap");
            }
            return outputGrid.GetSolvedOutputGrid();
        }
    }

}
