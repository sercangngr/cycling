using UnityEngine;

public class DarkZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.HasComponent<Player>())
            EventManager.enterDark.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.HasComponent<Player>())
            EventManager.exitDark.Invoke();
    }
}
