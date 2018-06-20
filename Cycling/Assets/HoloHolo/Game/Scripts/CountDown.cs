using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CountDown : MonoBehaviour 
{
	public Text counterText;
	public GameObject gameUIPrefab;


	public int counter;
	float timer;

	private void Awake()
	{
		counterText.text = counter + "";
		timer = counter;
	}

	private void Update()
	{
		timer -= Time.deltaTime;
		counter = (int)timer;
		counterText.text = counter + "";

		if(timer <= 0)
		{
			Destroy(gameObject);
			Instantiate(gameUIPrefab);
			GameState.Instance.StartGame();
		}
	}


}
