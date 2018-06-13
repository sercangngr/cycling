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

    private Vector3 currentVel = Vector3.zero;

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
		float turningAmount = Input.GetAxis("Horizontal") * RotationSpeed * Time.deltaTime;
		handleBarRotation += turningAmount;
		Turn(handleBarRotation);

		float y = Input.GetAxis("Vertical");
		speed += y * Acceleration * Time.deltaTime;
		speed -= Time.deltaTime * Drag;
		speed = Mathf.Clamp(speed, 0, MaxSpeed);
		
	}
    
	private void FixedUpdate()
	{
		Vector3 vel = speed * handleBar.transform.forward;
		vel.y = rigid.velocity.y;
		rigid.velocity = vel;


		float t = speed / RotationSpeedThreshold;
		Quaternion rot = Quaternion.Lerp(transform.rotation, handleBar.transform.rotation,t);
		rigid.MoveRotation(rot);
	}

	private void OnEnable()
	{
		GameState.EventStartGame.Register(OnGameStarted);
	}

	private void OnDisable()
	{
		GameState.EventStartGame.Unregister(OnGameStarted);
	}

	void OnGameStarted(PlayerState playerState)
	{
		state = playerState;
		state.position = transform.position;
	}

	private void OnTriggerEnter(Collider other)
	{
		Collectable collectable = other.GetComponent<Collectable>();
		if(collectable != null)
		{
			collectable.OnPlayerEnter(this);
		}
	}

}
