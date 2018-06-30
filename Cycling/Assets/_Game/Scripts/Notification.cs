using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kabuk;


public class Notification : MonoBehaviour 
{
	public Text messageText;
	public Image iconImage;

	public void Setup(string message, Sprite icon)
	{
		iconImage.sprite = icon;
		messageText.text = message;
	}
	
}
