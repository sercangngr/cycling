using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameOverUI : MonoBehaviour 
{
    public Image qrCode;
    public Text headerText;
    public Text text;

    Sprite facebookQr;
    Sprite twitterQr;

    const string facebookText = "FACEBOOK'TA PAYLAŞ";
    const string twitterText = "TWITTER'DA PAYLAŞ";
    bool facebook = true;

	private void Awake()
	{
        
        string twitterLink = GetTwitterLink();
        Debug.Log(twitterLink);
        string facebookLink = GetFacebookLink();
        Debug.Log(facebookLink);

        twitterQr = Sprite.Create(QRManager.GenerateQR(twitterLink), new Rect(Vector2.zero, Vector2.one * 256), Vector2.zero);
        facebookQr = Sprite.Create(QRManager.GenerateQR(facebookLink), new Rect(Vector2.zero, Vector2.one * 256), Vector2.zero);

        SetFacebook();

        if(GameState.Instance.timeLeft <= 0)
        {
            headerText.text = "ÜZGÜNÜM ZAMANIN DOLDU SKORUNU PAYLAŞ";
        }else if (GameState.Instance.energyLeft <= 0)
        {
            headerText.text = "ÜZGÜNÜM ENERJİN BİTTİ SKORUNU PAYLAŞ";
        }else
        {
            headerText.text = "TEBRİKLER İYİ BİR İŞ ÇIKARTTIN SKORUNU PAYLAŞ";
        }
	}

    string GetFacebookLink()
    {
        string website = "https://www.decathlon.com.tr/";
        string msg = "DecathRace%20skorum%20" + GameState.Instance.score;
        return "https://www.facebook.com/sharer/sharer.php?u=" + website + "&quote=" + msg;
    }

    string GetTwitterLink()
    {
        
        string msg = "DecathRace%20skorum%20" + GameState.Instance.score;
        string via = "Decathlon";

        return "https://twitter.com/intent/tweet?via=" + via + "&text=" + msg;
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
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
