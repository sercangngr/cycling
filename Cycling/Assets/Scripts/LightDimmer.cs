using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDimmer : MonoBehaviour {

   

    void OnEnable()
    {
        EventManager.enterRain.AddListener(EnterRain);
        EventManager.exitRain.AddListener(ExitRain);
    }

    void OnDisable()
    {
        EventManager.enterRain.RemoveListener(EnterRain);
        EventManager.exitRain.RemoveListener(ExitRain);
    }


    void EnterRain() 
    {
        GetComponent<Light>().enabled = false;
    }

    void ExitRain() 
    {
        GetComponent<Light>().enabled = true;
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
