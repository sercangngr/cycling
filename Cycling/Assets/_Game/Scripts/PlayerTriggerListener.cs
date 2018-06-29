using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerTriggerListener 
{
	void OnPlayerEnter(Player player);
	void OnPlayerExit(Player player);
}
