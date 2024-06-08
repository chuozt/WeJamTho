using UnityEngine;

public class UnitManager : Singleton<UnitManager>
{
    [SerializeField] Grid grid;
    [SerializeField] float moveGrid = 1f;
    Transform selectedUnit;
    bool unitSelected = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            // Convert mouse position to world position
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0; // Ensure the z-coordinate is 0 since we're in 2D space

            // Calculate grid position
            Vector3Int gridPosition = (Vector3Int)WorldToGrid(mouseWorldPosition);

            Debug.Log("Grid Position: " + gridPosition);
            GameObject obj = grid.CellToWorld(gridPosition);
            
        }
    }

    Vector2Int WorldToGrid(Vector3 worldPosition)
    {
        // Calculate the grid cell indices
        int x = Mathf.FloorToInt(worldPosition.x / GridManager.Instance.UnityGridSize);
        int y = Mathf.FloorToInt(worldPosition.y / GridManager.Instance.UnityGridSize);

        return new Vector2Int(x, y);
    }
}
