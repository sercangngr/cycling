using UnityEngine;
using UnityEngine.UI;

public class CustomizationPanel : MonoBehaviour
{
    [SerializeField]
    private Button play;
    [SerializeField]
    private Button back;

    void Start()
    {
        play.onClick.AddListener(OnPlay);
        back.onClick.AddListener(OnBack);
    }

    private void OnPlay()
    {
        UIManager.SetPanel(Panel.InGame);
        EventManager.requestRestart.Invoke();
    }

    private void OnBack()
    {
        UIManager.SetPanel(Panel.MainMenu);
    }
}
