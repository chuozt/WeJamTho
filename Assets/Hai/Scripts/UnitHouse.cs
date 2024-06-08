using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHouse : UnitParent
{
    [SerializeField] HouseLevel houseLevel;
    bool isLevelMax = false;
    public HouseLevel HouseLevel => houseLevel;
    public bool IsLevelMax => isLevelMax;

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
