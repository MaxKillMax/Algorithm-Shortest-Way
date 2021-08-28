using UnityEngine;

public class Cell : MonoBehaviour
{
    [Header ("Components")]
    private CellController cellController;

    public void GetInforamtion(CellController cellController)
    {
        this.cellController = cellController;
    }

    private void OnMouseUpAsButton()
    {
        cellController.SetCell(this.gameObject);
    }
}
