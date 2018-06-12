using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 10;
    public float scoreDamage = 5;
    public bool destroyOnImpact = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.HasComponent<Player>())
        {
            EventManager.hitHazard.Invoke(this);
            if (destroyOnImpact)
                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.HasComponent<Player>())
        {
            EventManager.hitHazard.Invoke(this);
            if (destroyOnImpact)
                Destroy(gameObject);
        }
    }

    void Start()
    {
        if (lifeTime > 0)
            Destroy(gameObject, lifeTime);
    }

}
