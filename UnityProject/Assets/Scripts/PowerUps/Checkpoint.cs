public class Checkpoint : PowerUp
{
    public float bonusTime = 5;

    public override void PickUp()
    {
        EventManager.checkpointPickedUp.Invoke(bonusTime);
        gameObject.SetActive(false);
        base.PickUp();
    }
}
