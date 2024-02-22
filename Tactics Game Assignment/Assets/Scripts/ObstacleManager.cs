using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager Instance;

    public ObstacleData obstacleData;
    public GameObject obstaclePrefab;
    public float x, y, z;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Update obstacles and then change position and rotation
        UpdateObstacles();

        // Set a new position for the ObstacleManager
        Vector3 newPosition = new Vector3(2.5f, 1f, 7.1f);
        transform.position = newPosition;

        // Set a new rotation for the ObstacleManager
        Quaternion newRotation = Quaternion.Euler(0f, 45f, 0f);
        transform.rotation = newRotation;
    }

    public void UpdateObstacles()
    {
        ClearObstacles();

        for (int x = 0; x < obstacleData.obstacleGrid.GetLength(0); x++)
        {
            for (int z = 0; z < obstacleData.obstacleGrid.GetLength(1); z++)
            {
                if (obstacleData.obstacleGrid[x, z])
                {
                    Vector3 spawnPosition = new Vector3(x * 1.2f, 2f, z * 1.2f);
                    Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        // Example: Draw a wireframe cube around the ObstacleManager object
        Gizmos.DrawWireCube(transform.position, new Vector3(x, y, z)); // Adjust the size as needed
    }

    void ClearObstacles()
    {
        // Remove existing obstacles
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }
    }
}