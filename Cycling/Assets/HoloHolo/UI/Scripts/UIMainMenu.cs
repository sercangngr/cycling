using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kabuk;

public class UIMainMenu : MonoBehaviour 
{
 
    public GameObject[] pagePrefabs;
    GameObject[] pages;
    public GameObject currentPage;
    int pageIndex = -1;

    public GameObject gameUIPrefab;


	private void Awake()
	{
        SetPage(0);
	}

    void SetPage(int pageNo)
    {
		Debug.Log(pageNo);

        pageNo = Mathf.Clamp(pageNo, 0, pagePrefabs.Length);
        if(pageNo != pageIndex)
        {
            pageIndex = pageNo;
            if(currentPage != null)
            {
                Destroy(currentPage);
            }
			if(pageIndex == pagePrefabs.Length - 1)
			{
				Destroy(gameObject);
				currentPage = Instantiate(pagePrefabs[pageIndex]);
			}else
			{
				currentPage = Instantiate(pagePrefabs[pageIndex], transform);
			}
        }


    }


    private void OnEnable()
    {
        Ardunio.EventButtonCross.Register(OnButtonCrossClicked);
        Ardunio.EventButtonCheck.Register(OnButtonCheckClicked);
    }

    private void OnDisable()
    {
        Ardunio.EventButtonCross.Unregister(OnButtonCrossClicked);
        Ardunio.EventButtonCheck.Unregister(OnButtonCheckClicked);
    }


    void OnButtonCrossClicked()
    {
        SetPage(pageIndex - 1);
    }

    void OnButtonCheckClicked()
    {
        SetPage(pageIndex + 1);
    }
}
