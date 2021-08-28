using UnityEngine;

public class CellGenerator : MonoBehaviour
{
    [Header ("Vars")]
    [SerializeField] private int Length;
    [SerializeField] private int Width;

    [Header("Components")]
    private ListController listController;

    [Header("Other")]
    [SerializeField] private GameObject CellPrefab;

    private void Awake()
    {
        listController = GetComponent<ListController>();
        listController.cellController = GetComponent<CellController>();
    }

    public void BuildCells()
    {
        // A smaller map doesn't make sense
        if (Length < 2)
        {
            Length = 2;
        }

        GameObject NewCell;
        listController.NewCellMap(Length, Width);
        for (int x = 0; x < Length; x++)
        {
            for (int y = 0; y < Width; y++)
            {
                NewCell = Instantiate(CellPrefab);
                NewCell.transform.position = new Vector3(x - Length / 2, y - Width / 2, 0);
                NewCell.transform.SetParent(transform);

                listController.AddCell(NewCell, (x, y));
            }
        }
    }
}
