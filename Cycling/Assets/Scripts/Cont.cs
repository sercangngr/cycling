using UnityEngine;
using System.IO.Ports;
using System.Xml;
using System.IO;
using System;

public class Cont : MonoBehaviour
{
    float speed = 0;
    float angle = 90;
    private Controller controller;
    private string comPort = "COM5";

    SerialPort sArduino;

    private void Awake()
    {
        Debug.Log("Ardunio Controller");
        sArduino = new SerialPort(comPort, 57600); // port define.
        controller = Controller.Instance;
    }

    void Start()
    {
        try
        {
            Debug.Log("serial begin.");
            sArduino.Open();
            sArduino.ReadTimeout = 20; //set this variable with arduino 1 loop time.
        }
        catch (Exception e)
        {
            Debug.LogError("Arduino connection is not set due to following error:");
            Debug.LogError(e);
            enabled = false;
        }
    }

    void Update()
    {
        readValues(out speed, out angle);
        Debug.Log("speed = " + speed + ", angle = " + angle);
        controller.SetInput(speed, angle);
    }

    public void readValues(out float speed, out float angle)
    {
        if (sArduino.IsOpen)
        {
            string[] values = sArduino.ReadLine().Split(',');   //recevied data looks like this: 150,20
            Debug.Log(values);
            speed = (float.Parse(values[0]));
            speed = speed / 90;
            angle = (float.Parse(values[1]));

            //angle value changes between 0 to 180, when communication starts this value=90.
                                                                // if you want move forward this value must be 90.
        }
        else
        {
            //port not opened. connect arduino or choose right com port.
            Debug.Log("port not opened.");
            speed = 0;
            angle = 90;
        }

    }

    public void reCalibSteering()               //you can use this function for recalibrate angle sensor. resets middle point of steering to 90.
    {                                           //after using this function wait for 2 seconds for restarting arduino.
        sArduino.Close();
        sArduino.Open();
        sArduino.ReadTimeout = 20;              //set this variable with arduino 1 loop time.

    }

    private void GetPort()
    {
        XmlDocument xDoc = new XmlDocument();
        if (File.Exists(Application.dataPath + "/Resources/ArduinoSettings.xml"))
        {
            xDoc.Load(Application.dataPath + "/Resources/ArduinoSettings.xml");
            XmlNode node = xDoc.SelectSingleNode("Settings");
            if (node == null)
                Debug.LogError("No <Settings/> node in " + Application.dataPath + "/Resources/ArduinoSettings.xml file! Default port is set to COM3");
            else
            {
                try
                {
                    comPort = node.Attributes["port"].Value;
                }
                catch (Exception e)
                {
                    Debug.LogError("Could not read the port attribute of the <Settings/> node in the " + Application.dataPath + "/Resources/ArduinoSettings.xml file! Default port is set to COM3");
                    Debug.LogError(e);
                }
            }
        }
        else
            Debug.LogError("No ArduinoSettings.xml in " + Application.dataPath + "/Resources folder! Default port set to COM3");
    }
}
