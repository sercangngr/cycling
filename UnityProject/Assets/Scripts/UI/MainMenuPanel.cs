using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    [SerializeField]
    private Button newGame;
    [SerializeField]
    private Button score;

    void Start()
    {
        newGame.onClick.AddListener(OnNewGame);
        score.onClick.AddListener(OnScore);
    }

    private void OnNewGame()
    {
        UIManager.SetPanel(Panel.Customization);
    }

    private void OnScore()
    {
        Debug.LogError("Not yet implemented");
    }
}
