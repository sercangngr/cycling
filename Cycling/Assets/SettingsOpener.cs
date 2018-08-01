using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kabuk;


public class SettingsOpener : MonoBehaviour 
{
	public GameObject settings;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Instantiate(settings);
        }
    }

	
}
