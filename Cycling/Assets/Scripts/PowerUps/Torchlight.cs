using UnityEngine;

public class Torchlight : PowerUp
{
    public override void PickUp()
    {
        gameObject.SetActive(false);
        base.PickUp();

		//GameObject.FindObjectOfType<Player> ().hasTorch = true;

    }
}
