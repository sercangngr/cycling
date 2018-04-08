using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "DecathRace/Player Status")]
public class PlayerStatus : ScriptableObject
{
    [SerializeField]
    private float energy = 100;
    [HideInInspector]
    public float currentEnergy = 100;
    public string playerName = "Player_1";
    public float score = 0;
    [SerializeField]
    private float speed = 500;
    [SerializeField]
    private float rainCoeff = 0.5f;
    public float RainCoeff
    {
        get { return rainCoeff; }
    }
    [HideInInspector]
    public float currentSpeed = 500;
    public float rotation = 1;
    public bool inRainZone = false;
    [SerializeField]
    private float darkZoneCoeff = 0.5f;
    public float DarkZoneCoeff
    {
        get { return darkZoneCoeff; }
    }
    public bool inDarkZone = false;
    public Vector3 playerPos = Vector3.zero;

    public void Reset()
    {
        ResetSpeed();
        ResetEnergy();
        ResetScore();
        inRainZone = false;
        inDarkZone = false;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public void ResetSpeed()
    {
        currentSpeed = speed;
    }

    public void ResetEnergy()
    {
        currentEnergy = energy;
    }
}
