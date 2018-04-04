using UnityEngine;

public class Player : MonoBehaviour
{
    private Controller cont;
    private Rigidbody rigid;
    private bool canControl = true;

    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float rotation = 1;

    private Vector3 currentVel = Vector3.zero;

    private void Awake()
    {
        EventManager.gameOver.AddListener(GameOver);
        EventManager.restartRace.AddListener(RestartRace);
        EventManager.pause.AddListener(Pause);
        EventManager.start.AddListener(StartRace);

        cont = Controller.Instance;
        Utils.SetComponent(out rigid, transform, true);
    }

    private void Move()
    {
        Vector3 vel = transform.forward * speed * cont.speed * Time.deltaTime;
        vel.y = rigid.velocity.y;
        rigid.velocity = vel;
    }

    private void Rotate()
    {
        Vector3 rot = rigid.rotation.eulerAngles + new Vector3(0, cont.angle - 90, 0) * Time.deltaTime * rotation;
        rigid.MoveRotation(Quaternion.Euler(rot));
    }

    private void Jump()
    {
        rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);
    }

    private void Pause(bool state)
    {
        canControl = !state;
        if(!canControl)
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
    }

    private void RestartRace(Transform startPos)
    {
        rigid.position = startPos.position;
        rigid.rotation = startPos.rotation;
        rigid.velocity = Vector3.zero;
        rigid.isKinematic = false;
        canControl = false;
    }

    private void StartRace()
    {
        canControl = true;
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
    }
}
