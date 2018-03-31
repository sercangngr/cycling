using UnityEngine;

public class RaceManager : Singleton<RaceManager>
{
    private GameObject startPos;

    public float raceTime = 10;

    public bool paused = false;

    private Checkpoint[] checkpoints;

    [Space(25), Header("Debug options"), SerializeField]
    private bool debug = false;

    private void Awake()
    {
        EventManager.finishReached.AddListener(FinishReached);
        EventManager.checkpointPickedUp.AddListener(CheckpointReached);
        EventManager.pause.AddListener(Pause);
    }

    private void Start()
    {
        if(!Utils.SetObjectWithTag(out startPos, Constants.startPosTag))
        {
            Player p;
            if(!Utils.SetObject(out p))
            {
                enabled = false;
                return;
            }
            startPos = ResourceManager.GetStartPos();
            startPos.transform.position = p.transform.position;
            startPos.transform.rotation = p.transform.rotation;
            Debug.Log("Created a StartPos object at Players's position");
        }

        checkpoints = FindObjectsOfType<Checkpoint>();
        if(checkpoints.Length == 0)
            Debug.LogWarning("No checkpoints in current scene!");
    }

    private void Update()
    {
        if(!paused)
        {
            if (raceTime > 0)
                raceTime -= Time.deltaTime;
            else
            {
                Debug.Log("Game Over!");
                paused = true;
            }
        }

        if(debug)
        {
            if (Input.GetKeyDown(KeyCode.R))
                RestartRace();
            if (Input.GetKeyDown(KeyCode.P))
                EventManager.pause.Invoke(!paused);
        }
    }

    public void CheckpointReached(float time)
    {
        raceTime += time;
    }

    private void FinishReached()
    {
        Debug.Log("Race compleated!");
        paused = true;
    }

    private void RestartRace()
    {
        for (int i = 0; i < checkpoints.Length; i++)
            checkpoints[i].gameObject.SetActive(true);

        EventManager.restartRace.Invoke(startPos.transform);
    }

    private void Pause(bool state)
    {
        paused = state;
    }
}
