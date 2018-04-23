using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    
    private string comPort = "COM5";
    SerialPort ardunioPort;

    float speed = 0;
    float angle = 90;

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("Ardunio Controller");
        ardunioPort = new SerialPort(comPort, 57600); // port define.

        try
        {
            Debug.Log("serial begin.");
            ardunioPort.Open();
            ardunioPort.ReadTimeout = 20; //set this variable with arduino 1 loop time.
        }
        catch (Exception e)
        {
            Debug.LogError("Arduino connection is not set due to following error:");
            Debug.LogError(e);
            //enabled = false;
        }
    }



	void Update()
    {
        if(ardunioPort.IsOpen)
        {
			ReadValues();
        }
        FakeInput();
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
        }
    }

    public void ReadValues()
    {

        string[] values = ardunioPort.ReadLine().Split(',');   //recevied data looks like this: 150,20
        float rawSpeed = -1;
        float rawAngle = -1;
        float.TryParse(values[0], out rawSpeed);
        float.TryParse(values[1], out rawAngle);


        if(rawAngle > 0 && rawSpeed > 0)
        {
            angle = rawAngle;
            speed = rawSpeed;
        }else
        {
            Debug.LogError("Ardunio error on parsing data" + values);
        }

    }



    public void reCalibSteering()               //you can use this function for recalibrate angle sensor. resets middle point of steering to 90.
    {                                           //after using this function wait for 2 seconds for restarting arduino.
        //sArduino.Close();
        //sArduino.Open();
        //sArduino.ReadTimeout = 20;              //set this variable with arduino 1 loop time.

    }

    private void OnDisable()
    {
        ardunioPort.Close();
    }


}
