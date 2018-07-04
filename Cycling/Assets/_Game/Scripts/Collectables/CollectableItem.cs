using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collectable",menuName ="KabukRace/Collectable_Item")]

public class CollectableItem : ScriptableObject 
{
	public enum Type
	{
		RAIN_COAT, EXTRA_TIME, ENERGY, SHIELD, LIGHT, BONUS
	}

	public string notificationName;
	public Sprite sprite;
	public Sprite notificationIcon;

	public Type type;
	public float energy = 0;
	public float score = 0;
	public float time = 0;


}

