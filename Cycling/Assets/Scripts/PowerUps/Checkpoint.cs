public class Checkpoint : PowerUp
{
    public float bonusTime = 5;

    public override void PickUp()
    {
        //EventManager.checkpointPickedUp.Invoke(bonusTime);
        //GameState.Instance.timeLeft += bonusTime;
        //gameObject.SetActive(false);
        //base.PickUp();
    }
}
