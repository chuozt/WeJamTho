using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHouse : Unit
{
    [SerializeField] HouseLevel houseLevel;
    bool isLevelMax = false;
    Vector2Int position;
    public HouseLevel HouseLevel { get{ return houseLevel; } set{ houseLevel = value; } }
    public bool IsLevelMax => isLevelMax;
    public Vector2Int Position => position;

    public override void Awake()
    {
        base.Awake();
        unitType = UnitType.House;
        if(houseLevel == HouseLevel.Lv4)
            isLevelMax = true;
    }

    void Update()
    {
        
    }
}
