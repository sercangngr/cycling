using System.Collections;
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
        itemIndex = Mathf.Clamp(index, 0, items.Length - 1);
        selectedItemImage.sprite = items[itemIndex].sprite;
        selectedItemText.text = items[itemIndex].text;
    }

	private void OnEnable()
	{
        UIMainMenu.EventButtonLeft.Register(OnButtonLeftClicked);
        UIMainMenu.EventButtonRight.Register(OnButtonRightClicked);
	}

	private void OnDisable()
	{
        UIMainMenu.EventButtonLeft.Unregister(OnButtonLeftClicked);
        UIMainMenu.EventButtonRight.Unregister(OnButtonRightClicked);
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
