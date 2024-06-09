using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScoreManager : Singleton<ScoreManager>
{

    int points = 0;
    [SerializeField] public TMP_Text scoreText;


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
    }

    void AddScore()
    {
        scoreText.text = (int.Parse(scoreText.text) + UnitManager.Instance.UpdateScore()).ToString();
    }
}
