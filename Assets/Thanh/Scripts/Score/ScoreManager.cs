using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScoreManager : Singleton<ScoreManager>
{
    List<UnitHouse> houses = new List<UnitHouse>();
    int points = 0;
    //public int Points { get; private set; }
    [SerializeField] public TMP_Text scoreText;
    //UnityEvent OnScoreChanged;
    //private UnitSlot[,] unitSlots;

    void Start()
    {
        //unitSlots = UnitManager.Instance.unitSlots;
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
            //amount += house.HouseLevel.Equals(HouseLevel.Lv1);
            points += GetPointsPerLevel(house.HouseLevel);
            Debug.Log(points);
            points.ToString("Score: " + points);
            DestroyHouse(house);  
        }
        return;
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
                {
                    adjacentHouse.Add(aHouse);
                    Debug.Log(adjacentHouse);
                }
            }
            return adjacentHouse;
        }
    }

    int GetPointsPerLevel(HouseLevel houseLevel)
    {
        switch(houseLevel)
        {
            case HouseLevel.Lv1:
                return 1;
            case HouseLevel.Lv2:
                return 5;
            case HouseLevel.Lv3:
                return 10;
            case HouseLevel.Lv4:
                return 25;
            default:
                return 0;
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
