using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private EnemyAI enemyAI;

    private void Start()
    {
        // Find the EnemyAI component in the scene
        enemyAI = FindObjectOfType<EnemyAI>();

        if (enemyAI == null)
        {
            Debug.LogError("EnemyAI script not found");
        }
    }

    private void Update()
    {
        if (enemyAI != null)
        {
            // Call the MoveTowardsPlayer method to make the enemy move towards the player
            enemyAI.MoveTowardsPlayer();
        }
    }
}