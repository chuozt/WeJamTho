using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitHazard : Unit
{
    [SerializeField] public HazardType hazardType;
    [SerializeField] private int preSpawnMovesLeft = 1;
    [SerializeField] public int movesLeft = 2;
    SpriteRenderer sr;
    public bool isGhostMode = true;
    public Vector2Int initialDirection = new Vector2Int(0,1);

    public static event Action onHazardUpdated;

    public override void Awake()
    {
        base.Awake();
        unitType = UnitType.Hazard;
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
    }

    void OnEnable()
    {
        UnitManager.onEndMove += HazardBehaviorUpdate;
    }

    void OnDisable()
    {
        UnitManager.onEndMove -= HazardBehaviorUpdate;
    }

    void HazardBehaviorUpdate()
    {
        if(preSpawnMovesLeft > 0)
        {
            preSpawnMovesLeft--;
            if(preSpawnMovesLeft == 0)
            {
                isGhostMode = false;
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
            }
            onHazardUpdated?.Invoke();
            return;
        }
        
        if(movesLeft > 1)
            movesLeft--;
        else
            Destroy(gameObject, 0.2f);
        
        onHazardUpdated?.Invoke();
    }

    public void CheckHazard()
    {
        switch(hazardType)
        {
            case HazardType.Fire:
                
                break;
            case HazardType.Tornado:

                break;
            case HazardType.Lightning:

                break;
        }
    }
}

public enum HazardType
{
    Fire,
    Tornado,
    Lightning
}
