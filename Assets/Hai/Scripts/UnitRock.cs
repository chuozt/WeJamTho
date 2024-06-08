using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRock : Unit
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
