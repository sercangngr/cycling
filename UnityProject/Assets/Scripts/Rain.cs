using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Rain : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.HasComponent<Player>())
            EventManager.enterRain.Invoke();

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.HasComponent<Player>())
            EventManager.exitRain.Invoke();
    }
}
