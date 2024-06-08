using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitSpawnerManager : Singleton<UnitSpawnerManager>
{
    [SerializeField] private List<GameObject> housePrefabs;
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private GameObject tornadoPrefab;
    [SerializeField] private GameObject lightningPrefab;
    [SerializeField] private int numberOfTurnsBeforeHazards = 5;

    private int turnCount = 0;
    private float hazardSpawnChance = 0.2f;
    private float tornadoRate = 0.1f;
    private float lightningRate = 0.05f;
    private UnitSlot[,] unitSlots;

    void OnEnable()
    {
        UnitManager.Instance.onEndMove += OnNewTurn;
    }

    void OnDisable()
    {
        
    }

    void Start()
    {
        // Initialize unitSlots or get them from another manager

    }

    public void OnNewTurn()
    {
        turnCount++;
        SpawnHouses();
        TrySpawnHazard();
    }

    private void SpawnHouses()
    {
        int houseCount = Random.Range(1, 3);
        for (int i = 0; i < houseCount; i++)
        {
            UnitSlot emptySlot = GetRandomEmptySlot();
            if (emptySlot != null)
            {
                int houseIndex = Random.Range(0, housePrefabs.Count);
                GameObject newHouse = Instantiate(housePrefabs[houseIndex], emptySlot.transform.position, Quaternion.identity);
                newHouse.transform.SetParent(emptySlot.transform);
                newHouse.transform.localPosition = Vector3.zero;
                emptySlot.Unit = newHouse.GetComponent<Unit>();
            }
        }
    }

    private void TrySpawnHazard()
    {
        if(turnCount < numberOfTurnsBeforeHazards)
            return;

        float chance = Random.value;
        if (chance < hazardSpawnChance)
        {
            UnitSlot emptySlot = GetRandomEmptySlot();
            if (emptySlot != null)
            {
                float hazardTypeChance = Random.value;
                GameObject hazardPrefab = null;

                if (hazardTypeChance < 0.85f)
                    hazardPrefab = firePrefab;
                else if (hazardTypeChance < 0.85f + tornadoRate)
                    hazardPrefab = tornadoPrefab;
                else
                    hazardPrefab = lightningPrefab;

                GameObject newHazard = Instantiate(hazardPrefab, emptySlot.transform.position, Quaternion.identity);
                newHazard.transform.SetParent(emptySlot.transform);
                newHazard.transform.localPosition = Vector3.zero;
                emptySlot.Unit = newHazard.GetComponent<Unit>();

                // Increase difficulty for future turns
                IncreaseHazardDifficulty();
            }
        }
    }

    private UnitSlot GetRandomEmptySlot()
    {
        List<UnitSlot> emptySlots = new List<UnitSlot>();
        foreach (UnitSlot slot in unitSlots)
        {
            if (slot.Unit == null)
                emptySlots.Add(slot);
        }

        if (emptySlots.Count == 0) return null;

        return emptySlots[Random.Range(0, emptySlots.Count)];
    }

    private void IncreaseHazardDifficulty()
    {
        hazardSpawnChance += 0.02f; // Increase hazard spawn chance each turn
        tornadoRate += 0.01f; // Increase tornado rate each turn
        lightningRate += 0.005f; // Increase lightning rate each turn
    }
}