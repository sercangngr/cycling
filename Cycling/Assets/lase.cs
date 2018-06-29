
using UnityEngine;
using System.Collections;

public class lase : MonoBehaviour
{
    public GameObject yarat;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        Instantiate(yarat, this.transform.position, this.transform.rotation);
    }
}