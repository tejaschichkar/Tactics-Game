using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleData", menuName = "ScriptableObjects/ObstacleData", order = 1)]
public class ObstacleData : ScriptableObject
{
    public bool[,] obstacleGrid = new bool[10, 10]; // Assuming a 10x10 grid

    // Additional properties, methods, or events related to obstacle data can be added here.
}
