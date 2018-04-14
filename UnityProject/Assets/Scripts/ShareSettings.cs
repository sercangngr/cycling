using System;
using System.IO;
using System.Xml;
using UnityEngine;

public static class ShareSettings
{
    private static string faceLink;

    public static string GetTwitterLink()
    {
        string path = Application.dataPath + "/Resources/ShareSettings.xml";

        if (!File.Exists(path))
        {
            Debug.LogError("No ShareSettings.xml file in " + Application.dataPath + "/Resources/. Cannot generate a message...");
            return "https://twitter.com/intent/tweet?via=game0wer&text=They%20forgot%20to%20put%20the%20ShareSettings%20file%20to%20the%20correct%20location";
        }

        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(path);
        XmlNode root = xDoc.SelectSingleNode("Root");
        if (root == null)
        {
            Debug.LogError("No root node in the ShareSettings.xml file! Fix it: " + Application.dataPath + "/Resources/");
            return "https://twitter.com/intent/tweet?via=game0wer&text=ShareSettings%20seems%20to%20be%20broken%20(no%20root%20node)";
        }


        XmlNode tweetNode = root.SelectSingleNode("Twitter");
        if (tweetNode == null)
        {
            Debug.LogError("No Twitter node under the Root in the ShareSettings.xml file! Fix it: " + Application.dataPath + "/Resources/");
            return "https://twitter.com/intent/tweet?via=game0wer&text=ShareSettings%20seems%20to%20be%20broken%20(no%20Twitter%20node)";
        }

        string msg = "No message";
        try
        {
            msg = tweetNode.Attributes["msg"].Value;
        }
        catch (Exception e)
        {
            Debug.LogError("No message attribute in Twitter node! Fix it: " + Application.dataPath + "/Resources/");
            Debug.LogError(e);
            return "https://twitter.com/intent/tweet?via=game0wer&text=ShareSettings%20seems%20to%20be%20broken%20(no%20msg%20attribute%20in%20Twitter%20node)";
        }

        msg = msg.Replace(" ", "%20");

        string via = "game0wer";
        try
        {
            via = tweetNode.Attributes["via"].Value;
        }
        catch (Exception e)
        {
            Debug.LogError("No via attribute in Twitter node! Fix it: " + Application.dataPath + "/Resources/");
            Debug.LogError(e);
            return "https://twitter.com/intent/tweet?via=game0wer&text=ShareSettings%20seems%20to%20be%20broken%20(no%20via%20attribute%20in%20Twitter%20node)";
        }

        return "https://twitter.com/intent/tweet?via=" + via + "&text=" + msg;
    }

    public static string GetFacebookLink()
    {
        string path = Application.dataPath + "/Resources/ShareSettings.xml";

        if (!File.Exists(path))
        {
            Debug.LogError("No ShareSettings.xml file in " + Application.dataPath + "/Resources/. Cannot generate a message...");
            return "https://www.facebook.com/sharer/sharer.php?u=anton.website&quote=They%20forgot%20to%20put%20the%20ShareSettings%20file%20to%20the%20correct%20location";
        }

        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(path);
        XmlNode root = xDoc.SelectSingleNode("Root");
        if (root == null)
        {
            Debug.LogError("No root node in the ShareSettings.xml file! Fix it: " + Application.dataPath + "/Resources/");
            return "https://www.facebook.com/sharer/sharer.php?u=anton.website&quote=ShareSettings%20seems%20to%20be%20broken%20(no%20root%20node)";
        }

        XmlNode xNode = root.SelectSingleNode("Facebook");
        if(xNode == null)
        {
            Debug.LogError("No Facebook node under the Root in the ShareSettings.xml file! Fix it: " + Application.dataPath + "/Resources/");
            return "https://www.facebook.com/sharer/sharer.php?u=anton.website&quote=ShareSettings%20seems%20to%20be%20broken%20(no%20Facebook%20node)";
        }

        string msg = "No message";
        try
        {
            msg = xNode.Attributes["msg"].Value;
        }
        catch (Exception e)
        {
            Debug.LogError("No message attribute in Facebook node! Fix it: " + Application.dataPath + "/Resources/");
            Debug.LogError(e);
            return "https://www.facebook.com/sharer/sharer.php?u=anton.website&quote=ShareSettings%20seems%20to%20be%20broken%20(no%20msg%20attribute%20in%20Facebook%20node)";
        }

        msg = msg.Replace(" ", "%20");

        string website = "anton.website";
        try
        {
            website = xNode.Attributes["website"].Value;
        }
        catch (Exception e)
        {
            Debug.LogError("No website attribute in Facebook node! Fix it: " + Application.dataPath + "/Resources/");
            Debug.LogError(e);
            return "https://www.facebook.com/sharer/sharer.php?u=anton.website&quote=ShareSettings%20seems%20to%20be%20broken%20(no%20website%20attribute%20in%20Facebook%20node)";
        }

        return "https://www.facebook.com/sharer/sharer.php?u=" + website + "&quote=" + msg;
    }
}
