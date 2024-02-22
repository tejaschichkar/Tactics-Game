using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IAI
{
    private Griddle griddle; // Reference to the Griddle script
    private Pathfinding pathfinding;
    private Transform playerTransform;
    private Vector3 lastPlayerPosition;

    private void Start()
    {
        griddle = FindObjectOfType<Griddle>();
        pathfinding = FindObjectOfType<Pathfinding>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (griddle == null)
        {
            Debug.LogError("Griddle script not found");
        }

        if (playerTransform == null)
        {
            Debug.LogError("Player transform not found");
        }

        // Set the initial player position
        lastPlayerPosition = playerTransform.position;
    }

    public void MoveTowardsPlayer()
    {
        if (griddle == null || playerTransform == null)
        {
            return; // Return early if references are not set
        }

        // Check if the player has moved to a new position
        if (lastPlayerPosition != playerTransform.position)
        {
            // Find the nearest node to the enemy's current position
            Node startNode = GetNearestNode(transform.position);

            // Find the nearest node to the player's position
            Node targetNode = GetNearestNode(playerTransform.position);

            // Perform pathfinding to find a path from the enemy to the player
            List<Node> path = pathfinding.FindPath(startNode.worldPosition, targetNode.worldPosition);

            // Move the enemy along the path
            StartCoroutine(MoveAlongPath(path));

            // Update the last known player position
            lastPlayerPosition = playerTransform.position;
        }
    }

    private Node GetNearestNode(Vector3 position)
    {
        // Convert the world position to grid coordinates
        int gridX = Mathf.FloorToInt((position.x - griddle.transform.position.x) / griddle.nodeSize);
        int gridY = Mathf.FloorToInt((position.z - griddle.transform.position.z) / griddle.nodeSize);

        // Ensure the coordinates are within the grid bounds
        gridX = Mathf.Clamp(gridX, 0, griddle.gridSize.x - 1);
        gridY = Mathf.Clamp(gridY, 0, griddle.gridSize.y - 1);

        // Return the nearest node
        return griddle.grid[gridX, gridY];
    }

    private IEnumerator MoveAlongPath(List<Node> path)
    {
        if (path != null && path.Count > 0)
        {
            foreach (Node node in path)
            {
                // Get the nearest walkable node to the player's position
                Node playerNode = GetNearestNode(playerTransform.position);
                Node targetNode = GetAdjacentWalkableNode(playerNode);

                if (targetNode != null)
                {
                    Vector3 targetPosition = new Vector3(targetNode.worldPosition.x, transform.position.y, targetNode.worldPosition.z);
                    yield return StartCoroutine(MoveToTarget(targetPosition));
                }
                else
                {
                    Debug.LogWarning("No adjacent walkable node found for enemy");
                    yield break; // Break out of the coroutine if no valid target node is found
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        // Implement your damage logic here
        Debug.Log($"Enemy takes {damage} damage!");

        // For simplicity, let's assume reducing health by the damage value
        // You may want to implement a more robust health system in your actual game
        // For example, you could have a separate Health component on the enemy
        // that handles health-related functionality
        // health -= damage;
    }

    private Node GetAdjacentWalkableNode(Node node)
    {
        // Check the four adjacent nodes (up, down, left, right) and return the first walkable one
        foreach (Vector2Int offset in new Vector2Int[] { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right })
        {
            int checkX = node.gridX + offset.x;
            int checkY = node.gridY + offset.y;

            if (IsNodeWalkable(checkX, checkY))
            {
                return griddle.grid[checkX, checkY];
            }
        }

        return null; // Return null if no walkable node is found
    }

    private bool IsNodeWalkable(int x, int y)
    {
        // Check if the node is within the grid bounds and walkable
        return x >= 0 && x < griddle.gridSize.x && y >= 0 && y < griddle.gridSize.y && griddle.grid[x, y].walkable;
    }

    private IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        float moveSpeed = 5f;

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Optional: Rotate the enemy to face the movement direction
            Vector3 lookDirection = targetPosition - transform.position;
            if (lookDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), 10f * Time.deltaTime);
            }

            yield return null;
        }
    }
}