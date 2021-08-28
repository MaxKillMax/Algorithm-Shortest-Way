using UnityEngine;

public class Componenter : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] public ListController listController;
    [SerializeField] public CellGenerator cellGenerator;
    [SerializeField] public CellController cellController;
    [SerializeField] public Wave wave;
    [SerializeField] public AStar astar;
}
