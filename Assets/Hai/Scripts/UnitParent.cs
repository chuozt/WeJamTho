using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitParent : MonoBehaviour
{
    [SerializeField] protected UnitType unitType;
    public UnitType UnitType => unitType;
    [SerializeField] protected HouseLevel houseLevel;
    public HouseLevel HouseLevel => houseLevel;
}

public enum UnitType
{
    House,
    Hazard,
    Rock
}

public enum HouseLevel
{
    Lv1,
    Lv2,
    Lv3,
    Lv4,
    Main
}
