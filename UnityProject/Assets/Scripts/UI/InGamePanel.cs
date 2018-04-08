using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGamePanel : MonoBehaviour
{
    [SerializeField]
    private RaceStatus raceStatus;
    [SerializeField]
    private PlayerStatus playerStatus;
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
        playerName.text = playerStatus.playerName;
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
        distance.value = 1 - (Vector3.SqrMagnitude(playerStatus.playerPos - raceStatus.finishPos) / Vector3.SqrMagnitude(raceStatus.finishPos - raceStatus.startPos));
    }

    private void UpdateRaceTime()
    {
        raceTime.text = raceStatus.currentRaceTime.ToString("0.0");
    }

    private void UpdateCountDown()
    {
        if (raceStatus.currentCountDown > 0)
        {
            if (!countDownTime.gameObject.activeSelf)
                countDownTime.gameObject.SetActive(true);

            countDownTime.text = raceStatus.UICountDown;
        }
        else
            countDownTime.gameObject.SetActive(false);
    }

    private void UpdateEnergy()
    {
        playerEnergy.text = playerStatus.currentEnergy.ToString("0.0");
    }

    private void UpdateScore()
    {
        playerScore.text = playerStatus.score.ToString("0.");
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
