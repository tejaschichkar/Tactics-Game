using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool isMoving = false;

    void Update()
    {
        if (!isMoving && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Tile"))
                {
                    Vector3 targetPosition = new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z);

                    Pathfinding pathfinding = FindObjectOfType<Pathfinding>();
                    if (pathfinding != null)
                    {
                        List<Node> path = pathfinding.FindPath(transform.position, targetPosition);
                        if (path != null)
                        {
                            // Move the player along the path
                            StartCoroutine(MoveAlongPath(path));
                        }
                        else
                        {
                            Debug.LogError("Path is null");
                        }
                    }
                    else
                    {
                        Debug.LogError("Pathfinding script not found");
                    }
                }
            }
        }
    }

    IEnumerator MoveAlongPath(List<Node> path)
    {
        if (path != null && path.Count > 0)
        {
            isMoving = true;
            foreach (Node node in path)
            {
                Vector3 targetPosition = new Vector3(node.worldPosition.x, transform.position.y, node.worldPosition.z);
                yield return StartCoroutine(MoveToTarget(targetPosition));
            }
            isMoving = false;
        }
    }

    IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        float distanceThreshold = 0.1f;

        while (Vector3.Distance(transform.position, targetPosition) > distanceThreshold)
        {
            // Check if the target tile is blocked by an obstacle
            if (!IsTileBlocked(targetPosition))
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
            else
            {
                Debug.Log("Target tile is blocked by an obstacle. Unable to move.");
                break; // Break out of the loop if the tile is blocked
            }

            yield return null;
        }
    }

    bool IsTileBlocked(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 0.1f); // Adjust the radius based on your grid spacing

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Obstacle"))
            {
                return true;
            }
        }

        return false;
    }
}