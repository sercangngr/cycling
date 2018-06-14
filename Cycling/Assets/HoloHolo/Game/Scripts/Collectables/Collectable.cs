﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour,PlayerTriggerListener
{
	public CollectableItem item;

	private void Awake()
	{
		GetComponent<MeshRenderer>().material.mainTexture = item.texture;
		if (transform.childCount != 0)
        {
            int randomPos = Random.Range(0, transform.childCount + 1);
            if (randomPos < transform.childCount)
            {
                transform.position = transform.GetChild(randomPos).position;
            }
        }
	}


	private void OnDrawGizmos()
	{
		if (Application.isPlaying) return;
		foreach(Transform t in transform)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(t.position, transform.position);
			Gizmos.DrawWireSphere(t.position, 0.3f);
		}
	}


	public void OnPlayerEnter(Player player)
	{
		GameState.EventNotify.Fire(item);

		player.state.score += item.score;
		player.state.timeLeft += item.time;
		player.state.energyLeft += item.energy;

		if(item.type == CollectableItem.Type.RAIN_COAT)
		{
			player.state.hasRainCoat = true;
		}else if(item.type == CollectableItem.Type.SHIELD)
		{

			player.state.hasShield = true;
			player.state.shieldTimer = PlayerState.ShieldDuration;
		}else if(item.type == CollectableItem.Type.LIGHT)
		{
			player.state.hasTorch = true;
		}
        
		GetComponent<AudioSource>().Play();
		Destroy(GetComponent<MeshRenderer>());
		Destroy(gameObject,2f);
		Destroy(this);
        

	}

	public void OnPlayerExit(Player player)
	{
		
	}
}
