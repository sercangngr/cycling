using System.Collections;
using UnityEngine;

public class RaceManager : Singleton<RaceManager>
{
    private GameObject startPos;

    [SerializeField]
    private RaceStatus status;

    private bool restarting = false;

    private PowerUp[] powerUps;

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

        powerUps = FindObjectsOfType<PowerUp>();
        if (powerUps.Length == 0)
            Debug.LogWarning("No checkpoints in current scene!");

        EventManager.pause.Invoke(true);
        status.startPos = startPos.transform.position;
        Finish f;
        if (Utils.SetObject(out f))
            status.finishPos = f.transform.position;
    }

    private void Update()
    {
        if (!status.paused)
        {
            if (status.currentRaceTime > 0)
                status.currentRaceTime -= Time.deltaTime;
            else
            {
                Debug.Log("Game Over!");
                status.paused = true;
            }
        }

        if (debug)
        {
            if (Input.GetKeyDown(KeyCode.R))
                RestartRace();
            if (Input.GetKeyDown(KeyCode.P))
                EventManager.pause.Invoke(!status.paused);
        }
    }

    public void CheckpointReached(float time)
    {
        status.currentRaceTime += time;
    }

    private void FinishReached()
    {
        Debug.Log("Race compleated!");
        status.paused = true;
        EventManager.pause.Invoke(true);
    }

    private void RestartRace()
    {
        if (restarting) return;
        restarting = true;
        EventManager.pause.Invoke(true);
        Inventory.Clear();

        for (int i = 0; i < powerUps.Length; i++)
            powerUps[i].gameObject.SetActive(true);

        EventManager.restartRace.Invoke(startPos.transform);
        status.Reset();
        StartCoroutine(CountDown());
    }

    private void Pause(bool state)
    {
        status.paused = state;
    }

    private IEnumerator CountDown()
    {
        Debug.Log("Counting down...");
        while (status.currentCountDown > 0)
        {
            status.currentCountDown -= Time.deltaTime;
            yield return null;
        }

        EventManager.start.Invoke();
        EventManager.pause.Invoke(false);
        restarting = false;
    }
}
