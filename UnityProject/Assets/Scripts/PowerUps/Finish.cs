public class Finish : PowerUp
{
    public override void PickUp()
    {
        EventManager.finishReached.Invoke();
        GameState.EventGameOver.Fire();
        base.PickUp();
    }
}
