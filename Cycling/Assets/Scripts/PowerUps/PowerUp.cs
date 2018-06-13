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
        this.gameObject.SetActive(false);
    }

    public virtual void PickUp()
    {
        EventManager.powerUpPickedUp.Invoke(this);
        Notification not = new Notification();
        not.text = itemName;
        GameState.EventNotify.Fire(not);
        //GameState.Instance.score += (int)score;
        SoundManager.instance.PickUpAudio.Play();
    }

}
