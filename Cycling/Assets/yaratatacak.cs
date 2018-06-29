using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yaratatacak : MonoBehaviour {
    public float hiz;
    public GameObject donen;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        
        donen.transform.Rotate(Vector3.up*Time.deltaTime*hiz);
        
    }
}
