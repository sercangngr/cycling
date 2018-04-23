using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameUI : MonoBehaviour 
{

    public GameObject notificationPrefab;
    public RectTransform notificationContainer;
    List<GameObject> notifications = new List<GameObject>();


    public Image timeBar;
    public Image energyBar;
    public Image distanceBar;

    public Text timeText;
    public Text energyText;
    public Text scoreText;

    void OnNewNotification(Notification notification)
    {
        if(notifications.Count == 3)
        {
            Destroy(notifications[0]);
            notifications.RemoveAt(0);
        }

        GameObject not = Instantiate(notificationPrefab, notificationContainer);
        not.GetComponentInChildren<Text>().text = notification.text;
        notifications.Add(not);
        StartCoroutine(RemoveNotification(not));

    }

    public IEnumerator RemoveNotification(GameObject not)
    {
        yield return new WaitForSeconds(1.5f);
        if(notifications.Remove(not))
        {
            Destroy(not);
        }
    }



	private void OnEnable()
	{
        GameState.EventStartGame.RegisterCoroutine(OnGameStarted);
        GameState.EventGameOver.Register(OnGameOver);
        GameState.EventNotify.Register(OnNewNotification);
	}


	private void OnDisable()
	{
        GameState.EventStartGame.UnregisterCoroutine(OnGameStarted);
        GameState.EventGameOver.Unregister(OnGameOver);
        GameState.EventNotify.Unregister(OnNewNotification);
	}


    IEnumerator OnGameStarted()
    {
        float initTime = GameState.Instance.timeLeft;
        float initEnergy = GameState.Instance.energyLeft;
        float initDistance = GameState.Instance.distanceLeft;

        while(enabled)
        {
            float tTime = GameState.Instance.timeLeft / initTime;
            float tEnergy = GameState.Instance.energyLeft / initEnergy;
            float tDistance = GameState.Instance.distanceLeft / initDistance;

            timeBar.fillAmount = tTime;
            energyBar.fillAmount = tEnergy;
            distanceBar.fillAmount = 1 - tDistance;

            timeText.text = "Kalan Süre " + ((int)GameState.Instance.timeLeft);
            energyText.text = "Kalan Enerji" + ((int)GameState.Instance.energyLeft);
            scoreText.text = "Skor\n" + GameState.Instance.score;

            yield return null;
        }

    }

    void OnGameOver()
    {
        Destroy(gameObject);
    }
}
