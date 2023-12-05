using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField][Range(0f, 5f)] float enemyMovementSpeed = 0.5f;
    public float EnemyMovementSpeed { get { return enemyMovementSpeed; } }
    Quaternion lookRotation;
    Enemy enemy;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    void FindPath()
    {
        path.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();

            if (waypoint != null)
            {
                path.Add(waypoint);
            }

        }
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }
    IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;

            if (startPosition != endPosition)
            {
                lookRotation = Quaternion.LookRotation(endPosition - startPosition);

                float travelPercent = 0f;

                while (travelPercent < 1f)
                {
                    travelPercent += enemyMovementSpeed * Time.deltaTime;

                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, travelPercent);
                    transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);

                    yield return new WaitForEndOfFrame();
                }
            }
        }

        FinishPath();

    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }
}
