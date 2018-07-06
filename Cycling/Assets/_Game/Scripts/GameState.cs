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

	public bool GameOver
	{
		private set; get;
	}

    public void StartGame()
    {
		GameOver = false;
		playerState = new PlayerState();
		EventStartGame.Fire(playerState);
        Sound.instance.InGameAudio.Play();

    }


    public int GetScore()
    {
		return (int)(playerState.score + playerState.energyLeft * 2 + playerState.timeLeft * 2);
    }

	private void OnEnable()
	{
		EventGameOver.Register(OnGameOver);
	}

	private void OnDisable()
	{
		EventGameOver.Unregister(OnGameOver);
	}

	void OnGameOver()
	{
		GameOver = true;

        if (Sound.instance.InGameAudio.isPlaying)
        {
            Sound.instance.InGameAudio.Stop();
        }
    }




}
