using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] float movementSpeed = 0.5f;
    [SerializeField] Transform weapon;
    [SerializeField] float range = 20f;
    [SerializeField] ParticleSystem projectileParticle;
    Transform target;
    EnemyMover enemyMover;
    void Awake()
    {
        Enemy enemy = FindObjectOfType<Enemy>();
        if (enemy != null)
        {
            target = enemy.transform;
            enemyMover = FindObjectOfType<EnemyMover>();
        }
    }

    void Update()
    {
        if (target == null || !target.gameObject.activeInHierarchy || IsTargetOutOfRange())
        {
            FindClosestTarget();
            Attack(false);
        }
        else
        {
            AimWeapon();
        }
    }

    bool IsTargetOutOfRange()
    {
        return Vector3.Distance(transform.position, target.transform.position) > range;
    }

    // bool IsTargetSelf()
    // {
    //     return target.gameObject == gameObject;
    // }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = range;

        foreach (Enemy enemy in enemies)
        {
            
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        target = closestTarget;
    }

    void AimWeapon()
    {
        float aimMultiplier = 0f;

        if (enemyMover != null)
        {
            aimMultiplier = enemyMover.EnemyMovementSpeed * 2;
        }

        Vector3 aimPoint = target.position + (target.forward * aimMultiplier); // aims ahead of the enemy based on its movement speed

        float targetDistance = Vector3.Distance(transform.position, aimPoint); // aimpointchange

        StartCoroutine(LookAtTargetSmooth(aimPoint));

        if (targetDistance < range)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    void Attack(bool isActive)
    {
        ParticleSystem.EmissionModule emissionModule = projectileParticle.emission;
        emissionModule.enabled = isActive;
    }

    IEnumerator LookAtTargetSmooth(Vector3 aimPoint)
    {
        Quaternion lookRotation = Quaternion.LookRotation(aimPoint - transform.position);

        float travelPercent = 0f;

        while (travelPercent < 1f)
        {
            travelPercent += movementSpeed * Time.deltaTime;

            weapon.transform.rotation = Quaternion.Slerp(weapon.transform.rotation, lookRotation, travelPercent);

            yield return new WaitForEndOfFrame();
        }
    }
}