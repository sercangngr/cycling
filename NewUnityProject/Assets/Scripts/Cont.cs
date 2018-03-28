using UnityEngine;
using System.IO.Ports;


public class Cont : MonoBehaviour
{
    float speed = 0;
    float angle = 90;

    SerialPort sArduino = new SerialPort("COM3", 57600); // port define.

    void Start()
    {
        Debug.Log("serial begin.");
        sArduino.Open();
        sArduino.ReadTimeout = 20; //set this variable with arduino 1 loop time.
    }

    void Update()
    {

        readValues(out speed, out angle);
        Debug.Log("speed = " + speed + ", angle = " + angle);        

    }

    public void readValues(out float speed, out float angle)
    {
        if (sArduino.IsOpen)
        {
            string[] values = sArduino.ReadLine().Split(',');   //recevied data looks like this: 150,20
            speed = (float.Parse(values[0]));            
            angle = (float.Parse(values[1]));                   //angle value changes between 0 to 180, when communication starts this value=90.
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
}
