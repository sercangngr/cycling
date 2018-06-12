using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Kabuk
{
    public class ScreenshotHelper : EditorWindow
    {

        public string editorWindowText = "Choose a project name: ";
        string screenShotName = "";
        int id = 0;

        [MenuItem("Tools/KabukGames/ScreenShotHelper")]
        public static void ShowWindow()
        {
            EditorWindow window = GetWindow(typeof(ScreenshotHelper));
            window.Show();
        }

        void OnGUI()
        {
            string ssname = EditorGUILayout.TextField("ScreenShot Name:", screenShotName);
            if (!string.IsNullOrEmpty(ssname))
            {
                screenShotName = ssname;
            }

            GUILayout.Space(20);
            if (GUILayout.Button("Capture"))
            {
                if (string.IsNullOrEmpty(screenShotName))
                {
                    Debug.LogError("Please enter a screen shot name!");
                }
                else
                {
                    ScreenCapture.CaptureScreenshot(screenShotName + id + ".png");
                    id++;
                    Debug.Log("ScreenShot Captured " + screenShotName);

                }
            }
            this.Repaint();
        }


    }
}

