using UnityEditor;

public class ScriptBatch
{
    [MenuItem("DecathRace/Build")]
    public static void BuildGame()
    {
        // Get filename.
        string path = EditorUtility.SaveFolderPanel("Choose Location of Built Game", "", "");
        string[] levels = new string[] { "Assets/Scenes/Main.unity" };

        // Build player.
        BuildPipeline.BuildPlayer(levels, path + "/DecathRace.exe", BuildTarget.StandaloneWindows, BuildOptions.None);

        // Copy a file from the project folder to the build folder, alongside the built game.
        FileUtil.CopyFileOrDirectory("Assets/Resources/Cosmetics", path + "/DecathRace_Data/Resources/Cosmetics");
    }
}
