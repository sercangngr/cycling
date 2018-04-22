using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ShareAndRate
{

    public static void Rate()
    {
#if UNITY_ANDROID
        Application.OpenURL("market://details?id=com.yush.android");
#elif UNITY_IPHONE
        Application.OpenURL("itms-apps://itunes.apple.com/app/id1329506895");
#endif

    }

    public static void OpenFacebookPage()
    {
        Application.OpenURL("https://www.facebook.com/MercilessPodium/");
    }

    public static void OpenTwitterPage()
    {
        Application.OpenURL("https://twitter.com/KabukGames");
    }

    public static void ShareText(string subject, string text)
    {
#if UNITY_ANDROID
        //Reference of AndroidJavaClass class for intent
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        //Reference of AndroidJavaObject class for intent
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
        //call setAction method of the Intent object created
        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        //set the type of sharing that is happening
        intentObject.Call<AndroidJavaObject>("setType", "text/plain");
        //add data to be passed to the other activity i.e., the data to be sent
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
        //intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), "Text Sharing ");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), text);
        //get the current activity
        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        //start the activity by sending the intent data
        AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Via");
        currentActivity.Call("startActivity", jChooser);
#elif UNITY_IPHONE || UNITY_IPAD
        NativeShare.Share(text, subject: subject, chooserText: "");
#endif
    }

    public static void ShareLink()
    {
#if UNITY_ANDROID
        ShareText("Yush", "https://play.google.com/store/apps/details?id=com.yush.android");
        #elif UNITY_IPHONE || UNITY_IPAD
        ShareText("Yush", "https://itunes.apple.com/app/id1329506895");
#endif
    }

}
