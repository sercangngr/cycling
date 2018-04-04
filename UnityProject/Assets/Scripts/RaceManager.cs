using System.Collections;
using UnityEngine;

public class RaceManager : Singleton<RaceManager>
{
    private GameObject startPos;

    public float raceTime = 10;
    private float countDown = 3;

    public bool paused = true;
    private bool restarting = false;

    private Checkpoint[] checkpoints;

    [Space(25), Header("Debug options"), SerializeField]
    private bool debug = false;

    private void Awake()
    {
        EventManager.finishReached.AddListener(FinishReached);
        EventManager.checkpointPickedUp.AddListener(CheckpointReached);
        EventManager.pause.AddListener(Pause);
        EventManager.requestRestart.AddListener(RestartRace);
    }

    private void Start()
    {
        if (!Utils.SetObjectWithTag(out startPos, Constants.startPosTag))
        {
            Player p;
            if (!Utils.SetObject(out p))
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
        if (checkpoints.Length == 0)
            Debug.LogWarning("No checkpoints in current scene!");

        EventManager.pause.Invoke(true);
    }

    private void Update()
    {
        if (!paused)
        {
            if (raceTime > 0)
                raceTime -= Time.deltaTime;
            else
            {
                Debug.Log("Game Over!");
                paused = true;
            }
        }

        if (debug)
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
        if (restarting) return;
        restarting = true;
        Inventory.Clear();

        for (int i = 0; i < checkpoints.Length; i++)
            checkpoints[i].gameObject.SetActive(true);

        EventManager.restartRace.Invoke(startPos.transform);
        StartCoroutine(CountDown());
    }

    private void Pause(bool state)
    {
        paused = state;
    }

    private IEnumerator CountDown()
    {
        Debug.Log("Counting down...");
        while (countDown > 0)
        {
            countDown -= Time.deltaTime;
            yield return null;
        }

        EventManager.start.Invoke();
        restarting = false;
        countDown = 3;
    }
}
