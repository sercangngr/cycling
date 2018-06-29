using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.IO.Ports;
using Kabuk;

[UnitySingleton(UnitySingletonAttribute.Type.CreateOnNewGameObject)]
public class Ardunio : UnitySingleton<Ardunio> 
{
    public class EventButtonLeft : CustomEvent<EventButtonLeft> { }
    public class EventButtonRight : CustomEvent<EventButtonRight> { }
    public class EventButtonCheck : CustomEvent<EventButtonCheck> { }
    public class EventButtonCross : CustomEvent<EventButtonCross> { }

	public class EventArdunioInput: CustomEvent<EventArdunioInput, AInput>{}

	public class AInput
	{
		public const float LeftMostAngle = 180;
		public const float RightMostAngle = 0;

		public const float LowestSpeed = 13;
		public const float HighestSpeed = 900;

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

	protected override void OnAwake()
	{
		serialPortController = new SerialPortController("COM7", 57600);
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
            string[] tokens = rawData.Split(',');
			float rawRotation = float.Parse(tokens[0]);
			float rawSpeed = float.Parse(tokens[1]);


            float normalizedRotation = (rawRotation - AInput.LeftMostAngle) / (AInput.RightMostAngle - AInput.LeftMostAngle);
			float normalizedSpeed = (rawSpeed - AInput.LowestSpeed) / (AInput.HighestSpeed - AInput.LowestSpeed);

            Debug.Log("Rawpeed" + rawSpeed + "normalizedSpeed: " + normalizedSpeed);



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
