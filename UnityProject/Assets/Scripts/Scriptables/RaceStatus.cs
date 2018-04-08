using UnityEngine;

[CreateAssetMenu(fileName = "RaceStatus", menuName = "DecathRace/Race Status")]
public class RaceStatus : ScriptableObject
{
    [SerializeField]
    private float raceTime = 10;
    [HideInInspector]
    public float currentRaceTime = 10;
    [SerializeField]
    private float countDown = 3;
    [HideInInspector]
    public float currentCountDown = 3;
    public string UICountDown
    {
        get
        {
            return Mathf.CeilToInt(currentCountDown).ToString();
        }
    }
    public Vector3 finishPos = Vector3.zero;
    public Vector3 startPos = Vector3.zero;

    public bool paused = false;

    public void Reset()
    {
        currentRaceTime = raceTime;
        currentCountDown = countDown;
    }
}
