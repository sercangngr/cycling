using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCameraEffects : MonoBehaviour 
{
	public Color transparent = new Color(0, 0, 0, 0);
	public Color tunnelBlack = new Color(0, 0, 0, 0.7f);
	public Color rainBlack = new Color(0, 0, 0, 0.3f);
	public Color green = new Color(0, 1, 0, 0.2f);

	public Player player;
	public Image effectImage;
	public Image gradiendEffectImage;
    
	private void Awake()
	{
		effectImage.color = transparent;
	}

	private void Update()
	{
		if(player.state.hasShield)
		{
			gradiendEffectImage.color = green;
			effectImage.color = transparent;
		}else if(player.state.insideTunnel && !player.state.hasTorch)
		{
			effectImage.color = tunnelBlack;
			gradiendEffectImage.color = transparent;
		}else if(player.state.insideRain)
		{
			effectImage.color = rainBlack;
			gradiendEffectImage.color = transparent;
		}else
		{
			gradiendEffectImage.color = transparent;
			effectImage.color = transparent;
		}
	}



}
