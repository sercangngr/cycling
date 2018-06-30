using System.Collections;
using UnityEngine;

public class BoulderZone : MonoBehaviour, PlayerTriggerListener
{
	public GameObject boulderPrefab;
	public Transform[] spawnPoints;
	Coroutine spawn;

	public void OnPlayerEnter(Player player)
	{
		spawn = StartCoroutine(Spawn());
	}

	public void OnPlayerExit(Player player)
	{
		StopCoroutine(spawn);
	}
    
	IEnumerator Spawn()
	{
		float interval = 0.5f;
		int counter = 15;
		while(this != null && enabled && counter > 0)
		{
			counter--;
			Vector3 pos = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
			Instantiate(boulderPrefab).transform.position = pos;
			yield return new WaitForSeconds(interval);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.magenta;
		Vector3 size = transform.TransformVector(GetComponent<BoxCollider>().size);
		Gizmos.DrawWireCube(transform.position, size);
		foreach(Transform t in spawnPoints)
		{
			Gizmos.DrawSphere(t.position, 0.3f);
		}
	}




}
