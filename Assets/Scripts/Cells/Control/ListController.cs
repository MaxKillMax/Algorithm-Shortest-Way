using UnityEngine;

public class ListController : MonoBehaviour
{
    [Header("Vars")]
    private GameObject[,] Cells = new GameObject[2, 2];
    private int Length;
    private int Width;

    [Header("Components")]
    private CellController cellcontroller;
    public CellController cellController 
    {
        // The property works only once, otherwise it will issue a warning
        set
        { 
            if (cellcontroller == null) 
            { 
                cellcontroller = value; 
            } 
            else
            {
                Debug.LogWarning("Unnecessary property access");
            } 
        } 
    }

    public void SetInformation(out GameObject[,] Cells)
    {
        Cells = this.Cells;
    }

    public void AddCell(GameObject Cell, (int, int) coordinate)
    {
        Cells[coordinate.Item1, coordinate.Item2] = Cell;
        Cell.GetComponent<Cell>().GetInforamtion(cellcontroller);
    }

    public void NewCellMap(int newLength, int newWidth)
    {
        RemoveOldMap();

        Length = newLength;
        Width = newWidth;
        Cells = new GameObject[Length, Width];
    }

    // Clearing all information about the old grid
    private void RemoveOldMap()
    {
        cellcontroller.RemoveInformation();

        for (int x = Cells.GetLength(0) - 1; x >= 0; x--)
        {
            for (int y = Cells.GetLength(1) - 1; y >= 0; y--)
            {
                Destroy(Cells[x, y]);
            }
        }
    }
}
