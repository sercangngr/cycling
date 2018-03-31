using UnityEngine;

public class PowerUp : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        PickUp();
    }

    public virtual void PickUp()
    {
        EventManager.powerUpPickedUp.Invoke(this);
    }

}
