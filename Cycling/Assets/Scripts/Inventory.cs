using System.Collections.Generic;
using UnityEngine;

public static class Inventory
{
    private static List<PowerUp> powerUps = new List<PowerUp>();
    public static List<PowerUp> PowerUps
    {
        get
        {
            if (!inited)
                Init();

            return powerUps;
        }
        private set
        {
            if (!inited)
                Init();

            powerUps = value;
        }
    }

    private static bool inited = false;

    private static void Init()
    {
        Debug.Log("Inventory inited");
        EventManager.powerUpPickedUp.AddListener(Add);
        EventManager.restartRace.AddListener((val) => Clear());
        inited = true;
    }

    public static void Add(PowerUp pu)
    {
        PowerUps.Add(pu);
        EventManager.powerUpAddedToInventory.Invoke(pu);
        Debug.Log("Got " + pu.itemName + " (" + pu.function + ")");
    }

    public static void Remove(PowerUp pu)
    {
        if (!PowerUps.Contains(pu))
        {
            //Debug.LogError(pu.itemName + " is not in the inventory!");
            return;
        }
        PowerUps.Remove(pu);
        EventManager.powerUpRemovedFromInventory.Invoke(pu);
        Debug.Log(pu.itemName + " removed");
    }

    public static void Clear()
    {
        for (int i = 0; i < PowerUps.Count; i++)
            Remove(PowerUps[i]);

        EventManager.inventoryCleared.Invoke();
    }
}
