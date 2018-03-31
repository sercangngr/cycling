using System.Collections.Generic;
using UnityEngine;

public class InGamePanel : MonoBehaviour
{
    [SerializeField]
    private Transform inventoryPanel;

    private List<GameObject> items = new List<GameObject>();

    private void Awake()
    {
        EventManager.powerUpAddedToInventory.AddListener(AddItem);
        EventManager.inventoryCleared.AddListener(UpdateInventory);
    }

    private void UpdateInventory()
    {
        for (int i = 0; i < items.Count; i++)
            Destroy(items[i].gameObject);

        items.Clear();

        for (int i = 0; i < Inventory.PowerUps.Count; i++)
            AddItem(Inventory.PowerUps[i]);
    }

    private void AddItem(PowerUp pu)
    {
        ItemPanel ip = ResourceManager.GetItemPanel();
        ip.Set(pu);
        ip.transform.SetParent(inventoryPanel, false);
        ip.transform.SetAsFirstSibling();
    }
}
