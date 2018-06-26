using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.IO.Ports;
using Kabuk;

[UnitySingleton(UnitySingletonAttribute.Type.ExistsInScene)]
public class Ardunio : UnitySingleton<Ardunio> 
{
    public class EventButtonLeft : CustomEvent<EventButtonLeft> { }
    public class EventButtonRight : CustomEvent<EventButtonRight> { }
    public class EventButtonCheck : CustomEvent<EventButtonCheck> { }
    public class EventButtonCross : CustomEvent<EventButtonCross> { }

	SerialPortController serialPortController;
	bool gameStarted = false;

	private void Start()
	{
		serialPortController = new SerialPortController("COM6", 57600);
		serialPortController.Launch();
		Debug.Log("Open Port!");
	}

	private void OnDestroy()
	{
		serialPortController.Stop();
		Debug.Log("Close Port");
	}


	void Update()
    {
        FakeInput();
        ReadValues();
    }


    void FakeInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            EventButtonLeft.Fire();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            EventButtonRight.Fire();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            EventButtonCheck.Fire();
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            EventButtonCross.Fire();
            if (gameStarted)
            {
				EventManager.Restart ();
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
        }
    }


    public void ReadValues()
    {
		string rawData = "";

		while(serialPortController.GetReceivedString(out rawData))
		{
			Debug.Log("Raw Data: " + rawData);
		}
    }


    private void OnEnable()
    {
        GameState.EventStartGame.Register(OnStartGame);
    }

    private void OnDisable()
    {
        GameState.EventStartGame.Unregister(OnStartGame);
    }

	void OnStartGame(PlayerState playerState)
    {
        gameStarted = true;
    }


}
