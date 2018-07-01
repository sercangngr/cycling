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
	public Color blue = new Color(0, 0, 1, 0.2f);

	public Player player;
	public Image effectImage;
	public Image gradiendEffectImage;
    
	private void Awake()
	{
		effectImage.color = transparent;
	}

	private void Update()
	{
		if(player.state.insideTunnel && !player.state.hasTorch)
		{
			effectImage.color = tunnelBlack;
		}else if(player.state.insideRain)
		{
			effectImage.color = rainBlack;
		}else
		{
			effectImage.color = transparent;
		}
	}

	private void OnEnable()
	{
		GameState.EventNotify.RegisterCoroutine(OnNotification);
		
	}

	private void OnDisable()
	{
		GameState.EventNotify.UnregisterCoroutine(OnNotification);
	}

	IEnumerator OnNotification(CollectableItem item)
	{
		if(item.type == CollectableItem.Type.SHIELD)
		{
			gradiendEffectImage.color = green;
			yield return new WaitForSeconds(0.1f);
			while(player.state.hasShield)
			{
				yield return null;
			}
			gradiendEffectImage.color = transparent;
			
		}else
		{
			gradiendEffectImage.color = blue;
			yield return new WaitForSeconds(0.3f);
			gradiendEffectImage.color = transparent;
        }
		
	}



}
