using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kabuk;


public class Boulder : MonoBehaviour, PlayerTriggerListener
{
	public float speedMultiplier = 0.1f;
    public float consumedEnergy = 3;
   

    public void OnPlayerEnter(Player player)
    {
        RemoveMeshes();
        if (player.state.hasShield)
        {
            Destroy(this);
            return;
        }

        player.state.energyLeft -= consumedEnergy;
        player.state.speedEffectorCounter++;
        player.state.speedMultiplier = speedMultiplier;

    }

    void RemoveMeshes()
    {
		Destroy(transform.parent.GetComponent<MeshRenderer>());
    }

    public void OnPlayerExit(Player player)
    {
        player.state.speedEffectorCounter--;
    }
}
