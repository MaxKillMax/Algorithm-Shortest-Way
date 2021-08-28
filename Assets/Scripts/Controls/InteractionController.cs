using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [Header("Components")]
    private Componenter componenter;

    private void Awake()
    {
        componenter = GetComponent<Componenter>();
    }

    void Update()
    {
        // Key B - Build
        if (Input.GetKeyUp(KeyCode.B))
        {
            // Cleans and builds a new grid
            componenter.cellGenerator.BuildCells();
        }

        // Key W - Wave
        if (Input.GetKeyUp(KeyCode.W))
        {
            // Wave algorithm starts
            componenter.wave.FindPath();
        }

        // Key S - Star
        if (Input.GetKeyUp(KeyCode.S))
        {
            // AStar algorithm starts
            componenter.astar.FindPath();
        }
    }
}
