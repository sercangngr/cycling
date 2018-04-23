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

    public void StartGame()
    {
        timeLeft = 120;
        energyLeft = 100;
        score = 0;

        EventStartGame.Fire();
    }



	
}
