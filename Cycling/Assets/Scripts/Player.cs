using UnityEngine;
using UnityEngine.PostProcessing;

public class Player : MonoBehaviour
{
    private Controller cont;
    private Rigidbody rigid;
    private bool canControl = true;
    [SerializeField]
    private RaceStatus raceStatus;
    [SerializeField]
    private PlayerStatus status;
    [SerializeField]
    private GameObject torchlight;

    public PostProcessingProfile profile;

    private Vector3 currentVel = Vector3.zero;

    private void Awake()
    {
        EventManager.gameOver.AddListener(GameOver);
        EventManager.restartRace.AddListener(RestartRace);
        EventManager.pause.AddListener(Pause);
        EventManager.start.AddListener(StartRace);
        EventManager.powerUpPickedUp.AddListener(PowerUpPickedUp);
        EventManager.finishReached.AddListener(FinishRace);
        EventManager.enterRain.AddListener(EnterRain);
        EventManager.exitRain.AddListener(ExitRain);
        EventManager.hitHazard.AddListener(HitHazard);
        EventManager.enterDark.AddListener(EnterDark);
        EventManager.exitDark.AddListener(ExitDark);
        EventManager.energyPickUp.AddListener(EnergyPickedUp);

        cont = Controller.Instance;
        Utils.SetComponent(out rigid, transform, true);
    }

    private void Start()
    {
        torchlight.SetActive(false);
    }

	const float MaxSpeed = 15;
    private void Move()
    {

		float speed = MaxSpeed
			* Ardunio.Instance.speed// * Time.deltaTime
			* (status.inRainZone ? status.RainCoeff : 1)
			* (status.inDarkZone ? status.DarkZoneCoeff : 1);

		Debug.Log (speed);

		Vector3 vel = transform.forward.normalized * speed;

		Debug.Log ("Vel" + vel);

        vel.y = rigid.velocity.y;
        rigid.velocity = vel;
    }

    private void Rotate()
    {

        Vector3 rot = rigid.rotation.eulerAngles + new Vector3(0, cont.angle - 90, 0) * Time.deltaTime * status.rotation;
        rigid.MoveRotation(Quaternion.Euler(rot));
    }

    private void Jump()
    {
        rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);
    }

    private void Pause(bool state)
    {
        canControl = !state;
        if (!canControl)
        {
            currentVel = rigid.velocity;
            rigid.velocity = Vector3.zero;
            rigid.isKinematic = true;
        }
        else
        {
            rigid.velocity = currentVel;
            rigid.isKinematic = false;
        }
    }

    private void GameOver()
    {
        canControl = false;
        if(SoundManager.instance.InGameAudio.isPlaying)
        {
            SoundManager.instance.InGameAudio.Stop();
        }
    }

    private void RestartRace(Transform startPos)
    {
        rigid.position = startPos.position;
        rigid.rotation = startPos.rotation;
        rigid.velocity = Vector3.zero;
        rigid.isKinematic = false;
        canControl = false;
        status.Reset();
    }

    private void StartRace()
    {
        canControl = true;
    }

    private void FinishRace()
    {
        status.score += status.currentEnergy * 2;
    }

    private void UpdateStatus()
    {
        if (cont.speed != 0)
        {
            status.currentEnergy -= Time.deltaTime;
            GameState.Instance.energyLeft -= Time.deltaTime;
        }

        if (status.inRainZone)
        {
            status.currentEnergy -= Time.deltaTime;
            GameState.Instance.energyLeft -= Time.deltaTime;
        }
            

        status.playerPos = transform.position;
    }

    private void PowerUpPickedUp(PowerUp pu)
    {
        status.score += pu.score;
    }

    private void EnergyPickedUp(Energy en)
    {
        status.currentEnergy += en.energyValue;
    }

    private void EnterRain()
    {
        if (!Inventory.PowerUps.Find(pu => pu.GetType() == typeof(RainCoat)))
            status.inRainZone = true;

     
    }

    private void ExitRain()
    {
        status.ResetSpeed();
        status.inRainZone = false;

    }

    private void HitHazard(Hazard boulder)
    {
        if (!Inventory.PowerUps.Find(obj => obj.GetType() == typeof(Shield)))
            status.score -= boulder.scoreDamage;
    }

	public bool hasTorch = false;

    private void EnterDark()
    {
        if (!torchlight)
        {
            Debug.LogError("No torchlight object is set on player!");
            return;
        }
		if (hasTorch)
            torchlight.SetActive(true);
        else
            status.inDarkZone = true;

    }

    private void ExitDark()
    {
        torchlight.SetActive(false);
        status.inDarkZone = false;
    }

    void LateUpdate()
    {
        if (canControl)
        {
            Rotate();
            Move();
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
        }

        UpdateStatus();
    }

	private void OnEnable()
	{
        GameState.EventGameOver.Register(OnGameOver);
	}

	private void OnDisable()
	{
        GameState.EventGameOver.Unregister(OnGameOver);
		status.inDarkZone = false;
		status.inRainZone = false;
	}

    void OnGameOver()
    {
        rigid.velocity = Vector3.zero;
        enabled = false;
    }
}
