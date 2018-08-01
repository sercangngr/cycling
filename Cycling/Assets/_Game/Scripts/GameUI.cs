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
	public GameObject itemFeedbackPrefab;

    public Image energyBar;
    public Image distanceBar;

    public Text timeText;
	public Text scoreText;

	void OnNewNotification(CollectableItem item)
    {
        if(notifications.Count == 3)
        {
            Destroy(notifications[0]);
            notifications.RemoveAt(0);
        }
        
        GameObject not = Instantiate(notificationPrefab, notificationContainer);
		not.GetComponent<Notification>().Setup(item.notificationName, item.notificationIcon);
		not.GetComponentInChildren<Text>().text = item.notificationName;
        notifications.Add(not);
        StartCoroutine(RemoveNotification(not));

		if(item.score > 0 || item.energy > 0 || item.time > 0)
		{
			Instantiate(itemFeedbackPrefab).GetComponent<ItemFeedback>().Setup(item);
        }

    }

    public IEnumerator RemoveNotification(GameObject not)
    {
        yield return new WaitForSeconds(3.5f);
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
		yield return null;

		float initTime = playerState.timeLeft;
		float initEnergy = playerState.energyLeft;
		float initDistance = Marks.Instance.GetDistance(playerState.position);

        while(this != null && enabled)
        {
			float tEnergy = playerState.energyLeft / initEnergy;
			float tDistance = Marks.Instance.GetDistance(playerState.position) / initDistance;


            energyBar.fillAmount = tEnergy;


            distanceBar.fillAmount = 1 - tDistance;

			int minute = (int)(playerState.timeLeft / 60);
			minute = Mathf.Max(0, minute);
			int seconds = (int)(playerState.timeLeft - minute * 60);
			seconds = Mathf.Max(0, seconds);

			timeText.text = FillDigits2(minute) + ":" + FillDigits2(seconds);
			scoreText.text = "" + playerState.score;

            yield return null;
        }

    }

    void OnGameOver()
    {
        Destroy(gameObject);
        Instantiate(gameOverPrefab);
    }
 

	public string FillDigits2(int number)
	{
		int n = number / 10;
        if(n < 1)
		{
			return "0" + number;
		}
		return "" + number;
	}
}
