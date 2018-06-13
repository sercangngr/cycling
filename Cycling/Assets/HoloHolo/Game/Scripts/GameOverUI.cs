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

        SoundManager.instance.InGameAudio.Stop();

        SetFacebook();

		if(GameState.Instance.playerState.timeLeft <= 0)
        {
            headerText.text = "ÜZGÜNÜM ZAMANIN DOLDU SKORUN " + GameState.Instance.GetScore() + " PAYLAŞ";
		}else if (GameState.Instance.playerState.energyLeft <= 0)
        {
            headerText.text = "ÜZGÜNÜM ENERJİN BİTTİ SKORUN " + GameState.Instance.GetScore() + "  PAYLAŞ";
        }else
        {
            headerText.text = "TEBRİKLER İYİ BİR İŞ ÇIKARTTIN SKORU " + GameState.Instance.GetScore() + "  PAYLAŞ";
        }
	}

    string GetFacebookLink()
    {
        string website = "https://www.decathlon.com.tr/";
        string msg = "DecathRace%20skorum%20" + GameState.Instance.GetScore();
        return "https://www.facebook.com/sharer/sharer.php?u=" + website + "&quote=" + msg;
    }

    string GetTwitterLink()
    {
        
        string msg = "DecathRace%20skorum%20" + GameState.Instance.GetScore();
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
		EventManager.Restart ();
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
