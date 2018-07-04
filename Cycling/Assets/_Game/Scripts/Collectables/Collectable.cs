using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour,PlayerTriggerListener
{
	public CollectableItem item;
	public Image itemImage;
	public GameObject particleEffect;
	public GameObject platform;

	public Transform positionsContainer;

	private void Awake()
	{
		itemImage.sprite = item.sprite;
		if (positionsContainer.childCount != 0)
        {
			int randomPos = Random.Range(0, positionsContainer.childCount + 1);
			if (randomPos < positionsContainer.childCount)
            {
				transform.position = positionsContainer.GetChild(randomPos).position;
            }
        }

		AdjustPosition();
	}


	private void OnDrawGizmos()
	{
		if (Application.isPlaying) return;
		foreach(Transform t in positionsContainer)
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
		Destroy(GetComponentInChildren<Canvas>().gameObject);
		foreach(MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
		{
			Destroy(mr);
		}
		Destroy(particleEffect);
		Destroy(gameObject,2f);
		Destroy(this);

	}

	public void OnPlayerExit(Player player)
	{
		
	}

	void AdjustPosition()
	{
		Ray ray = new Ray(transform.position + Vector3.up, Vector3.down);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 4))
		{
			Debug.Log("Holo: " + hit.point);
			transform.position = hit.point;
		}else
		{
			Debug.LogError("Misplaced Collectable");
		}
	}
}
