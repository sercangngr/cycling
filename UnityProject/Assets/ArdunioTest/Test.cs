using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using UnityEngine.EventSystems;


public class Test : MonoBehaviour {


    SerialPortController portController;
    SerialPort port;


	KeyCode[] speedInputs = { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I };
	KeyCode[] rotationInputs = {
		KeyCode.Alpha1,
		KeyCode.Alpha2,
		KeyCode.Alpha3,
		KeyCode.Alpha4,
		KeyCode.Alpha5,
		KeyCode.Alpha6,
		KeyCode.Alpha7,
		KeyCode.Alpha8
	};



    public GameObject cubePrefab;

    public int gpuCounter = 0;
    public int cpuCounter = 0;

    void Awake() 
    {
        //portController = new SerialPortController("COM10", 57600);
        //portController.Launch();


        
      
    }

    IEnumerator Start() 
    {
        port = new SerialPort("COM6", 57600);
        port.Open();
        

        yield return new WaitForSeconds(1f);
        port.WriteLine("1");
    }

    void OnDisable()
	{
        //portController.Stop();
        port.Close();
    }


    void OnGUI() 
    {
		if (Event.current.isKey)
		{
			Debug.Log(Event.current.keyCode);
		}
    }






	// Update is called once per frame
	void Update () 
    {

		for (int i = 0; i < speedInputs.Length; i++) {
		
			if(Input.GetKeyDown(speedInputs[i]))
				{
					Debug.Log("Speed:" + speedInputs[i]);
				}
		}

		for (int i = 0; i < rotationInputs.Length; i++) {

			if(Input.GetKeyDown(rotationInputs[i]))
			{
				Debug.Log("Rotation:" + rotationInputs[i]);
			}
		}

        //ReadWT();
        ////Read();


        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    //StartCoroutine(Work());
        //    GenerateCube();
        //    gpuCounter++;
        //}

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    StartCoroutine(Work());
        //    cpuCounter++;
        //}
		
	}

    void ReadWT() 
    {
        if (portController.IsInitialized && !portController.ConnectionClosed)
        {
            string data = "";
            while (portController.GetReceivedString(out data))
            {
                Debug.Log("Data:" + data);
            }
        }
    }

    void Read()
    {
        if (port.IsOpen)
        {
            try
            {
                string data = port.ReadLine();
                Debug.Log(data);
            }
            catch (Exception e)
            {
                Debug.Log("E" + e);
            }
        }

    }

    IEnumerator Work() 
    {
        while (enabled)
        {
            int counter = 0;
            for (int i = 0; i < 5000; i++)
            {
                counter++;
                Physics.Raycast(new Ray(transform.position, Vector3.up));
            }
            yield return null;
        }
    }


    void GenerateCube() 
    {

        for (int i = 0; i < 100; i++)
        {
            GameObject cube = Instantiate(cubePrefab);
            cube.GetComponent<MeshRenderer>().material.color = Color.red;
        }
       
    }

}
