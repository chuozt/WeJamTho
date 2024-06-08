using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHazard : UnitParent
{
    public override void Awake()
    {
        base.Awake();
        unitType = UnitType.Hazard;
    }
}
