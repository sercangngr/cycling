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
}
