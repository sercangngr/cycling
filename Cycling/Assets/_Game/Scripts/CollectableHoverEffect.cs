using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kabuk;


public class CollectableHoverEffect : MonoBehaviour 
{
    Transform cam;

    

	private IEnumerator Start()
	{
        cam = Camera.main.transform;

		float duration = 1f;
		float distance = 0.5f;
		float timer = duration / 2;
		bool reverse = false;

		Vector3 topPos = transform.position + Vector3.up * distance / 2;
		Vector3 bottomPos = transform.position + Vector3.down * distance / 2;


		while(this != null && enabled)
		{
			float nTime = timer / duration;
			if (reverse) { nTime = 1 - nTime; }
			Vector3 pos = Vector3.Lerp(topPos, bottomPos,nTime);
			transform.position = pos;

			timer += Time.deltaTime;
			if(timer > duration)
			{
				timer = timer - duration;
				reverse = !reverse;
			}

			yield return null;
		}

		
	}

    private void Update()
    {

		transform.LookAt(Camera.main.transform);
		Vector3 rot = transform.rotation.eulerAngles;
		rot.z = 0;
		rot.x = 0;
		transform.rotation = Quaternion.Euler(rot);
    }

}
