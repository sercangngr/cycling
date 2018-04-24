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

    public Text dataText;
    
   // private string comPort = "COM10";
    public SerialPort bicyclePort;

    public float speed = 0;
    public float angle = 90;

    //public SerialPortController serialPortController;

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("Ardunio Controller");
        bicyclePort = new SerialPort("COM10", 57600); // port define.

        try
        {
            Debug.Log("serial begin.");
            bicyclePort.Open();
            bicyclePort.ReadTimeout = 50; //set this variable with arduino 1 loop time.
        }
        catch (Exception e)
        {
            Debug.LogError("Arduino connection is not set due to following error:");
            Debug.LogError(e);
            //enabled = false;
        }

        //serialPortController = new SerialPortController(comPort, 57600);
        //Debug.Log("SerialPOrt Launch");
        //serialPortController.Launch();
    }



    void Update()
    {
        //     if(serialPortController.IsInitialized && !serialPortController.ConnectionClosed)
        //     {
        //ReadValues();
        //     }
        //     else
        //     {
        //         Debug.Log("failed to read values");
        //     }
        if (bicyclePort.IsOpen)
        {
            ReadValues();
        }
        FakeInput();
    }

    bool isStart = false;

    void FakeInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            EventButtonLeft.Fire();
            Debug.Log("Left");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            EventButtonRight.Fire();
            Debug.Log("Right");
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            EventButtonCheck.Fire();

        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            EventButtonCross.Fire();
            if (isStart)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
        }
    }
    int counter = 0;
    public void ReadValues()
    {

        //string data = "";
        //Debug.Log("reading values");
        //while (serialPortController.GetReceivedString(out data))
        //{

        //    Debug.Log("Data1: " + data);
        //    dataText.text = data + "," + counter;
        //    counter = ++counter%100;
        //    string[] values = data.Split(',');   //recevied data looks like this: 150,20
        //    float rawSpeed = -1;
        //    float rawAngle = -1;
        //    bool valid = true;
        //    valid = valid && float.TryParse(values[0], out rawSpeed);
        //    valid = valid && float.TryParse(values[1], out rawAngle);


        //    if (valid)
        //    {
        //        angle = rawAngle;
        //        //angle = 127;
        //        speed = rawSpeed;
        //    }
        //    else
        //    {
        //        Debug.LogWarning("Ardunio error on parsing data" + values);
        //    }

        //}


        try
        {
            string data = bicyclePort.ReadLine();
            Debug.Log("Data Speed,Angle: " + data);

            dataText.text = data + "," + counter;
            counter = ++counter % 100;

            string[] values = data.Split(',');   //recevied data looks like this: 150,20
            float rawSpeed = -1;
            float rawAngle = -1;
            bool valid = true;
            valid = valid && float.TryParse(values[0], out rawSpeed);
            valid = valid && float.TryParse(values[1], out rawAngle);

            Debug.Log("Raw" + rawSpeed + "," + rawAngle);

            if (valid)
            {
                angle = rawAngle;
                speed = rawSpeed;
            }
            else
            {
                Debug.LogWarning("Ardunio error on parsing data" + values);
            }
        }
        catch (Exception e)
        {
            dataText.text = "Exp" + e.Message;
            //Debug.LogWarning("TimeOut");
            //enabled = false;
        }


    }



    //public void ReCalibSteering()               //you can use this function for recalibrate angle sensor. resets middle point of steering to 90.
    //{                                           //after using this function wait for 2 seconds for restarting arduino.
    //    bicyclePort.Close();
    //    bicyclePort = new SerialPort(comPort, 57600); // port define.
    //    bicyclePort.Open();
    //    bicyclePort.ReadTimeout = 30;              //set this variable with arduino 1 loop time.

    //}


    private void OnEnable()
    {
        GameState.EventStartGame.Register(OnStartGame);
    }

    private void OnDisable()
    {
        bicyclePort.Close();
        //serialPortController.Stop();
        Debug.Log("Stop Serial Port");
       
        GameState.EventStartGame.Unregister(OnStartGame);
    }

    void OnStartGame()
    {
        isStart = true;
    }


}
