using UnityEngine;
using Pathfinding;

public class GridGraphSetup : MonoBehaviour
{
    private GridGraph gg;

    private void Start()
    {
        // Hole das GridGraph-Objekt
        gg = AstarPath.active.data.gridGraph;

        // Scanne den Graphen erneut
        AstarPath.active.Scan();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Hallo");
            // Aktualisiere die Werte und führe den Scan erneut aus
            gg.center = new Vector3(20, 40, 0);
            AstarPath.active.Scan();
        }
    }
}
