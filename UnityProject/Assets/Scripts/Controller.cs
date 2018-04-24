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
            if(Ardunio.Instance.bicyclePort.IsOpen)
        {
            speed = Ardunio.Instance.speed;
            speed = ReMap(speed, 0, 500);
            angle = Ardunio.Instance.angle - 30;
        }
        else
        {
            GetInput();
        }
            
    }
    #endregion

    float ReMap(float value, float min, float max)
    {
        float t = (value - min) / (max - min);
        t = Mathf.Clamp01(t);
        return t;
    }
}
