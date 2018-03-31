public class Finish : PowerUp
{
    public override void PickUp()
    {
        EventManager.finishReached.Invoke();
        base.PickUp();
    }
}
