using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour, PlayerTriggerListener
{
	public void OnPlayerEnter(Player player)
	{
		player.state.insideTunnel = true;
		if(player.state.hasTorch)
		{
			player.torchlight.SetActive(true);
		}
	}

	public void OnPlayerExit(Player player)
	{
		player.state.insideTunnel = false;
		player.state.hasTorch = false;
		player.torchlight.SetActive(false);
	}

	private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        BoxCollider c = GetComponent<BoxCollider>();
        Gizmos.DrawWireCube(c.center + transform.position, c.size);
    }
}
