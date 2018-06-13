using UnityEngine;
using UnityEngine.PostProcessing;

public class Player : MonoBehaviour
{
	public PlayerState state;
    public Rigidbody rigid;
	public GameObject torchlight;

    private Vector3 currentVel = Vector3.zero;

	const float MaxSpeed = 15;
    private void Move()
    {

		float speed = MaxSpeed
			* Ardunio.Instance.speed// * Time.deltaTime
		             * (state.insideRain ? PlayerState.RainSlowdownCoef : 1)
		             * (state.insideTunnel ? PlayerState.TunnelSlowdownCoef: 1);


		Vector3 vel = transform.forward.normalized * speed;

        vel.y = rigid.velocity.y;
        rigid.velocity = vel;
    }

    private void Rotate()
    {

        //Vector3 rot = rigid.rotation.eulerAngles + new Vector3(0, cont.angle - 90, 0) * Time.deltaTime * status.rotation;
        //rigid.MoveRotation(Quaternion.Euler(rot));
    }

    private void Jump()
    {
        rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);
    }


	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

}
