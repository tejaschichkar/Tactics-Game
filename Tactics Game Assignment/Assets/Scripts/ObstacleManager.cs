using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager Instance;

    public ObstacleData obstacleData;
    public GameObject obstaclePrefab;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateObstacles();
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
