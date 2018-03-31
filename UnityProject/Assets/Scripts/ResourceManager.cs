using UnityEngine;

public static class ResourceManager
{
    public static GameObject GetStartPos()
    {
        return Object.Instantiate(Resources.Load<GameObject>("Prefabs/StartPos"));
    }
}
