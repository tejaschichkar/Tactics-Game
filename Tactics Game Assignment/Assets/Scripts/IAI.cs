using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAI
{
    void MoveTowardsPlayer();
    void TakeDamage(int damage);
    // Add more AI-related methods or properties as needed
}
