using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class Labelller : MonoBehaviour
{
    [SerializeField] private Vector2Int cords = new Vector2Int();
    public Vector2Int Cords => cords;

    GridManager gridManager;
    TextMeshPro label;

    public void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponentInChildren<TextMeshPro>();

        //DisplayCords();
    }

    public void Update()
    {
        //DisplayCords();
        transform.name = cords.ToString();
    }

    public void DisplayCords()
    {
        if (!gridManager) return;
        cords.x = Mathf.Abs(Mathf.RoundToInt(transform.position.y / gridManager.UnityGridSize));
        cords.y = Mathf.RoundToInt(transform.position.x / gridManager.UnityGridSize);

        label.text = $"{cords.x},{cords.y}";
    }
}
