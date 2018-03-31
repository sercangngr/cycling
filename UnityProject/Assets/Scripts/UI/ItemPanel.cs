using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    [SerializeField]
    private Text function;
    [SerializeField]
    private Image image;

    public void Set(PowerUp pu)
    {
        function.text = pu.function;
        image.sprite = pu.sprite;
    }
}
