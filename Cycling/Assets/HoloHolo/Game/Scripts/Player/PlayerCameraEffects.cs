using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCameraEffects : MonoBehaviour 
{
	Color transparent = new Color(0, 0, 0, 0);
	Color black = new Color(0, 0, 0, 0.7f);
	Color green = new Color(0, 1, 0, 0.2f);

	public Player player;
	public Image effectImage;
    
	private void Awake()
	{
		effectImage.color = transparent;
	}

	private void Update()
	{
		if(player.state.hasShield)
		{
			effectImage.color = green;
		}else if(player.state.insideTunnel && !player.state.hasTorch)
		{
			effectImage.color = black;
		}else
		{
			effectImage.color = transparent;
		}
	}



}
