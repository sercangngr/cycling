﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Obstacle : MonoBehaviour, PlayerTriggerListener
{
	[Range(0,1)]
	public float speedMultiplier = 0.1f;
	public float consumedEnergy = 3;


	public void OnPlayerEnter(Player player)
	{

		if(player.state.hasShield)
		{
			Destroy(this);
			return;
		}

		player.state.energyLeft -= consumedEnergy;
		player.state.speedEffectorCounter++;
		player.state.speedMultiplier = speedMultiplier;

	}

	public void OnPlayerExit(Player player)
	{
		player.state.speedEffectorCounter--;
	}

}
