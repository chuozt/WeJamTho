using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// public class UnitManager : Singleton<UnitManager>
// {
//     [SerializeField] Labelller[] tilesCords;
//     [SerializeField] LayerMask tileLayer;
//     [SerializeField] List<GameObject> housePrefabs;
//     UnitSlot[,] unitSlots = new UnitSlot[5,5];

//     [SerializeField] GameObject specialGridPrefabs;
//     int maximumSpecialGridSpawn = 3;
//     int count = 0;

//     UnitSlot selectedUnitSlot;
//     bool unitSelected = false;
//     Vector2Int selectedTileCord;
//     Color originalColor;

//     public static event Action onEndSpecialMove;

//     void Start()
//     {
//         SaveTilesCordsIn2DArray();
//         InitSpecialGrid();
//         selectedUnitSlot = null;
//     }

//     void SaveTilesCordsIn2DArray()
//     {
//         int currentIndex = 0;
//         for(int i = 0; i < 5; i++)
//         {
//             for (int j = 0; j < 5; j++)
//             {
//                 unitSlots[j,i] = tilesCords[currentIndex].gameObject.GetComponent<UnitSlot>();
//                 //Debug.Log(unitSlots[j,i].Unit);
//                 currentIndex++;
//             }
//         }
//     }

//     void InitSpecialGrid()
//     {
//         while(count < maximumSpecialGridSpawn)
//         {
//             int randomYIndex = UnityEngine.Random.Range(0, 4);
//             int randomXIndex = UnityEngine.Random.Range(0, 4);
//             if (!unitSlots[randomXIndex, randomYIndex].IsSpecialGrid)
//             {
//                 unitSlots[randomXIndex, randomYIndex].IsSpecialGrid = true;
//                 count++;
//                 GameObject newUnit = Instantiate(specialGridPrefabs, unitSlots[randomXIndex, randomYIndex].transform.position, Quaternion.identity);
//                 newUnit.transform.SetParent(unitSlots[randomXIndex, randomYIndex].transform);
//                 newUnit.transform.localPosition = Vector3.zero;
//             }
//             else
//                 continue;
//         }

//         //GameObject newUnit = Instantiate(specialGridPrefabs, new Vector3(randomPosition.x, randomPosition.y, 0), Quaternion.identity);
//         //newUnit.transform.SetParent(.transform.parent);
//         //newUnit.transform.localPosition = Vector3.zero;

//     }

//     private void Update()
//     {
//         if (Input.GetMouseButtonDown(0)) // Left mouse button
//         {
//             // Convert mouse position to world position
//             Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//             mouseWorldPosition.z = 0;

//             // Calculate grid position
//             Vector2Int gridCord = WorldToGrid(mouseWorldPosition);
//             gridCord.y = Mathf.Abs(gridCord.y);

//             if(unitSlots[gridCord.x, gridCord.y].Unit != null)
//             {
//                 UnitSlot slot = unitSlots[gridCord.x, gridCord.y];
//                 Unit unit = slot.Unit;

//                 if(unit != null && unit.UnitType != UnitType.House)
//                     return;

//                 if(!unitSelected)
//                 {
//                     //If there is an unit on the slot
//                     if(unit != null)
//                         SelectState(slot, gridCord);
//                 }
//                 else
//                 {
//                     //If the player select that slot again, then Deselect
//                     if(unit != null && selectedUnitSlot.Unit == unit)
//                     {
//                         DeselectState();
//                         return;
//                     }

//                     if(!CheckIfCanMove(gridCord)) 
//                         return;

//                     //If the slot is empty, then move the current unit to that slot
//                     if(unit == null)
//                         MoveState(slot);
//                     else
//                     {
//                         if(unit.UnitType == UnitType.Rock)
//                             return;
                        
//                         if(selectedUnitSlot.Unit.UnitType == unit.UnitType && ((UnitHouse)selectedUnitSlot.Unit).HouseLevel == ((UnitHouse)slot.Unit).HouseLevel)
//                             MergeState((UnitHouse)unit);
//                         else if(selectedUnitSlot.Unit.UnitType == unit.UnitType && ((UnitHouse)selectedUnitSlot.Unit).HouseLevel != ((UnitHouse)slot.Unit).HouseLevel)
//                             SwapState(selectedUnitSlot, slot);
//                     }
//                 }
//             }
//         }
//     }

//     bool CheckIfCanMove(Vector2Int gridCord)
//     {
//         Debug.Log(gridCord);
//         if((Mathf.Abs(gridCord.x - selectedTileCord.x) == 1 && gridCord.y == selectedTileCord.y) ||
//             (gridCord.x == selectedTileCord.x && Mathf.Abs(gridCord.y - selectedTileCord.y) == 1))
//             {
//                 Debug.Log("Tr");
//                 return true;
//             }
        
//         return false;
//     }

//     void GetUnitInSlot(UnitSlot unitSlot, Unit unit)
//     {

//     }

//     void SetUnitInSlot(UnitSlot unitSlot, Unit unitToSet)
//     {
//         unitSlot.Unit = unitToSet;
//         unitToSet.transform.SetParent(unitSlot.transform);
//         unitToSet.transform.localPosition = Vector3.zero;
//     }

//     void SelectState(UnitSlot unitSlot, Vector2Int gridCord)
//     {
//         unitSelected = true;
//         selectedUnitSlot = unitSlot;
//         selectedUnitSlot.Unit = unitSlot.Unit;
//         selectedTileCord = gridCord;
//         originalColor = selectedUnitSlot.Unit.GetComponent<SpriteRenderer>().color;
//         selectedUnitSlot.Unit.GetComponent<SpriteRenderer>().color = Color.green;
//         Debug.Log("Selecting");
//     }

//     void DeselectState()
//     {
//         selectedUnitSlot.Unit.GetComponent<SpriteRenderer>().color = originalColor;
//         unitSelected = false;
//         selectedUnitSlot = null;
//         selectedTileCord = Vector2Int.zero;
//         Debug.Log("Deselected");
//     }

//     void MoveState(UnitSlot targetSlot)
//     {
//         selectedUnitSlot.Unit.transform.SetParent(targetSlot.transform);
//         selectedUnitSlot.Unit.transform.localPosition = Vector3.zero;
//         targetSlot.Unit = selectedUnitSlot.Unit;
//         selectedUnitSlot.Unit = null;
//         DeselectState();
//     }

//     public void SpecialMove(Transform targetGrid)
//     {
        
//     }

//     void SwapState(UnitSlot unitSlot1, UnitSlot unitSlot2)
//     {
//         Unit tempUnit1 = unitSlot1.Unit;
//         Unit tempUnit2 = unitSlot2.Unit;

//         SetUnitInSlot(unitSlot1, tempUnit2);
//         SetUnitInSlot(unitSlot2, tempUnit1);

//         DeselectState();
//     }

//     void MergeState(UnitHouse unit)
//     {
//         int index = (int)unit.HouseLevel + 1;
//         if (index == 4)
//             return;
//         GameObject newUnit = Instantiate(housePrefabs[index], Vector3.zero, Quaternion.identity);
//         newUnit.transform.SetParent(unit.transform.parent);
//         newUnit.transform.localPosition = Vector3.zero;
//         Destroy(selectedUnitSlot.Unit.gameObject);
//         Destroy(unit.gameObject);
//         DeselectState();
//     }

//     Vector2Int WorldToGrid(Vector3 worldPosition)
//     {
//         // Calculate the grid cell indices
//         int x = Mathf.FloorToInt(worldPosition.x / GridManager.Instance.UnityGridSize);
//         int y = Mathf.FloorToInt(worldPosition.y / GridManager.Instance.UnityGridSize);

//         return new Vector2Int(x, y);
//     }
// }

public class UnitManager : Singleton<UnitManager>
{
    [SerializeField] Labelller[] tilesCords;
    [SerializeField] LayerMask tileLayer;
    [SerializeField] List<GameObject> housePrefabs;
    [SerializeField] UnitSlot[,] unitSlots = new UnitSlot[5, 5];

    [SerializeField] GameObject specialGridPrefabs;
    int maximumSpecialGridSpawn = 3;
    int count = 0;
    Vector2Int[] spawnedSpecialGrid;

    UnitSlot selectedUnitSlot;
    bool unitSelected = false;
    Vector2Int selectedTileCord;
    Color originalColor;

    public static event Action onEndMove;

    void Start()
    {
        SaveTilesCordsIn2DArray();
        InitSpecialGrid();
        selectedUnitSlot = null;
        //spawnedSpecialGrid = new Vector2Int[];
    }

    void SaveTilesCordsIn2DArray()
    {
        int currentIndex = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                unitSlots[j, i] = tilesCords[currentIndex].gameObject.GetComponent<UnitSlot>();
                currentIndex++;
            }
        }
    }

    void InitSpecialGrid()
    {
        while (count < maximumSpecialGridSpawn)
        {
            int randomYIndex = UnityEngine.Random.Range(0, 4);
            int randomXIndex = UnityEngine.Random.Range(0, 4);
            if (!unitSlots[randomXIndex, randomYIndex].IsSpecialGrid)
            {
                unitSlots[randomXIndex, randomYIndex].IsSpecialGrid = true;
                count++;
                GameObject newUnit = Instantiate(specialGridPrefabs, unitSlots[randomXIndex, randomYIndex].transform.position, Quaternion.identity);
                newUnit.transform.SetParent(unitSlots[randomXIndex, randomYIndex].transform);
                newUnit.transform.localPosition = Vector3.zero;
                Debug.Log(unitSlots[randomXIndex, randomYIndex]);
                
            }
            else
                continue;
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
            gridCord.y = Mathf.Abs(gridCord.y);

            if (gridCord.x < 0 || gridCord.x >= 5 || gridCord.y < 0 || gridCord.y >= 5) return;

            UnitSlot slot = unitSlots[gridCord.x, gridCord.y];
            Unit unit = slot.Unit;

            if (unit != null && unit.UnitType != UnitType.House)
                return;

            if (!unitSelected)
            {
                // If there is a unit on the slot
                if (unit != null)
                    SelectState(slot, gridCord);
            }
            else
            {
                // If the player selects that slot again, then Deselect
                if (unit != null && selectedUnitSlot.Unit == unit)
                {
                    DeselectState();
                    return;
                }

                if (!CheckIfCanMove(gridCord))
                    return;

                // If the slot is empty, then move the current unit to that slot
                if (unit == null)
                    MoveState(slot);
                else
                {
                    if (unit.UnitType == UnitType.Rock)
                        return;

                    if (selectedUnitSlot.Unit.UnitType == unit.UnitType && ((UnitHouse)selectedUnitSlot.Unit).HouseLevel == ((UnitHouse)slot.Unit).HouseLevel)
                        MergeState(slot);
                    else if (selectedUnitSlot.Unit.UnitType == unit.UnitType && ((UnitHouse)selectedUnitSlot.Unit).HouseLevel != ((UnitHouse)slot.Unit).HouseLevel)
                        SwapState(selectedUnitSlot, slot);
                }
            }
        }
    }

    bool CheckIfCanMove(Vector2Int gridCord)
    {
        if ((Mathf.Abs(gridCord.x - selectedTileCord.x) == 1 && gridCord.y == selectedTileCord.y) ||
            (gridCord.x == selectedTileCord.x && Mathf.Abs(gridCord.y - selectedTileCord.y) == 1))
        {
            return true;
        }

        return false;
    }

    void CheckForSpecialMoves(Vector2Int cords)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //CheckForGrid();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

        }

        onEndMove?.Invoke();
    }

    void CheckForGrid(Unit unit)
    {
        if (unit.UnitType == UnitType.Rock)
            return;
    }

    void SelectState(UnitSlot unitSlot, Vector2Int gridCord)
    {
        unitSelected = true;
        selectedUnitSlot = unitSlot;
        selectedTileCord = gridCord;
        originalColor = selectedUnitSlot.Unit.gameObject.GetComponent<SpriteRenderer>().color;
        selectedUnitSlot.Unit.GetComponent<SpriteRenderer>().color = Color.green;
        Debug.Log("Selecting");
    }

    void DeselectState()
    {
        if(selectedUnitSlot != null)
            selectedUnitSlot.Unit.gameObject.GetComponent<SpriteRenderer>().color = originalColor;

        selectedUnitSlot = null;
        unitSelected = false;
        selectedTileCord = Vector2Int.zero;
        Debug.Log("Deselected");
    }

    void MoveState(UnitSlot targetSlot)
    {
        targetSlot.Unit = selectedUnitSlot.Unit;
        selectedUnitSlot.Unit = null;
        targetSlot.Unit.transform.SetParent(targetSlot.transform);
        GraduallyMoveLocal(targetSlot.Unit.transform);
        //targetSlot.Unit.transform.localPosition = Vector3.zero;
        selectedUnitSlot = targetSlot;

        DeselectState();
        onEndMove?.Invoke();
    }

    void SwapState(UnitSlot slot1, UnitSlot slot2)
    {
        selectedUnitSlot.Unit.gameObject.GetComponent<SpriteRenderer>().color = originalColor;

        Unit unit1 = slot1.Unit;
        Unit unit2 = slot2.Unit;

        // Swap the units
        slot1.Unit = unit2;
        slot2.Unit = unit1;

        // Update the parent and local position for each unit
        if (unit1 != null)
        {
            unit1.transform.SetParent(slot2.transform);
            unit1.transform.localPosition = Vector3.zero;
        }

        if (unit2 != null)
        {
            unit2.transform.SetParent(slot1.transform);
            unit2.transform.localPosition = Vector3.zero;
        }

        selectedUnitSlot = null;

        DeselectState();
        onEndMove?.Invoke();
    }

    void MergeState(UnitSlot slot)
    {
        int index = (int)((UnitHouse)slot.Unit).HouseLevel + 1;
        if (index >= housePrefabs.Count)
            return;

        GameObject newUnit = Instantiate(housePrefabs[index], Vector3.zero, Quaternion.identity);
        newUnit.transform.SetParent(slot.Unit.transform.parent);
        newUnit.transform.localPosition = Vector3.zero;
        Destroy(selectedUnitSlot.Unit.gameObject);
        Destroy(slot.Unit.gameObject);
        slot.Unit = newUnit.GetComponent<Unit>();
        DeselectState();
        onEndMove?.Invoke();
    }

    void GraduallyMoveLocal(Transform objectToMove)
    {
        float duration = 0.25f;
        Vector3 initialLocalPosition = objectToMove.localPosition;
        float startTime = Time.time;

        // Start a coroutine to gradually move the object
        StartCoroutine(MoveCoroutine());

        IEnumerator MoveCoroutine()
        {
            // While the duration hasn't elapsed yet
            while (Time.time - startTime < duration)
            {
                // Calculate the percentage of completion based on the elapsed time
                float percentageComplete = (Time.time - startTime) / duration;

                // Interpolate the object's local position towards the target local position
                objectToMove.localPosition = Vector3.Lerp(initialLocalPosition, Vector3.zero, percentageComplete);

                // Wait for the next frame
                yield return null;
            }

            // Ensure the object reaches the exact target local position
            objectToMove.localPosition = Vector3.zero;
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
