using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kabuk;

public class UIMainMenu : MonoBehaviour 
{
    public class EventStartGame  : CustomEvent<EventStartGame>{}
    public class EventNextPage : CustomEvent<EventNextPage>{}
    public class EventPreviousPage : CustomEvent<EventPreviousPage>{}


    public class EventButtonLeft : CustomEvent<EventButtonLeft>{}
    public class EventButtonRight : CustomEvent<EventButtonRight>{}
    public class EventButtonCheck : CustomEvent<EventButtonCheck> { }
    public class EventButtonCross : CustomEvent<EventButtonCross> { }


    public GameObject[] pagePrefabs;
    GameObject[] pages;
    public GameObject currentPage;
    int pageIndex = -1;


	private void Awake()
	{
        SetPage(0);
	}

    void SetPage(int pageNo)
    {
        if(pageNo == pagePrefabs.Length)
        {
            EventStartGame.Fire();
            Destroy(gameObject);
            return;
        }


        pageNo = Mathf.Clamp(pageNo, 0, pagePrefabs.Length - 1);
        if(pageNo != pageIndex)
        {
            pageIndex = pageNo;
            if(currentPage != null)
            {
                Destroy(currentPage);
            }
            currentPage = Instantiate(pagePrefabs[pageIndex], transform);
        }
    }

	private void Update()
	{
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            EventButtonLeft.Fire();
        }else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            EventButtonRight.Fire();
        }else if (Input.GetKeyDown(KeyCode.Return))
        {
            EventButtonCheck.Fire();

        }else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            EventButtonCross.Fire();
        }
	}


    private void OnEnable()
    {
        EventButtonCross.Register(OnButtonCrossClicked);
        EventButtonCheck.Register(OnButtonCheckClicked);
    }

    private void OnDisable()
    {
        EventButtonCross.Unregister(OnButtonCrossClicked);
        EventButtonCheck.Unregister(OnButtonCheckClicked);
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
