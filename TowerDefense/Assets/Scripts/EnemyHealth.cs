using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 5;

    [Tooltip("Adds amount to maxHealth when the enemy dies")]
    [SerializeField] int difficultyRamp = 1;
    int currentHealth = 5;
    Enemy enemy;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        currentHealth = maxHealth;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            enemy.DropGold();
            maxHealth += difficultyRamp;
        }
    }
}
