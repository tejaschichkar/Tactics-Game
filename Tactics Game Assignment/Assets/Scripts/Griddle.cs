using UnityEngine;

public class Griddle : MonoBehaviour
{
    public LayerMask obstacleLayer;
    public Vector2Int gridSize;
    public float nodeSize;

    public Node[,] grid;

    void Start()
    {
        CreateGrid();

        // Set a new position for the Griddle
        Vector3 newPosition = new Vector3(2.5f, 1f, 7.1f);
        transform.position = newPosition;

        // Set a new rotation for the Griddle
        Quaternion newRotation = Quaternion.Euler(45f, 0f, 0f);
        transform.rotation = newRotation;
    }

    void CreateGrid()
    {
        grid = new Node[gridSize.x, gridSize.y];

        Vector3 worldBottomLeft = transform.position - Vector3.right * gridSize.x * nodeSize / 2 - Vector3.forward * gridSize.y * nodeSize / 2;

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeSize + nodeSize / 2) + Vector3.forward * (y * nodeSize + nodeSize / 2);
                bool isWalkable = !Physics.CheckSphere(worldPoint, nodeSize / 2, obstacleLayer);
                grid[x, y] = new Node(isWalkable, worldPoint, x, y);
            }
        }
    }

    void OnDrawGizmos()
    {
        // Example: Draw a wireframe cube around the grid object
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x * nodeSize, 0.1f, gridSize.y * nodeSize));
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        // Convert world coordinates to grid coordinates
        int x = Mathf.FloorToInt((worldPosition.x + gridSize.x * nodeSize / 2) / nodeSize);
        int y = Mathf.FloorToInt((worldPosition.z + gridSize.y * nodeSize / 2) / nodeSize);

        // Ensure that x and y are within the bounds of the grid array
        x = Mathf.Clamp(x, 0, gridSize.x - 1);
        y = Mathf.Clamp(y, 0, gridSize.y - 1);

        // Debug logs for debugging
        Debug.Log($"World Position: {worldPosition}, X: {x}, Y: {y}");

        // Return the corresponding Node from the grid
        return grid[x, y];
    }
}