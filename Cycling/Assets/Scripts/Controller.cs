using UnityEngine;

public class Controller : Singleton<Controller>
{

    public float speed = 0;
    public float angle = 90;

    public bool useKeyboard = true;

    public void SetInput(float _speed, float _angle)
    {
        //Hizi ve gidon acisini yolla,
        //Buradan sonrasi benim :)
        speed = _speed;
        angle = _angle;
    }

    #region Keyboard Input
    private void GetInput()
    {
        speed = Input.GetAxis("Vertical");
        angle = 90 + (Input.GetAxis("Horizontal")) * 90;
    }

    private void Update()
    {
        //if (Ardunio.Instance.serialPortController.IsInitialized && !Ardunio.Instance.serialPortController.ConnectionClosed)
        if(Ardunio.Instance.Ready())
        {
            speed = Ardunio.Instance.speed;
            angle = Ardunio.Instance.angle;
        }
        else
        {
            GetInput();
        }
            
    }
    #endregion

}
