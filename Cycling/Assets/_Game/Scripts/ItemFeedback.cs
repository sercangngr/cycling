using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kabuk;


public class ItemFeedback : MonoBehaviour 
{
	public Text numberText;
	public Text contentText;

    
	public void Setup(CollectableItem item)
	{
		if(item.energy > 0)
		{
			contentText.text = "Enerji";
			numberText.text = "+" + ((int)item.energy);
		}else if(item.score > 0)
		{
			contentText.text = "Puan";
			numberText.text = "+" + ((int)item.score);
		}else if(item.time > 0)
		{
			contentText.text = "Zaman";
			numberText.text = "+" + ((int)item.time);
		}
	}


	private IEnumerator Start()
	{
		

		float duration = 3f;
		float timer = 0;
		while(timer < duration)
		{
			timer += Time.deltaTime;
			float nTime = timer / duration;
			Color color = new Color(1, 1, 1, 1 - nTime);
			contentText.color = color;
			numberText.color = color;

			yield return null;
		}

		Destroy(gameObject);
	}

}
