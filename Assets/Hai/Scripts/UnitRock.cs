using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRock : UnitParent
{
    public override void Awake()
    {
        base.Awake();
        unitType = UnitType.Rock;
    }

    void Update()
    {
        
    }
}
