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

    SerialPort port;
    bool gameStarted = false;

	KeyCode[] speedInputs = {  KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I };


	KeyCode[] rotationInputs = {
		KeyCode.Alpha1,
		KeyCode.Alpha2,
		KeyCode.Alpha3,
		KeyCode.Alpha4,
		KeyCode.Alpha5,
		KeyCode.Alpha6,
		KeyCode.Alpha7,
		KeyCode.Alpha8,
		KeyCode.Alpha9
	};

   

    [Header("Angle")]
    public float nextAngle = 90;
    public float angle = 90;
    public float interTimerAngle = 0;

    public float[] angleMap =
    {
        10,30,50,70,90,110,130,150,170

    };

    [Header("Speed")]
    public float speed = 0;
    public float nextSpeed = 0;
    public float interTimerSpeed = 0;
    public float drag = 0.05f;

    public float[] speedMap =
    {
        0.1f,0.2f,0.3f,0.4f,0.5f,0.6f,0.7f,0.8f,0.9f

    };


	IEnumerator Start() 
	{
		Cursor.visible = false;
		port = new SerialPort("COM6", 57600);
		port.WriteTimeout = 100;

		try
		{
			port.Open();
		}
		catch (Exception e)
		{
			Debug.Log("Error at port oppening" + e);
		}

		yield return new WaitForSeconds(1f);
		port.WriteLine("1");
	}

    void Update()
    {
        FakeInput();
        ReadValues();
    }


    const float firstNew = 90;
    float correctionValue = 0;
    float lastValue = 90;
    bool recalibrate = false;


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

		float newSpeed = -1;
		float speedInterval = 1.0f / speedInputs.Length;
		float newAngle = -1;
		float rotationInterval = 180.0f / rotationInputs.Length;
      
		for (int i = 0; i < speedInputs.Length; i++) {

			if(Input.GetKeyDown(speedInputs[i]))
			{
                newSpeed = speedMap[i];
                //break;
			}
		}

		for (int i = 0; i < rotationInputs.Length; i++) {

			if(Input.GetKeyDown(rotationInputs[i]))
			{
                newAngle = angleMap[i];
				break;
			}
		}

        if (newAngle > 0 && Math.Abs(newAngle - nextAngle) > Mathf.Epsilon) 
		{
            nextAngle = newAngle;
			interTimerAngle = 0;
		}
		if (newSpeed > 0)
		{
			nextSpeed = newSpeed;
			interTimerSpeed = 0;
		}

		interTimerAngle = Mathf.Clamp01(interTimerAngle + Time.deltaTime * 10);
		interTimerSpeed = Mathf.Clamp01(interTimerSpeed + Time.deltaTime * 3);

        if(newSpeed < 0 && drag > 0)
        {
			speed = Mathf.Clamp01(speed - drag * Time.deltaTime);
			Debug.Log ("Drag");
        }else
        {
            speed = Mathf.Lerp(speed,nextSpeed,interTimerSpeed);
        }

		angle = Mathf.Lerp(angle,nextAngle,interTimerAngle);

    }

    public bool Ready() 
    {
        //return port.IsOpen;
		return true;
    }


    private void OnEnable()
    {
        //TODO uncomment
        GameState.EventStartGame.Register(OnStartGame);
    }

    private void OnDisable()
    {
        port.Close();
        //TODO uncomment
        GameState.EventStartGame.Unregister(OnStartGame);
    }

    void OnStartGame()
    {
        gameStarted = true;
    }


}
