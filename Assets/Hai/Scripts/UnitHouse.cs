using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHouse : UnitParent
{
    public override void Awake()
    {
        base.Awake();
        unitType = UnitType.House;
    }

    void Update()
    {
        
    }
}
