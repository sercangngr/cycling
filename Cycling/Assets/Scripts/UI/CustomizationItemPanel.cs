using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationItemPanel : MonoBehaviour
{
    [SerializeField]
    private string fileName;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Text text;
    [SerializeField]
    private Button next;
    [SerializeField]
    private Button previous;

    private CustomizationItem[] items;
    private int currentIndex = 0;

    private void Start()
    {
        if(!Directory.Exists(Application.dataPath + "/Resources/Cosmetics/" + fileName))
        {
            Debug.LogError("No such directory exists: " + Application.dataPath + "/Resources/Cosmetics/" + fileName);
            enabled = false;
            return;
        }

        string[] dirs = Directory.GetDirectories(Application.dataPath + "/Resources/Cosmetics/" + fileName);
        if(dirs.Length == 0)
        {
            Debug.LogError("No items in " + Application.dataPath + "/Resources/Cosmetics/" + fileName);
            enabled = false;
            return;
        }

        items = new CustomizationItem[dirs.Length];
        for (int i = 0; i < dirs.Length; i++)
            items[i] = new CustomizationItem(dirs[i]);

        currentIndex = 0;
        SetItem(items[currentIndex]);

        next.onClick.AddListener(OnNext);
        previous.onClick.AddListener(OnPrevious);
    }

    private void SetItem(CustomizationItem item)
    {
        text.text = item.description;
        image.sprite = item.sprite;
    }

    private void OnNext()
    {
        if (++currentIndex >= items.Length)
            currentIndex = 0;

        SetItem(items[currentIndex]);
    }

    private void OnPrevious()
    {
        if (--currentIndex < 0)
            currentIndex = items.Length - 1;

        SetItem(items[currentIndex]);
    }
}
