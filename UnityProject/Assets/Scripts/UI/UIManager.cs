using UnityEngine;

public enum Panel
{
    InGame,
    Pause,
    None
}

public class UIManager : MonoBehaviour
{

    private static InGamePanel inGamePanel;

    void Start()
    {
        Utils.SetObject(out inGamePanel);
    }

    public static void SetPanel(Panel type)
    {
        inGamePanel.gameObject.SetActive(type == Panel.InGame);
    }
}
