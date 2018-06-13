using System.Collections;
using UnityEngine;

public class RaceManager : Singleton<RaceManager>
{
    private GameObject startPos;

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
    }

    private void Update()
    {
     
    }

    public void CheckpointReached(float time)
    {
    }

    private void FinishReached()
    {
        EventManager.pause.Invoke(true);
        UIManager.GenerateQR(ShareSettings.GetTwitterLink(), ShareSettings.GetFacebookLink());
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
    }

    private void Pause(bool state)
    {
    }

}
