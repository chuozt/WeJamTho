using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    List<UnitHouse> houses = new List<UnitHouse>();
    int points = 0;
    private UnitSlot[,] unitSlots;

    void Start()
    {
        unitSlots = UnitManager.Instance.unitSlots;
    }
    void OnEnable()
    {
        UnitManager.onEndMove += OnNewTurn;
    }

    void OnDisable()
    {
        UnitManager.onEndMove -= OnNewTurn;
    }

    void OnNewTurn()
    {
        AddScore();
        //DestroyHouse();
    }

    void AddScore()
    {
        UnitHouse mainHouse = houses.Find(h => h.UnitType == UnitType.House);
        List<UnitHouse> adjacentHouses = GetAdjacentHouse(mainHouse.Position);
        foreach(UnitHouse house in adjacentHouses)
        {
            //ADD POINTS
            //house == house.HouseLevel.Equals(HouseLevel.Lv1) + 1;
            //points += house.HouseLevel.Equals(HouseLevel.Lv1);
            DestroyHouse(house);
        }
        //UnitSlot houseSlot = CheckForAdjacentHouse();
        List<UnitHouse> GetAdjacentHouse(Vector2Int position)
        {
            List<UnitHouse> adjacentHouse = new List<UnitHouse>();
            Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

            foreach (Vector2Int direction in directions)
            {
                Vector2Int adjacentPosition = position + direction;
                UnitHouse aHouse = houses.Find(h => h.Position == adjacentPosition && h.UnitType == UnitType.House);

                if(aHouse != null) 
                    adjacentHouse.Add(aHouse);
            }
            return adjacentHouse;
        }
    }

    //private UnitSlot CheckForAdjacentHouse()
    //{
    //    List<UnitSlot> houses = new List<UnitSlot>();
    //    foreach(UnitSlot house in unitSlots)
    //    {
    //        if(house.Unit != null && house.Unit.UnitType == UnitType.House)
    //            houses.Add(house);
    //    }
    //    return houses[];
    //}

    void DestroyHouse(UnitHouse house)
    {
        houses.Remove(house);
    }
}
