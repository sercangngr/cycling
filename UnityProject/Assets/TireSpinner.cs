using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireSpinner : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start ()
    {

        while (enabled)
        {
            //Quaternion rot = Quaternion.AngleAxis(Controller.Instance.speed * Time.deltaTime * 100, Vector3.left);
            Quaternion rot = new Quaternion();
            transform.Rotate(new Vector3(0, -100 * Time.deltaTime * Controller.Instance.speed, 0));
            //transform.localRotation = rot * transform.rotation;
            yield return null;
               
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
