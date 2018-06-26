﻿using System.Collections;
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

	public class EventArdunioInput: CustomEvent<EventArdunioInput, AInput>{}

	public class AInput
	{
		public const float LeftMostAngle = -90;
		public const float RightMostAngle = 90;

		public const float LowestSpeed = 0;
		public const float HighestSpeed = 100;

        // Between -1 and 1
		public float normalizedHandleBarRotation;
        // Between 0 and 1
		public float normalizedSpeed;

		public AInput(float nRotation, float nSpeed)
		{
			normalizedHandleBarRotation = nRotation;
			normalizedSpeed = nSpeed;
		}
	}



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
			float rawRotation = 90;
			float rawSpeed = 90;

			float normalizedRotation = (rawRotation - AInput.LeftMostAngle) / (AInput.RightMostAngle - AInput.LeftMostAngle);
			float normalizedSpeed = (rawSpeed - AInput.LowestSpeed) / (AInput.HighestSpeed - AInput.LowestSpeed);

			EventArdunioInput.Fire(new AInput(normalizedRotation, normalizedSpeed));
            
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


	public bool Working
	{
		get { return serialPortController.IsInitialized && !serialPortController.ConnectionClosed; }
	}
    


}
