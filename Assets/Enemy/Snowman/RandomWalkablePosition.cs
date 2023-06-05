using UnityEngine;
using Pathfinding;
using System.Collections.Generic;

public class RandomWalkablePosition : MonoBehaviour
{
    public GameObject objectPrefab; // Prefab of the object to be placed on the grid

    private void Start()
    {
        // Find the AstarPath component
        AstarPath astarPath = FindObjectOfType<AstarPath>();

        if (astarPath == null)
        {
            Debug.LogError("AstarPath component not found in the scene.");
            return;
        }

        // Get the GridGraph from the AstarPath component
        GridGraph gridGraph = astarPath.data.gridGraph;

        if (gridGraph == null)
        {
            Debug.LogError("GridGraph component not found in the AstarPath component.");
            return;
        }

        // Get the width, depth, and nodes of the GridGraph
        int width = gridGraph.width;
        int depth = gridGraph.depth;
        GraphNode[] nodes = gridGraph.nodes;

        // Collect walkable node positions
        List<Vector3> walkablePositions = new List<Vector3>();

        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = x + z * width;
                GraphNode node = nodes[index];

                if (node.Walkable)
                {
                    Vector3 position = (Vector3)node.position;
                    walkablePositions.Add(position);
                }
            }
        }

        // Check if any walkable positions were found
        if (walkablePositions.Count == 0)
        {
            Debug.LogWarning("No walkable positions found.");
            return;
        }

        // Select a random walkable position
        Vector3 randomPosition = walkablePositions[Random.Range(0, walkablePositions.Count)];

        // Instantiate the object prefab at the random position
        GameObject createdObject = Instantiate(objectPrefab, randomPosition, Quaternion.identity);
        Debug.Log("Object placed at position: " + randomPosition);

        // Get the AIDestinationSetter component
        AIDestinationSetter destinationSetter = GetComponent<AIDestinationSetter>();

        if (destinationSetter != null)
        {
            // Set the created object as the target in the AIDestinationSetter script
            destinationSetter.target = createdObject.transform;
        }
        else
        {
            Debug.LogError("AIDestinationSetter component not found.");
        }
    }

    public void RepositionMarker()
    {
        // Find the AstarPath component
        AstarPath astarPath = FindObjectOfType<AstarPath>();

        if (astarPath == null)
        {
            Debug.LogError("AstarPath component not found in the scene.");
            return;
        }

        // Get the GridGraph from the AstarPath component
        GridGraph gridGraph = astarPath.data.gridGraph;

        if (gridGraph == null)
        {
            Debug.LogError("GridGraph component not found in the AstarPath component.");
            return;
        }

        // Get the width, depth, and nodes of the GridGraph
        int width = gridGraph.width;
        int depth = gridGraph.depth;
        GraphNode[] nodes = gridGraph.nodes;

        // Collect walkable node positions
        List<Vector3> walkablePositions = new List<Vector3>();

        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = x + z * width;
                GraphNode node = nodes[index];

                if (node.Walkable)
                {
                    Vector3 position = (Vector3)node.position;
                    walkablePositions.Add(position);
                }
            }
        }

        // Check if any walkable positions were found
        if (walkablePositions.Count == 0)
        {
            Debug.LogWarning("No walkable positions found.");
            return;
        }

        // Select a random walkable position
        Vector3 randomPosition = walkablePositions[Random.Range(0, walkablePositions.Count)];

        // Move the marker to the random position
        transform.position = randomPosition;
        Debug.Log("Marker repositioned at position: " + randomPosition);
    }
}
