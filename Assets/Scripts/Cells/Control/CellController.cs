using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    [Header ("Points on grid")]
    private GameObject StartPoint;
    private GameObject EndPoint;
    private List<GameObject> Obstacles = new List<GameObject>();

    // Clicking on the cell checks if the start and finish are free,
    // and if not, puts an obstacle (if there is an obstacle, removes it)
    public void SetCell(GameObject cell)
    {
        if (StartPoint == null)
        {
            StartPoint = cell;
            StartPoint.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else if (EndPoint == null)
        {
            EndPoint = cell;
            EndPoint.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            if (cell != StartPoint && cell != EndPoint)
            {
                if (!Obstacles.Contains(cell))
                {
                    Obstacles.Add(cell);
                    Obstacles[Obstacles.Count - 1].GetComponent<SpriteRenderer>().color = Color.black;
                }
                else
                {
                    cell.GetComponent<SpriteRenderer>().color = Color.white;
                    Obstacles.Remove(cell);
                }
            }
        }
    }

    public void SetInformation(out GameObject startPoint, out GameObject endPoint, out List<GameObject> obstacles)
    {
        startPoint = StartPoint;
        endPoint = EndPoint;
        obstacles = Obstacles;
    }

    public void RemoveInformation()
    {
        if (StartPoint != null)
        {
            StartPoint.GetComponent<SpriteRenderer>().color = Color.white;
            StartPoint = null;
        }

        if (EndPoint != null)
        {
            EndPoint.GetComponent<SpriteRenderer>().color = Color.white;
            EndPoint = null;
        }

        if (Obstacles.Count > 0)
        {
            for (int i = Obstacles.Count - 1; i >= 0; i--)
            {
                Obstacles[i].GetComponent<SpriteRenderer>().color = Color.white;
                Obstacles.RemoveAt(i);
            }
        }
    }
}
