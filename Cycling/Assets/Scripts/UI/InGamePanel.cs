using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGamePanel : MonoBehaviour
{
    [SerializeField]
    private Transform inventoryPanel;
    [SerializeField]
    private Text raceTime;
    [SerializeField]
    private Text countDownTime;
    [SerializeField]
    private Text playerName;
    [SerializeField]
    private Text playerEnergy;
    [SerializeField]
    private Text playerScore;
    [SerializeField]
    private GameObject shieldEffect;
    [SerializeField]
    private Slider distance;

    private List<GameObject> items = new List<GameObject>();

    private void Awake()
    {
        EventManager.powerUpAddedToInventory.AddListener(AddItem);
        EventManager.inventoryCleared.AddListener(UpdateInventory);
        EventManager.powerUpRemovedFromInventory.AddListener(RemoveItem);
    }

    private void OnEnable()
    {
    }

    private void Update()
    {
        UpdateRaceTime();
        UpdateCountDown();
        UpdateEnergy();
        UpdateScore();
        UpdateDistance();
    }

    private void UpdateDistance()
    {
    }

    private void UpdateRaceTime()
    {
    }

    private void UpdateCountDown()
    {
    }

    private void UpdateEnergy()
    {
    }

    private void UpdateScore()
    {
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

        items.Add(ip.gameObject);

        if (pu is Shield)
            shieldEffect.SetActive(true);
    }

    private void RemoveItem(PowerUp pu)
    {
        if (pu is Shield)
            shieldEffect.SetActive(false);
    }
}
