using System.Collections.Generic;
using UnityEngine;

public class AStar : Algorithm
{
    private float StartDistance;
    private int StartX = new int();
    private int StartY = new int();

    private int TargetX = new int();
    private int TargetY = new int();

    int CurrentX = new int();
    int CurrentY = new int();

    public override void FindPath()
    {
        // Preparation
        SetInformation();
        Distance = new int[Cells.GetLength(0), Cells.GetLength(1)];
        StartDistance = (EndPoint.transform.position - StartPoint.transform.position).magnitude;

        // Calls
        if (StartPoint != null && EndPoint != null)
        {
            PlaceObstaclesAndOther(ref TargetX, ref TargetY, ref StartX, ref StartY);
            SearchPath(StartDistance);
            DrawPath();
            PathDrawn = true;
        }
    }

    private void SearchPath(float startDistance)
    {
        // The algorithm works, but only in some situations
        // A WARNING! An endless loop could happen
        // -------------------------------------------------------------------------------------

        // Vars
        float CurrentDistance = StartDistance;
        CurrentX = StartX;
        CurrentY = StartY;

        // The black sheet helps this algorithm "learn". Where he once stopped, he won't go
        List<GameObject> BlackList = new List<GameObject>();
        // Real-time path drawing
        List<GameObject> CurrentPath = new List<GameObject>();
        CurrentPath.Add(StartPoint);

        // Preparation
        bool Stop = false;

        while (!Stop)
        {
            // Vars
            float MagnitudePower = CurrentDistance;
            bool Activated = false;
            int Index = 0;

            CreateNeighbors(out int[,] Neighbors, out bool[] Skips);

            for (int i = 0; i < Neighbors.GetLength(0); i++)
            {
                if (Skips[i])
                {
                    continue;
                }
                else if (!BlackList.Contains(Cells[Neighbors[i, 0], Neighbors[i, 1]]) &&
                        !CurrentPath.Contains(Cells[Neighbors[i, 0], Neighbors[i, 1]]) &&
                         Distance[Neighbors[i, 0], Neighbors[i, 1]] >= -1)
                {
                    // If the cell matches the conditions, then the algorithm will go there. However,
                    // if there are ones closer to the final target, then the algorithm will make a way there.
                    if ((Cells[Neighbors[i, 0], Neighbors[i, 1]].transform.position - EndPoint.transform.position).magnitude < MagnitudePower)
                    {
                        MagnitudePower = (Cells[Neighbors[i, 0], Neighbors[i, 1]].transform.position - EndPoint.transform.position).magnitude;
                        Activated = true;
                        Index = i;
                    }
                }
            }


            CurrentDistance = (Cells[Neighbors[Index, 0], Neighbors[Index, 1]].transform.position - EndPoint.transform.position).magnitude;

            // Below ñhecking conditions
            if (Activated)
            {
                CurrentX = Neighbors[Index, 0];
                CurrentY = Neighbors[Index, 1];
                CurrentPath.Add(Cells[CurrentX, CurrentY]);
            }

            if (CurrentX == TargetX && CurrentY == TargetY)
            {
                Stop = true;
            }

            if (!Activated && (CurrentX != TargetX || CurrentY != TargetY))
            {
                CurrentX = StartX;
                CurrentY = StartY;
                CurrentPath = new List<GameObject>();
                CurrentPath.Add(StartPoint);

                BlackList.Add(Cells[Neighbors[Index, 0], Neighbors[Index, 1]]);
                CurrentDistance = StartDistance;
            }
        }

        Path = CurrentPath;
    }

    private void CreateNeighbors(out int[,] neighbors, out bool[] skips)
    {
        // Helps the method "Search Path"

        // Left, Up, Right, Down
        neighbors = new int[4, 2]
        {
                { CurrentX - 1, CurrentY },
                { CurrentX, CurrentY + 1 },
                { CurrentX + 1, CurrentY },
                { CurrentX, CurrentY - 1 }
        };

        // Turns off a neighbor if it goes out of the array
        skips = new bool[4]
        {
                false,
                false,
                false,
                false
        };

        if (CurrentX - 1 < 0) skips[0] = true;
        if (CurrentY + 1 >= Distance.GetLength(1)) skips[1] = true;
        if (CurrentX + 1 >= Distance.GetLength(0)) skips[2] = true;
        if (CurrentY - 1 < 0) skips[3] = true;
    }
}
