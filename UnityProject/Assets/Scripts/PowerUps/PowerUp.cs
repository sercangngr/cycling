using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PowerUp : MonoBehaviour
{
    [HideInInspector]
    public Sprite sprite;
    public string itemName = "PowerUp";
    public string function = "Powers the player up";
    public float score = 5;

    protected virtual void Start()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
    }

    private void OnTriggerEnter(Collider other)
    {
        PickUp();
    }

    public virtual void PickUp()
    {
        EventManager.powerUpPickedUp.Invoke(this);
    }

}
