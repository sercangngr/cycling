public class Shield : PowerUp
{
    public float lifeTime;

    public override void PickUp()
    {
        gameObject.SetActive(false);
        base.PickUp();
        Invoke("RemoveFromInventory", lifeTime);
    }

    private void RemoveFromInventory()
    {
        Inventory.Remove(this);
    }
}
