using UnityEngine;

public class LookAtCam : MonoBehaviour
{
    private Transform cam;

    private void Start()
    {
        cam = Camera.main.transform;
    }

    void Update()
    {
        transform.forward = transform.position - cam.position;
    }
}
