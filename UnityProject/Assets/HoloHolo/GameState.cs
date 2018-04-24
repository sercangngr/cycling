using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kabuk;

[UnitySingleton(UnitySingletonAttribute.Type.CreateOnNewGameObject)]
public class GameState : UnitySingleton<GameState> 
{
    public class EventStartGame : CustomEvent<EventStartGame>{}
    public class EventGameOver : CustomEvent<EventGameOver>{}
    public class EventNotify : CustomEvent<EventNotify,Notification>{}


    public float timeLeft = 120;
    public float energyLeft = 100;
    public int score = 0;
    public float distanceLeft = 100;

    Transform player;
    Transform endPoint;

    public void StartGame()
    {
        timeLeft = 300;
        energyLeft = 300;
        score = 0;
        player = GameObject.Find("Player").transform;
        endPoint = GameObject.Find("Finish").transform;
        distanceLeft = Marks.Instance.GetDistance(player.transform.position);
        Debug.Log(distanceLeft);




        EventStartGame.Fire();
        EventManager.start.Invoke();
        EventManager.pause.Invoke(false);
        StartCoroutine(Track());

        SoundManager.instance.InGameAudio.Play();

    }

    IEnumerator Track()
    {
        bool run = true;
        while (run)
        {
            timeLeft -= Time.deltaTime;
            distanceLeft = Marks.Instance.GetDistance(player.transform.position);



            if(timeLeft <= 0 || energyLeft <= 0 || distanceLeft < 0.5f)
            {
                EventGameOver.Fire();
                run = false;
            }
            yield return null;
        }
       
    }

    public int GetScore()
    {
        return GameState.Instance.score + (int)GameState.Instance.energyLeft * 2 + (int)GameState.Instance.timeLeft * 2;
    }



	
}
