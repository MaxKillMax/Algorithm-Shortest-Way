using System.Collections.Generic;
using UnityEngine;

public class Algorithm : MonoBehaviour
{
    [Header("Vars")]
    public bool PathDrawn = false;

    protected List<GameObject> Path = new List<GameObject>();
    protected GameObject[,] Cells;
    protected int[,] Distance;

    protected List<GameObject> Obstacles;
    protected GameObject StartPoint;
    protected GameObject EndPoint;

    [Header("Components")]
    [SerializeField] protected CellController cellController;
    [SerializeField] protected ListController listController;

    public virtual void FindPath()
    {
        Debug.LogError("Unnecessary call to basic algorithm creation");
    }

    protected void PlaceObstaclesAndOther(ref int targetX, ref int targetY, ref int startX, ref int startY)
    {
        for (int x = 0; x < Cells.GetLength(0); x++)
        {
            for (int y = 0; y < Cells.GetLength(1); y++)
            {
                if (Obstacles.Contains(Cells[x, y]))
                {
                    // -2 is an obstacle
                    Distance[x, y] = -2;
                }
                else if (Cells[x, y] == StartPoint)
                {
                    // 0 is start
                    Distance[x, y] = 0;
                    startX = x;
                    startY = y;
                }
                else if (Cells[x, y] == EndPoint)
                {
                    // -1 is end
                    Distance[x, y] = -1;
                    targetX = x;
                    targetY = y;
                }
                else
                {
                    // -1 is other
                    Distance[x, y] = -1;
                }
            }
        }
    }

    protected void DrawPath()
    {
        for (int i = 0; i < Path.Count; i++)
        {
            if (i == 0 || i == Path.Count - 1)
            {
                Path[i].GetComponent<SpriteRenderer>().color = Color.blue;
            }
            else
            {
                Path[i].GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
    }

    protected void SetInformation()
    {
        cellController.SetInformation(out GameObject StartPoint2, out GameObject EndPoint2, out List<GameObject> Obstacles2);
        listController.SetInformation(out GameObject[,] Cells2);
        Path = new List<GameObject>();

        StartPoint = StartPoint2;
        EndPoint = EndPoint2;
        Obstacles = Obstacles2;
        Cells = Cells2;
    }
}
