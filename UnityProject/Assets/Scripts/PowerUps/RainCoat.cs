public class RainCoat : PowerUp
{
    public override void PickUp()
    {
        gameObject.SetActive(false);
        base.PickUp();
    }
}
