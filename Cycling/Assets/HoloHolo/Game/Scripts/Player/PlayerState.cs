using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // For Debug
public class PlayerState 
{
	public const float ShieldDuration = 5;
	public const float RainSlowdownCoef = 0.5f;
	public const float TunnelSlowdownCoef = 0.5f;

	public bool hasShield = false;
	public float shieldTimer = 0;

	public bool hasTorch = false;
	public bool hasRainCoat = false;
	public Vector3 position;

	public bool insideRain = false;
	public bool insideTunnel = false;

	public float score = 0;
	public float timeLeft = 250;
	public float energyLeft = 300;
	public float distanceLeft = 150;


}
