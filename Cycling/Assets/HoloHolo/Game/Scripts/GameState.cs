using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kabuk;

[UnitySingleton(UnitySingletonAttribute.Type.CreateOnNewGameObject)]
public class GameState : UnitySingleton<GameState> 
{
	public class EventStartGame : CustomEvent<EventStartGame,PlayerState>{}
    public class EventGameOver : CustomEvent<EventGameOver>{}
	public class EventNotify : CustomEvent<EventNotify,CollectableItem>{}

	public PlayerState playerState;

    public void StartGame()
    {
		playerState = new PlayerState();

		EventStartGame.Fire(playerState);
        EventManager.start.Invoke();
        EventManager.pause.Invoke(false);

        SoundManager.instance.InGameAudio.Play();

    }

    //IEnumerator Track()
    //{
    //    //bool run = true;
    //    //while (run)
    //    //{
    //    //    timeLeft -= Time.deltaTime;
    //    //    distanceLeft = Marks.Instance.GetDistance(player.transform.position);



    //    //    if(timeLeft <= 0 || energyLeft <= 0 || distanceLeft < 0.5f)
    //    //    {
    //    //        EventGameOver.Fire();
    //    //        run = false;
    //    //    }
    //    //    yield return null;
    //    //}
       
    //}

    public int GetScore()
    {
		return (int)(playerState.score + playerState.energyLeft * 2 + playerState.timeLeft * 2);
    }



	
}
