using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    private void OnEnable()
    {
        if(spawnPoints != null && spawnPoints.Length > 0)
            transform.position = spawnPoints.PickRandom().position;
    }
}
