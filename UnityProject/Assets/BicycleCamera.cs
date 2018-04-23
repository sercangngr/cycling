using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BicycleCamera : MonoBehaviour 
{

    Vector3 hitPos;
    Vector3 forward;
    //Quaternion headTilt = Quaternion.Euler(-Vector3.right * 15);
    public LayerMask mask;


    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -Vector3.up, out hit, 5.0f,mask))
        {
            hitPos = hit.point;
            Vector3 parentForward = transform.parent.forward;
            forward = parentForward - (Vector3.Dot(parentForward, hit.normal) * hit.normal) / hit.normal.sqrMagnitude;
            //forward = headTilt * forward;

            transform.forward =  Vector3.Slerp(transform.forward,forward , 5f * Time.deltaTime);
        }

        
    }

	private void OnDrawGizmos()
	{
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(hitPos,0.1f);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Ray(transform.position, forward));
	}


}
