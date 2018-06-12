using UnityEngine;
using System.IO;
using System.Xml;
using System;

public class CustomizationItem
{
    public Sprite sprite;
    public string name;
    public string description;

    public CustomizationItem(string path)
    {
        if (!Directory.Exists(path))
        {
            Debug.LogError("No such directory: " + path);
            return;
        }

        if (File.Exists(path + "/Description.xml"))
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path + "/Description.xml");
            XmlNode xNode = xDoc.SelectSingleNode("Item");
            if (xNode == null)
                Debug.LogError("No node with name 'Item' is found in " + path + "/Description.xml");
            else
            {
                name = xNode.Attributes["name"].Value;
                description = xNode.InnerText;
                Debug.Log(name + " - " + description);
            }
        }
        else
            Debug.LogError("No Description.xml file in " + path + "/Description.xml");

        string[] sprites = Directory.GetFiles(path, "*.png", SearchOption.TopDirectoryOnly);
        if (sprites == null || sprites.Length == 0)
        {
            Debug.Log("No *.png images of the product in " + path);
            sprite = new Sprite();
        }
        else
        {
            try
            {
                Texture2D tex = LoadTexture(Application.dataPath + "/Resources/Cosmetics/" + sprites[0].GetLast('/'));
                sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
                Debug.Log("Created: " + sprites[0].GetLast('\\').Split('.')[0]);
            }
            catch (Exception e)
            {
                Debug.LogError("Something happend during image loading at " + sprites[0] + "\nResources path: " + "Cosmetics/" + sprites[0].GetLast('/').Split('.')[0] + "\n" + e);
            }
        }
    }

    public Texture2D LoadTexture(string FilePath)
    {

        // Load a PNG or JPG file from disk to a Texture2D
        // Returns null if load fails

        Texture2D Tex2D;
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
            if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
                return Tex2D;                 // If data = readable -> return texture
        }
        return null;                     // Return null if load failed
    }
}
