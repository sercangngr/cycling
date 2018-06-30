using UnityEngine;
using UnityEngine.PostProcessing;

public class Player : MonoBehaviour
{

	public GameObject handleBar;
	public float handleBarRotation = 0;


	public float speed = 0;

	[Header("Constants")]
	public float RotationSpeed;
	public float RotationSpeedThreshold;
	public float Acceleration;
	public float Drag;
	public float MaxSpeed;
    

	public PlayerState state;
    public Rigidbody rigid;
	public GameObject torchlight;


	Ardunio.AInput ardunioInput;


	private void Awake()
	{
		speed = 0;
		handleBarRotation = 0;
	}


	public void Turn(float angle)
    {
		// -90 LeftMost
		// +90 RightMost
		angle = Mathf.Clamp(angle, -90, 90);
		handleBar.transform.localRotation = Quaternion.Euler(0, angle,0);
    }


	private void Update()
	{
		if (!state.started) { return; }
		if(GameState.Instance.GameOver)
		{
			rigid.velocity = Vector3.zero;
			return;
		}

		HandleInput();
		UpdateState();
	}

	void UpdateState()
	{
		//TIME
		state.timeLeft -= Time.deltaTime;

        // ENERGY
		float energyConsumption = Time.deltaTime * 0.25f;
        if (state.insideRain && !state.hasRainCoat)
        {
            energyConsumption *= PlayerState.RainEnergyConsumptionMultiplier;
        }
        else if (state.insideTunnel && !state.hasShield)
        {
            energyConsumption *= PlayerState.TunnelConsumptionMultiplier;
        }

		if(speed > 0)
		{
			state.energyLeft -= Time.deltaTime / 4;
        }

        //SHIELD
        if (state.hasShield)
        {
            state.shieldTimer -= Time.deltaTime;
            if (state.shieldTimer < 0)
            {
                state.hasShield = false;
            }
        }

		//POSITION
		state.position = transform.position;

		if(state.energyLeft <= 0 || state.timeLeft <= 0)
		{
			GameState.EventGameOver.Fire();
		}
		
	}
    
	void HandleInput()
	{

		if(Ardunio.Instance.Working)
		{
			handleBarRotation = (ardunioInput.normalizedHandleBarRotation - 0.5f) * 90;
            handleBarRotation = Mathf.Clamp(handleBarRotation, -90, 90);
            Turn(handleBarRotation);

            float y = ardunioInput.normalizedSpeed;
            speed += y * Acceleration * Time.deltaTime;
            speed -= Time.deltaTime * Drag;
            speed = Mathf.Clamp(speed, 0, MaxSpeed);

            Debug.Log("Speed: " + speed);

			
		}else
		{
			float turningAmount = Input.GetAxis("Horizontal") * RotationSpeed * Time.deltaTime;
            handleBarRotation += turningAmount;

           

            handleBarRotation = Mathf.Clamp(handleBarRotation, -90, 90);

            Turn(handleBarRotation);

            float y = Input.GetAxis("Vertical");
            speed += y * Acceleration * Time.deltaTime;
            speed -= Time.deltaTime * Drag;
            speed = Mathf.Clamp(speed, 0, MaxSpeed);
			
		}
		
	}
    
	private void FixedUpdate()
	{
		float s = speed;
		if (state.speedEffectorCounter > 0)
        {
            s *= state.speedMultiplier;
        }
		Vector3 vel = s * handleBar.transform.forward;
		vel.y = rigid.velocity.y;
		rigid.velocity = vel;


		float t = speed / RotationSpeedThreshold;
		Quaternion rot = Quaternion.Lerp(transform.rotation, handleBar.transform.rotation,t);
		rigid.MoveRotation(rot);
	}

	private void OnEnable()
	{
		GameState.EventStartGame.Register(OnGameStarted);
		Ardunio.EventArdunioInput.Register(OnArdunioInput);
	}

	private void OnDisable()
	{
		GameState.EventStartGame.Unregister(OnGameStarted);
		Ardunio.EventArdunioInput.Unregister(OnArdunioInput);
	}

	void OnGameStarted(PlayerState playerState)
	{
		state = playerState;
		state.started = true;
		state.position = transform.position;
	}

	private void OnTriggerEnter(Collider other)
	{
		PlayerTriggerListener[] listeners = other.GetComponents<PlayerTriggerListener>();
		for (int i = 0; i < listeners.Length; i ++)
		{
			listeners[i].OnPlayerEnter(this);
		}

	}

	private void OnTriggerExit(Collider other)
	{
		PlayerTriggerListener[] listeners = other.GetComponents<PlayerTriggerListener>();
        for (int i = 0; i < listeners.Length; i++)
        {
			listeners[i].OnPlayerExit(this);
        }
	}


	void OnArdunioInput(Ardunio.AInput input)
	{
		ardunioInput = input;
	}

}
