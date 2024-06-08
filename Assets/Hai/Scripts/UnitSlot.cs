using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSlot : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private UnitHazard hazard;
    public bool IsSpecialGrid { get; set; } = false;
    public Unit Unit {get{ return unit; } set{ unit = value; }}

    void Awake()
    {
        unit = GetComponentInChildren<Unit>();
    }
}
