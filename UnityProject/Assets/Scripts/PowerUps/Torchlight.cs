public class Torchlight : PowerUp
{
    public override void PickUp()
    {
        gameObject.SetActive(false);
        base.PickUp();
    }
}
