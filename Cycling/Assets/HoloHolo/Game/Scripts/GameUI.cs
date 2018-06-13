using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameUI : MonoBehaviour 
{

    public GameObject notificationPrefab;
    public RectTransform notificationContainer;
    List<GameObject> notifications = new List<GameObject>();

    public GameObject gameOverPrefab;


    public Image timeBar;
    public Image energyBar;
    public Image distanceBar;

    public Text timeText;
    public Text energyText;
    public Text scoreText;

	void OnNewNotification(CollectableItem item)
    {
        if(notifications.Count == 3)
        {
            Destroy(notifications[0]);
            notifications.RemoveAt(0);
        }
        
        GameObject not = Instantiate(notificationPrefab, notificationContainer);
		not.GetComponentInChildren<Text>().text = item.notificationName;
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


	IEnumerator OnGameStarted(PlayerState playerState)
    {
		float initTime = playerState.timeLeft;
		float initEnergy = playerState.energyLeft;
		float initDistance = playerState.distanceLeft;

        while(this != null && enabled)
        {
			float tTime = playerState.timeLeft / initTime;
			float tEnergy = playerState.energyLeft / initEnergy;
			float tDistance = playerState.distanceLeft / initDistance;

            timeBar.fillAmount = tTime;
            energyBar.fillAmount = tEnergy;

           // Debug.Log("Distance" + tDistance);
            distanceBar.fillAmount = 1 - tDistance;

			timeText.text = "Kalan Süre " + ((int)playerState.timeLeft);
			energyText.text = "Kalan Enerji " + ((int)playerState.energyLeft);
			scoreText.text = "Skor\n" + playerState.score;

            yield return null;
        }

    }

    void OnGameOver()
    {
        Destroy(gameObject);
        Instantiate(gameOverPrefab);
    }
}
