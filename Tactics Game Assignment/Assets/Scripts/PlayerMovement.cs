using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1.5f; // Adjust the cooldown duration as needed
    private bool isMoving = false;
    private bool isOnCooldown = false;
    private EnemyAI enemyAI;

    void Start()
    {
        enemyAI = FindObjectOfType<EnemyAI>();

        if (enemyAI == null)
        {
            Debug.LogError("EnemyAI script not found");
        }
    }

    void Update()
    {
        if (!isMoving && Input.GetMouseButtonDown(0) && !isOnCooldown)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Tile"))
                {
                    Vector3 targetPosition = new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z);

                    if (Vector3.Distance(transform.position, enemyAI.transform.position) <= attackRange)
                    {
                        // Perform attack on the enemy
                        AttackEnemy();
                    }
                    else
                    {
                        StartCoroutine(MoveToTarget(targetPosition));
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
        isMoving = true;

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
    }

    void AttackEnemy()
    {
        if (enemyAI != null && enemyAI is IAI)
        {
            (enemyAI as IAI).TakeDamage(10); // Assuming 10 as the damage value, modify as needed

            // Start the attack cooldown
            StartCoroutine(AttackCooldown());
        }
        else
        {
            Debug.LogWarning("EnemyAI does not implement the IAI interface or does not have a TakeDamage method");
        }
    }

    IEnumerator AttackCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isOnCooldown = false;
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