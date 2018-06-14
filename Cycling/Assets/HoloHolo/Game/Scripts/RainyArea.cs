using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainyArea : MonoBehaviour, PlayerTriggerListener
{

	public float speedMultiplier = 0.9f;


	public void OnPlayerEnter(Player player)
	{
		player.state.insideRain = true;
		player.state.speedEffectorCounter++;
		player.state.speedMultiplier = speedMultiplier;
	}

	public void OnPlayerExit(Player player)
	{
		player.state.insideRain = false;
		player.state.speedEffectorCounter--;
	}
    
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		BoxCollider c = GetComponent<BoxCollider>();
		Gizmos.DrawWireCube(c.center + transform.position, c.size);
	}
}
