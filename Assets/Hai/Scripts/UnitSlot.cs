using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSlot : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private UnitHazard unitHazard;
    Vector2Int slotCoordinates;

    public bool IsSpecialGrid { get; set; } = false;
    public Unit Unit { get{ return unit; } set{ unit = value; }}
    public UnitHazard UnitHazard { get{ return unitHazard; } set{ unitHazard = value; }}

    void OnEnable()
    {
        UnitHazard.onHazardUpdated += HazardAttack;
    }

    void OnDisable()
    {
        UnitHazard.onHazardUpdated -= HazardAttack;
    }

    void Awake()
    {
        slotCoordinates = GetComponent<Labelller>().Cords;
        unit = GetComponentInChildren<UnitHouse>();
        if (unit == null)
            unit = GetComponentInChildren<UnitRock>();

        unitHazard = GetComponentInChildren<UnitHazard>();
    }

    void HazardAttack()
    {
        if (unitHazard == null || unit == null || unitHazard.isGhostMode)
            return;

        switch (unitHazard.hazardType)
        {
            case HazardType.Fire:
                HandleFire();
                break;
            case HazardType.Tornado:
                HandleTornado();
                break;
            case HazardType.Lightning:
                HandleLightning();
                break;
        }
    }

    void HandleFire()
    {
        if (unit is UnitHouse)
        {
            Destroy(unit.gameObject);
            Destroy(unitHazard.gameObject);
            unit = null;
            unitHazard = null;
        }
    }

    void HandleTornado()
    {
        if(!unitHazard.isGhostMode)
        {
            if (unit is UnitHouse)
            {
                Destroy(unit.gameObject);
                Destroy(unitHazard.gameObject);
                unit = null;
                unitHazard = null;
                return;
            }
        }

        Vector2Int direction = unitHazard.initialDirection;
        Vector2Int currentGridPosition = slotCoordinates;
        Vector2Int nextGridPosition = currentGridPosition + direction;

        if (UnitManager.Instance.IsPositionValid(nextGridPosition))
        {
            UnitSlot nextSlot = UnitManager.Instance.GetUnitSlot(nextGridPosition);
            if (nextSlot.Unit is UnitHouse)
            {
                Destroy(nextSlot.Unit.gameObject);
                nextSlot.Unit = null;
                Destroy(unitHazard.gameObject);
                unitHazard = null;
                return;
            }

            nextSlot.Unit = this.unit;
            this.unit = null;
            slotCoordinates = nextGridPosition;

            transform.SetParent(nextSlot.transform);
            transform.localPosition = Vector3.zero;
        }  
    }

    void HandleLightning()
    {
        if (unit is UnitHouse)
        {
            HashSet<UnitHouse> connectedHouses = new HashSet<UnitHouse>();
            connectedHouses.Add((UnitHouse)unit);
            FindConnectedHouses(slotCoordinates, connectedHouses);

            foreach (UnitHouse house in connectedHouses)
            {
                DecreaseHouseLevel(house);
            }

            Debug.Log("Light");
            Destroy(unitHazard.gameObject, 0.1f);
            unitHazard = null;
            return;
        }
        else
        {
            Destroy(unitHazard.gameObject, 0.1f);
            unitHazard = null;
            return;
        }
    }

    void FindConnectedHouses(Vector2Int position, HashSet<UnitHouse> connectedHouses)
    {
        if (!UnitManager.Instance.IsPositionValid(position))
            return;

        UnitSlot slot = UnitManager.Instance.GetUnitSlot(position);
        if (slot.Unit is UnitHouse house && !connectedHouses.Contains(house) && slot.Unit.UnitType != UnitType.MainHouse)
        {
            connectedHouses.Add(house);

            Vector2Int[] directions = new Vector2Int[]
            {
                Vector2Int.up,
                Vector2Int.down,
                Vector2Int.left,
                Vector2Int.right
            };

            foreach (Vector2Int dir in directions)
            {
                FindConnectedHouses(position + dir, connectedHouses);
            }
        }
    }

    void DecreaseHouseLevel(UnitHouse house)
    {
        Vector2Int position = house.GetComponentInParent<UnitSlot>().slotCoordinates;
        UnitSlot houseSlot = UnitManager.Instance.GetUnitSlot(new Vector2Int(position.y, position.x));
        
        if (house.HouseLevel > 0 && house.HouseLevel != HouseLevel.Main)
        {
            house.HouseLevel--;
            int newIndex = (int)house.HouseLevel;
            GameObject newHousePrefab = UnitManager.Instance.housePrefabs[newIndex];
            GameObject newHouse = Instantiate(newHousePrefab, house.transform.position, Quaternion.identity);
            newHouse.transform.SetParent(house.transform.parent);
            newHouse.transform.localPosition = Vector3.zero;
            houseSlot.Unit = newHouse.GetComponent<Unit>();
            GameObject tempLightning = Instantiate(UnitSpawnerManager.Instance.lightningPrefab, house.transform.position, Quaternion.identity);
            tempLightning.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Destroy(tempLightning, 0.3f);

            Destroy(house.gameObject);
        }
        else if(house.HouseLevel != HouseLevel.Main)
        {
            Destroy(house.gameObject);
        }
    }
}
