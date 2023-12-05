using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }
    bool emptyTile = true;
    public bool EmptyTile { get { return emptyTile; } }

    void OnMouseDown()
    {
        if (isPlaceable && emptyTile)
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            emptyTile = !isPlaced;

        }
    }
}
