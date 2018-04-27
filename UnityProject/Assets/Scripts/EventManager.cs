using UnityEngine;
using UnityEngine.Events;

public class CheckpointPicked : UnityEvent<float>
{ }

public class PowerUpPickedUp : UnityEvent<PowerUp>
{ }

public class FinishReached : UnityEvent
{ }

public class RestartRace : UnityEvent<Transform>
{ }

public class GameOver : UnityEvent
{ }

public class Pause : UnityEvent<bool>
{ }

public class PowerUpAddedToInventory : UnityEvent<PowerUp>
{ }

public class PowerUpRemovedFromInventory : UnityEvent<PowerUp>
{ }

public class InventoryCleared : UnityEvent
{ }

public class Start : UnityEvent
{ }

public class RequestRestart : UnityEvent
{ }

public class EnterRain : UnityEvent
{ }

public class ExitRain : UnityEvent
{ }

public class EnterBoulder : UnityEvent
{ }

public class ExitBoulder : UnityEvent
{ }

public class HitBoulder : UnityEvent<Hazard>
{ }

public class EnterDark : UnityEvent
{ }

public class ExitDark : UnityEvent
{ }

public class EnergyPickUp : UnityEvent<Energy>
{ }

public static class EventManager
{
    public static CheckpointPicked checkpointPickedUp = new CheckpointPicked();
    public static PowerUpPickedUp powerUpPickedUp = new PowerUpPickedUp();
    public static FinishReached finishReached = new FinishReached();
    public static RestartRace restartRace = new RestartRace();
    public static GameOver gameOver = new GameOver();
    public static Pause pause = new Pause();
    public static PowerUpAddedToInventory powerUpAddedToInventory = new PowerUpAddedToInventory();
    public static PowerUpRemovedFromInventory powerUpRemovedFromInventory = new PowerUpRemovedFromInventory();
    public static InventoryCleared inventoryCleared = new InventoryCleared();
    public static Start start = new Start();
    public static RequestRestart requestRestart = new RequestRestart();
    public static EnterRain enterRain = new EnterRain();
    public static ExitRain exitRain = new ExitRain();
    public static EnterBoulder enterBoulder = new EnterBoulder();
    public static ExitBoulder exitBoulder = new ExitBoulder();
    public static HitBoulder hitHazard = new HitBoulder();
    public static EnterDark enterDark = new EnterDark();
    public static ExitDark exitDark = new ExitDark();
    public static EnergyPickUp energyPickUp = new EnergyPickUp();


	public static void Restart()
	{

		checkpointPickedUp = new CheckpointPicked();
		powerUpPickedUp = new PowerUpPickedUp();
		finishReached = new FinishReached();
		restartRace = new RestartRace();
		gameOver = new GameOver();
		pause = new Pause();
		powerUpAddedToInventory = new PowerUpAddedToInventory();
		powerUpRemovedFromInventory = new PowerUpRemovedFromInventory();
		inventoryCleared = new InventoryCleared();
		start = new Start();
		requestRestart = new RequestRestart();
		enterRain = new EnterRain();
		exitRain = new ExitRain();
		enterBoulder = new EnterBoulder();
		exitBoulder = new ExitBoulder();
		hitHazard = new HitBoulder();
		enterDark = new EnterDark();
		exitDark = new ExitDark();
		energyPickUp = new EnergyPickUp();
		
	}




    // HOLO Events
}
