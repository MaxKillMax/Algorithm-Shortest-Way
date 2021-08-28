using UnityEngine;

public class Wave : Algorithm
{
    private int StartX = 0;
    private int StartY = 0;

    private int TargetX = 0;
    private int TargetY = 0;

    private int LastX = 0;
    private int LastY = 0;

    public override void FindPath()
    {
        // Preparation
        SetInformation();
        Distance = new int[Cells.GetLength(0), Cells.GetLength(1)];

        // Calls
        if (StartPoint != null && EndPoint != null)
        {
            PlaceObstaclesAndOther(ref TargetX, ref TargetY, ref StartX, ref StartY);
            CheckDistance();
            SetNames();
            SetPath();
            DrawPath();
            PathDrawn = true;
        }
    }

    private void CheckDistance()
    {
        // Preparation
        int CurrentStep = 0;
        bool Stop = false;

        // Launches waves across the map, determining the distance to the end point
        // If it gets to the point, it will end
        while (!Stop)
        {
            int CountOfNew = 0;
            for (int x = 0; x < Distance.GetLength(0); x++)
            {
                for (int y = 0; y < Distance.GetLength(1); y++)
                {
                    if (Distance[x, y] == CurrentStep)
                    {
                        if (y > 0)
                            AddWave(CurrentStep, ref CountOfNew, ref Distance[x, y - 1]);
                        if (x > 0)
                            AddWave(CurrentStep, ref CountOfNew, ref Distance[x - 1, y]);
                        if (y < Distance.GetLength(1) - 1)
                            AddWave(CurrentStep, ref CountOfNew, ref Distance[x, y + 1]);   
                        if (x < Distance.GetLength(0) - 1)
                            AddWave(CurrentStep, ref CountOfNew, ref Distance[x + 1, y]);   
                    }
                }
            }

            CurrentStep++;

            if (Distance[TargetX, TargetY] != -1)
            {
                Stop = true;
            }
            else if (CountOfNew == 0)
            {
                Debug.LogError("Нет прогресса");
                Debug.Break();
            }
        }
    }

    private void SetNames()
    {
        // To make it easier to track, what is their distance from the beginning
        for (int x = 0; x < Distance.GetLength(0); x++)
        {
            for (int y = 0; y < Distance.GetLength(1); y++)
            {
                if (Distance[x, y] == -1)
                    Cells[x, y].transform.name = "Unknown";
                else
                if (Distance[x, y] == -2)
                    Cells[x, y].transform.name = "Obstacle";
                else
                if (Distance[x, y] >= 0)
                    Cells[x, y].transform.name = $"{Distance[x, y]}";
            }
        }
    }

    private void SetPath()
    {
        // Vars
        Path.Add(Cells[TargetX, TargetY]);
        LastX = TargetX;
        LastY = TargetY;

        // Preparation
        int CurrentStep = Distance[TargetX, TargetY] - 1;
        bool Stop = true;

        while (Stop)
        {
            // Vars
            int CountOfNew = 0;
            bool ObjectSelected = false;

            // Ensures that the square of the path is next to the last square of the path,
            // as well as the correct distance
            for (int x = 0; x < Distance.GetLength(0); x++)
            {
                for (int y = 0; y < Distance.GetLength(1); y++)
                {
                    if (Distance[x, y] == CurrentStep && !ObjectSelected)
                    {
                        if (TryAddPath(ref CountOfNew, ref x, ref y)) ObjectSelected = true;
                    }
                }
            }

            CurrentStep--;

            if (Path.Contains(StartPoint))
            {
                Stop = false;
            }
            else if (CountOfNew == 0)
            {
                Debug.LogError("Нет прогресса");
                Debug.Break();
            }
        }
    }

    private void AddWave(int currentStep, ref int countOfNew, ref int cell)
    {
        // Helps the method "Check distance"
        if (cell == -1)
        {
            countOfNew++;
            cell = currentStep + 1;
        }
    }

    private bool TryAddPath(ref int countOfNew, ref int x, ref int y)
    {
        // Helps the method "Set Path"
        bool Chance = false;

        // Many checks
        if ((LastX, LastY) == (x - 1, y) ||
            (LastX, LastY) == (x + 1, y) ||
            (LastX, LastY) == (x, y - 1) ||
            (LastX, LastY) == (x, y + 1))
        {
            if (Distance[x, y] >= 0)
            {
                if (y > 0)
                {
                    Chance = true;
                }
                if (x > 0)
                {
                    Chance = true;
                }
                if (y < Distance.GetLength(1) - 1)
                {
                    Chance = true;
                }
                if (x < Distance.GetLength(0) - 1)
                {
                    Chance = true;
                }
            }
        }

        if (Chance)
        {
            // Adding the path itself
            Path.Add(Cells[x, y]);
            countOfNew++;

            LastX = x;
            LastY = y;

            x = Distance.GetLength(0);
            y = Distance.GetLength(1);

            return true;
        }
        
        return false;
    }
}
