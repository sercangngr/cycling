public class Energy : PowerUp
{
    public float energyValue = 5;

    public override void PickUp()
    {
        //EventManager.energyPickUp.Invoke(this);
        //gameObject.SetActive(false);
        //GameState.Instance.energyLeft += (int)energyValue;
        //base.PickUp();
    }
}
