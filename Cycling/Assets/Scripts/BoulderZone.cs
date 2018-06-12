using UnityEngine;

public class BoulderZone : MonoBehaviour
{
    [SerializeField]
    private Transform[] boulderSpawn;
    [SerializeField]
    private float minSpawnTime = 1;
    [SerializeField]
    private float maxSpawnTime = 5;
    private float currentSpawnTime = 0;
    private bool spawn = false;

    private void OnTriggerEnter(Collider other)
    {
		Debug.Log ("Trigger");
        if (other.transform.HasComponent<Player>())
        {
			Debug.Log ("Player Enter");
            spawn = true;
            EventManager.enterBoulder.Invoke();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.HasComponent<Player>())
        {
            spawn = false;
            EventManager.exitBoulder.Invoke();
        }
    }

    private void Update()
    {
        if (spawn)
            CheckSpawn();
    }

    private void CheckSpawn()
    {
		if (currentSpawnTime <= 0) {
			currentSpawnTime = Random.Range (minSpawnTime, maxSpawnTime);
			SpawnBoulder ();
		} else {
			currentSpawnTime -= Time.deltaTime;
		}
          
    }

    private void SpawnBoulder()
    {
		if (boulderSpawn != null && boulderSpawn.Length > 0)
		{
			ResourceManager.GetBoulder(boulderSpawn.PickRandom().position);
			Debug.Log ("Boulder");
		}
        else
        {
            enabled = false;
            Debug.LogWarning("Cannot spawn boulders, no bouler spawn points are registered to BoulderZone (" + name + ")");
        }
    }

}
