using UnityEngine;
using UnityEngine.UI;

public class QRPanel : MonoBehaviour
{
    [SerializeField]
    private Image twitterQR;
    [SerializeField]
    private Image faceQR;
    [SerializeField]
    private Button done;

    private void Start()
    {
        done.onClick.AddListener(OnDone);
    }

    private void OnDone()
    {
        UIManager.SetPanel(Panel.MainMenu);
    }

    public void GenerateQR(string twitterText, string faceText)
    {
        GenerateTwitterQR(twitterText);
        GenerateFaceQR(faceText);
    }

    private void GenerateTwitterQR(string text)
    {
        twitterQR.sprite = Sprite.Create(QRManager.GenerateQR(text), new Rect(Vector2.zero, Vector2.one * 256), Vector2.zero);
    }

    private void GenerateFaceQR(string text)
    {
        faceQR.sprite = Sprite.Create(QRManager.GenerateQR(text), new Rect(Vector2.zero, Vector2.one * 256), Vector2.zero);
    }
}
