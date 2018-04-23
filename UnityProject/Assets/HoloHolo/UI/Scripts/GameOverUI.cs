using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameOverUI : MonoBehaviour 
{
    public Image qrCode;
    public Text text;

    Sprite facebookQr;
    Sprite twitterQr;

    const string facebookText = "FACEBOOK'TA PAYLAŞ";
    const string twitterText = "TWITTER'DA PAYLAŞ";
    bool facebook = true;

	private void Awake()
	{
        
        string twitterLink = GetTwitterLink();
        string facebookLink = GetFacebookLink();

        twitterQr = Sprite.Create(QRManager.GenerateQR(twitterLink), new Rect(Vector2.zero, Vector2.one * 256), Vector2.zero);
        facebookQr = Sprite.Create(QRManager.GenerateQR(facebookLink), new Rect(Vector2.zero, Vector2.one * 256), Vector2.zero);

        SetFacebook();
	}

    string GetFacebookLink()
    {
        return "https://twitter.com/intent/tweet?via=";// + via + "&text=" + msg;
    }

    string GetTwitterLink()
    {
        return "https://www.facebook.com/sharer/sharer.php?u=";// + website + "&quote=" + msg;
    }

	private void OnEnable()
	{
        Ardunio.EventButtonLeft.Register(OnLeftButtonPressed);
        Ardunio.EventButtonRight.Register(OnRightButtonPressed);
        Ardunio.EventButtonCheck.Register(OnCheckButtonPressed);
	}

	private void OnDisable()
	{
        Ardunio.EventButtonLeft.Unregister(OnLeftButtonPressed);
        Ardunio.EventButtonRight.Unregister(OnRightButtonPressed);
        Ardunio.EventButtonCheck.Unregister(OnCheckButtonPressed);
	}


    void OnLeftButtonPressed()
    {
        Switch();
    }

    void OnRightButtonPressed()
    {
        Switch();
    }

    void Switch()
    {
        if (facebook)
        {
            SetTwitter();
        }
        else
        {
            SetFacebook();
        }
        facebook = !facebook;
    }

    void OnCheckButtonPressed()
    {
        Debug.Log("Restart");
    }

    void SetFacebook()
    {
        text.text = facebookText;
        qrCode.sprite = facebookQr;
    }

    void SetTwitter()
    {
        text.text = twitterText;
        qrCode.sprite = twitterQr;
    }

}
