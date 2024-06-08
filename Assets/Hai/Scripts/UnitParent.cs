using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UnitParent : MonoBehaviour
{
    private Animator anim;
    [SerializeField] protected UnitType unitType;
    public UnitType UnitType => unitType;

    public virtual void Awake()
    {
        anim = GetComponent<Animator>();
    }

    protected void OnUnitDestroy()
    {
        anim.SetTrigger("Disappear");
    }
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
