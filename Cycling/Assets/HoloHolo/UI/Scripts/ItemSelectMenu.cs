﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectMenu : MonoBehaviour 
{
    public UIItem[] items;
    public Image selectedItemImage;
    public Text selectedItemText;
    int itemIndex = 0;

	private void Awake()
	{
        SetSelected(0);
	}

    void SetSelected(int index)
    {
		itemIndex = (itemIndex + 1) % items.Length;
        selectedItemImage.sprite = items[itemIndex].sprite;
        selectedItemText.text = items[itemIndex].text;
    }

	private void OnEnable()
	{
        Ardunio.EventButtonLeft.Register(OnButtonLeftClicked);
        Ardunio.EventButtonRight.Register(OnButtonRightClicked);
	}

	private void OnDisable()
	{
        Ardunio.EventButtonLeft.Unregister(OnButtonLeftClicked);
        Ardunio.EventButtonRight.Unregister(OnButtonRightClicked);
	}

	void OnButtonLeftClicked()
    {
        SetSelected(itemIndex - 1);
    }

    void OnButtonRightClicked()
    {
        SetSelected(itemIndex + 1);
    }

}
