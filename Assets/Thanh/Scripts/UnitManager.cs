using UnityEngine;

public class UnitManager : Singleton<UnitManager>
{
    [SerializeField] Labelller[] tilesCords;
    [SerializeField] LayerMask tileLayer;
    Vector2Int[,] tilesCords2DArray = new Vector2Int[5,5];

    UnitParent selectedUnit;
    bool unitSelected = false;
    Vector2Int selectedTileCord;

    void Start()
    {
        int currentIndex = 0;
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                tilesCords2DArray[i,j] = tilesCords[currentIndex].Cords;
                currentIndex++;
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            // Convert mouse position to world position
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0;

            // Calculate grid position
            Vector2Int gridCord = WorldToGrid(mouseWorldPosition);

            Collider2D grid = Physics2D.OverlapPoint(gridCord, tileLayer);
            if(grid != null)
            {
                UnitParent currentUnit = grid.GetComponentInChildren<UnitParent>();
                if(currentUnit != null && currentUnit.UnitType != UnitType.House)
                    return;

                if(!unitSelected)
                {
                    //If there is an unit on the slot
                    if(currentUnit != null)
                        SelectState(currentUnit, gridCord);
                }
                else
                {
                    //If the player select that slot again, then Deselect
                    if(currentUnit != null && selectedUnit == currentUnit)
                    {
                        DeselectState();
                        return;
                    }

                    if(!CheckIfCanMove(gridCord)) 
                        return;

                    //If the slot is empty, then move the current unit to that slot
                    if(currentUnit == null)
                        MoveState(grid);
                    else
                    {
                        if(selectedUnit.UnitType == currentUnit.UnitType && selectedUnit.HouseLevel == currentUnit.HouseLevel)
                            MergeState(currentUnit);
                        else if(selectedUnit.UnitType == currentUnit.UnitType && selectedUnit.HouseLevel != currentUnit.HouseLevel)
                            SwapState(currentUnit);
                    }
                }
            }
        }
    }

    bool CheckIfCanMove(Vector2Int gridCord)
    {
        if((Mathf.Abs(gridCord.x - selectedTileCord.x) == 1 && gridCord.y == selectedTileCord.y) ||
            (gridCord.x == selectedTileCord.x && Mathf.Abs(gridCord.y - selectedTileCord.y) == 1))
                return true;
        
        return false;
    }

    void SelectState(UnitParent unit, Vector2Int gridCord)
    {
        unitSelected = true;
        selectedUnit = unit;
        selectedTileCord = gridCord;
        selectedUnit.GetComponent<SpriteRenderer>().color = Color.green;
        Debug.Log("Selecting");
    }

    void DeselectState()
    {
        selectedUnit.GetComponent<SpriteRenderer>().color = Color.yellow;
        unitSelected = false;
        selectedUnit = null;
        selectedTileCord = Vector2Int.zero;
        Debug.Log("Deselected");
    }

    void MoveState(Collider2D grid)
    {
        unitSelected = false;
        selectedUnit.transform.SetParent(grid.transform);
        selectedUnit.transform.localPosition = Vector3.zero;
        DeselectState();
    }

    void SwapState(UnitParent unit)
    {
        Transform tempParrent = selectedUnit.transform.parent;

        selectedUnit.transform.SetParent(unit.transform.parent);
        selectedUnit.transform.localPosition = Vector3.zero;

        unit.transform.SetParent(tempParrent);
        unit.transform.localPosition = Vector3.zero;
        DeselectState();
    }

    void MergeState(UnitParent unit)
    {
        DeselectState();
    }

    Vector2Int WorldToGrid(Vector3 worldPosition)
    {
        // Calculate the grid cell indices
        int x = Mathf.FloorToInt(worldPosition.x / GridManager.Instance.UnityGridSize);
        int y = Mathf.FloorToInt(worldPosition.y / GridManager.Instance.UnityGridSize);

        return new Vector2Int(x, y);
    }
}
