using UnityEngine;

public enum Panel
{
    InGame,
    Pause,
    MainMenu,
    Customization,
    Score,
    QR,
    None
}

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Panel defaultPanel = Panel.MainMenu;

    private static InGamePanel inGamePanel;
    private static MainMenuPanel mainMenu;
    private static CustomizationPanel customizationPanel;
    private static ScorePanel scorePanel;
    private static PausePanel pausePanel;
    private static QRPanel qrPanel;

    void Start()
    {
        Utils.SetObject(out inGamePanel);
        Utils.SetObject(out pausePanel);
        Utils.SetObject(out mainMenu);
        Utils.SetObject(out customizationPanel);
        Utils.SetObject(out scorePanel);
        Utils.SetObject(out qrPanel);

        SetPanel(defaultPanel);
    }

    public static void GenerateQR(string twitterLing, string facebookLing)
    {
        qrPanel.GenerateQR(twitterLing, facebookLing);
        SetPanel(Panel.QR);
    }

    public static void SetPanel(Panel type)
    {
        inGamePanel.gameObject.SetActive(type == Panel.InGame);
        pausePanel.gameObject.SetActive(type == Panel.Pause);
        mainMenu.gameObject.SetActive(type == Panel.MainMenu);
        customizationPanel.gameObject.SetActive(type == Panel.Customization);
        scorePanel.gameObject.SetActive(type == Panel.Score);
        qrPanel.gameObject.SetActive(type == Panel.QR);
    }
}
