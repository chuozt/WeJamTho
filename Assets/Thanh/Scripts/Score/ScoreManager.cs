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
    [SerializeField] public TMP_Text scoreText;
    //private UnitSlot[,] unitSlots;
    public string text;

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
        AddScore(text);
        Debug.Log(text);
        //DestroyHouse();
    }



    void AddScore(string text)
    {
        Debug.Log(points);
        //scoreText.text = "";
        text = scoreText.text;
        foreach (UnitHouse house in houses)
        {
            //points += GetPointsPerLevel(house.HouseLevel);
            Debug.Log(points);
            //scoreText.text = text;
            //points.ToString(text + points);
            text = "Score: " + points;
        }
        return;

    }

}
