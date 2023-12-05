using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeller : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.red;
    [SerializeField] Color openNotPlaceable = Color.magenta;
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    Waypoint waypoint;

    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        waypoint = GetComponentInParent<Waypoint>();
        DisplayCoordinates();
    }
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
        }

        ColorCoordinates();
        ToggleLabels();
    }

    void ColorCoordinates()
    {
        if (waypoint.IsPlaceable && waypoint.EmptyTile)
        {
            label.color = defaultColor;
        }
        else if (!waypoint.IsPlaceable && waypoint.EmptyTile)
        {
            label.color = openNotPlaceable;
        }
        else
        {
            label.color = blockedColor;
        }
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.enabled;

        }
    }

    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        label.text = coordinates.x + ", " + coordinates.y;

        ChangeParentName();
    }

    private void ChangeParentName()
    {
        transform.parent.name = label.text;
    }
}
