using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawnerManager : Singleton<UnitSpawnerManager>
{
    [SerializeField] public List<GameObject> housePrefabs;
    [SerializeField] public GameObject firePrefab;
    [SerializeField] public GameObject tornadoPrefab;
    [SerializeField] public GameObject lightningPrefab;
    [SerializeField] public GameObject rockPrefab;
    [SerializeField] private int numberOfTurnsBeforeHazards = 5;

    private int turnCount = 0;
    private float hazardSpawnChance = 0.2f;
    private float fireRate = 0.45f;
    private float tornadoRate = 0.35f;
    private float lightningRate = 0.2f;
    private UnitSlot[,] unitSlots;

    void OnEnable()
    {
        UnitManager.onEndMove += OnNewTurn;
    }

    void OnDisable()
    {
        UnitManager.onEndMove -= OnNewTurn;
    }

    void Start()
    {
        // Initialize unitSlots or get them from another manager
        unitSlots = UnitManager.Instance.unitSlots;
    }

    public void OnNewTurn()
    {
        turnCount++;
        SpawnHouses();
        TrySpawnHazard();
    }

    private void SpawnHouses()
    {
        // int houseCount = Random.Range(1, 3);
        // for (int i = 0; i < houseCount; i++)
        // {

        float randomNum = Random.Range(0f, 1f);
        if(randomNum > 0.7f)
            return;

        UnitSlot emptySlot = GetRandomEmptySlotForHouses();
        if (emptySlot != null)
        {
            int houseIndex = Random.Range(0, housePrefabs.Count);
            GameObject newHouse = Instantiate(housePrefabs[houseIndex], emptySlot.transform.position, Quaternion.identity);
            newHouse.transform.SetParent(emptySlot.transform);
            newHouse.transform.localPosition = Vector3.zero;
            emptySlot.Unit = newHouse.GetComponent<Unit>();
        }
        // }
        //AudioManager.PlaySFX("SpawnSFX");
    }

    private void TrySpawnHazard()
    {
        if(turnCount < numberOfTurnsBeforeHazards)
            return;

        float chance = Random.value;
        float total = fireRate + tornadoRate + lightningRate;

        if (chance < hazardSpawnChance)
        {
            UnitSlot emptySlot = GetRandomEmptySlotForHazards();
            if (emptySlot != null)
            {
                float hazardTypeChance = Random.value;
                GameObject hazardPrefab = null;

                // if (hazardTypeChance < 0.85f)
                //     hazardPrefab = firePrefab;
                // else if (hazardTypeChance < 0.85f + tornadoRate)
                //     hazardPrefab = tornadoPrefab;
                // else
                //     hazardPrefab = lightningPrefab;

                if(fireRate / total > chance)
                    hazardPrefab = firePrefab;
                else if((tornadoRate / total) + fireRate/total > chance && turnCount > 8)
                    hazardPrefab = tornadoPrefab;
                else if((lightningRate / total) + (fireRate + tornadoRate)/total >= chance && turnCount > 15)
                    hazardPrefab = lightningPrefab;

                GameObject newHazard = Instantiate(hazardPrefab, emptySlot.transform.position, Quaternion.identity);
                newHazard.transform.SetParent(emptySlot.transform);
                newHazard.transform.localPosition = Vector3.zero;
                emptySlot.UnitHazard = newHazard.GetComponent<UnitHazard>();
            }
        }

        IncreaseHazardDifficulty();
    }

    private UnitSlot GetRandomEmptySlotForHazards()
    {
        List<UnitSlot> emptySlots = new List<UnitSlot>();
        foreach (UnitSlot slot in unitSlots)
        {
            if (slot.UnitHazard == null && (slot.Unit == null || slot.Unit.UnitType != UnitType.Rock))
                emptySlots.Add(slot);
        }

        if (emptySlots.Count == 0) return null;

        return emptySlots[Random.Range(0, emptySlots.Count)];
    }

    private UnitSlot GetRandomEmptySlotForHouses()
    {
        List<UnitSlot> emptySlots = new List<UnitSlot>();
        foreach (UnitSlot slot in unitSlots)
        {
            if (slot.Unit == null && slot.UnitHazard == null)
                emptySlots.Add(slot);
        }

        if (emptySlots.Count == 0) return null;

        return emptySlots[Random.Range(0, emptySlots.Count)];
    }

    private void IncreaseHazardDifficulty()
    {
        Debug.Log("Turn: " + turnCount);
        if(turnCount % 3 == 0 || turnCount % 4 == 0 || turnCount % 5 == 0)
        {
            hazardSpawnChance += 0.08f;
            hazardSpawnChance *= 0.9f;
            Debug.Log(hazardSpawnChance);
        }
        fireRate -= 0.001f;
        tornadoRate += 0.0005f;
        lightningRate += 0.005f;
    }
}