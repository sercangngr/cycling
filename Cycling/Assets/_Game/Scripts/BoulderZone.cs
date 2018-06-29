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
		float interval = 1.5f;

		while(this != null && enabled)
		{
			Vector3 pos = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
			Instantiate(boulderPrefab).transform.position = pos;
			yield return new WaitForSeconds(interval);
		}
	}



}
