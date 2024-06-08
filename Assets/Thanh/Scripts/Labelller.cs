using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class Labelller : MonoBehaviour
{
    TextMeshPro label;
    public Vector2Int cords = new Vector2Int();
    GridManager gridManager;

    public void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponentInChildren<TextMeshPro>();

        DisplayCords();
    }

    public void Update()
    {
        DisplayCords();
        transform.name = cords.ToString();
    }

    public void DisplayCords()
    {
        if (!gridManager) return;
        cords.x = Mathf.RoundToInt(transform.position.x / gridManager.UnityGridSize);
        cords.y = Mathf.RoundToInt(transform.position.y / gridManager.UnityGridSize);

        label.text = $"{cords.x},{cords.y}";
    }
}
