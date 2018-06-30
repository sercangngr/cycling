using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kabuk;


public class DistanceIndicator : MonoBehaviour 
{

	public RectTransform distanceBarBackground;
	public Image distanceBar;


	private void Update()
	{
		float height = distanceBar.rectTransform.sizeDelta.y;
		float posY = distanceBar.fillAmount * height;
		((RectTransform)transform).anchoredPosition = new Vector2(2, posY + 5);
	}



}
