using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHazard : UnitParent
{
    [SerializeField] private HazardType hazardType;
    [SerializeField] private int preSpawnMovesLeft = 1;
    [SerializeField] private int movesLeft = 2;

    public override void Awake()
    {
        base.Awake();
        unitType = UnitType.Hazard;
    }

    public void CheckHazard()
    {
        if(preSpawnMovesLeft > 0)
        {
            preSpawnMovesLeft--;
            return;
        }
        
        if(movesLeft > 0)
            movesLeft--;
        else
            Destroy(gameObject, 0.5f);

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
