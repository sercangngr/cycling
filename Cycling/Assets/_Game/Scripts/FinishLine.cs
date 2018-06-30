using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kabuk;


public class FinishLine : MonoBehaviour, PlayerTriggerListener
{

	public void OnPlayerEnter(Player player)
    {
		GameState.EventGameOver.Fire();      
    }

	public void OnPlayerExit(Player player)
	{
		
	}

	
}
