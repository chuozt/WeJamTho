using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSlot : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private UnitHazard unitHazard;

    public bool IsSpecialGrid { get; set; } = false;
    public Unit Unit { get{ return unit; } set{ unit = value; }}
    public UnitHazard UnitHazard { get{ return unitHazard; } set{ unitHazard = value; }}

    void Awake()
    {
        unit = GetComponentInChildren<UnitHouse>();
        if(unit == null)
            unit = GetComponentInChildren<UnitRock>();
            
        unitHazard = GetComponentInChildren<UnitHazard>();
    }
}
