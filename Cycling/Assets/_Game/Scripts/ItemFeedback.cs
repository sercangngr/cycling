using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kabuk;


public class ItemFeedback : MonoBehaviour 
{
	public Text numberText;
	public Text contentText;
    public RectTransform container;

    
	public void Setup(CollectableItem item)
	{
        int number = 0;
		if(item.energy > 0)
		{
			contentText.text = "Enerji";
            number = ((int)item.energy);
		}else if(item.score > 0)
		{
			contentText.text = "Puan";
            number = ((int)item.score);
		}else if(item.time > 0)
		{
			contentText.text = "Zaman";
            number = ((int)item.time);
		}

        Vector3 pos = container.transform.localPosition;
        if (Mathf.FloorToInt(number / 100) > 0)
        {
            pos.x = 164;
        }
        container.transform.localPosition = pos;
        numberText.text = "+" + number;
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
