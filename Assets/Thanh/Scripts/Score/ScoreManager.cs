using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] List<UnitHouse> houses = new List<UnitHouse>();
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

    public void AddScore()
    {
        // Find the main house
        UnitHouse mainHouse = houses.Find(h => h.UnitType == UnitType.MainHouse);
        if (mainHouse == null) return;

        // Get adjacent houses
        List<UnitHouse> adjacentHouses = GetAdjacentHouses(mainHouse.Position);

        // Add points for each adjacent house and destroy them
        foreach (UnitHouse house in adjacentHouses)
        {
            points += GetPointsPerLevel(house.HouseLevel);
            Debug.Log($"Score: {points}");
            DestroyHouse(house);
        }
    }

    private List<UnitHouse> GetAdjacentHouses(Vector2Int position)
    {
        List<UnitHouse> adjacentHouses = new List<UnitHouse>();
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        foreach (Vector2Int direction in directions)
        {
            Vector2Int adjacentPosition = position + direction;
            UnitHouse adjacentHouse = houses.Find(h => h.Position == adjacentPosition && h.UnitType == UnitType.House);

            if (adjacentHouse != null)
            {
                adjacentHouses.Add(adjacentHouse);
            }
        }

        return adjacentHouses;
    }

    private int GetPointsPerLevel(HouseLevel level)
    {
        switch (level)
        {
            case HouseLevel.Lv1:
                return 1;
            case HouseLevel.Lv2:
                return 2;
            case HouseLevel.Lv3:
                return 3;
            case HouseLevel.Lv4:
                return 4;
            default:
                return 0;
        }
    }

    void DestroyHouse(UnitHouse house)
    {
        houses.Remove(house);
    }
}
