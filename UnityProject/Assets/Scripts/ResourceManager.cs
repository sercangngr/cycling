using UnityEngine;

public static class ResourceManager
{
    public static GameObject GetStartPos()
    {
        return Object.Instantiate(Resources.Load<GameObject>("Prefabs/StartPos"));
    }

    public static ItemPanel GetItemPanel()
    {
        return (Object.Instantiate(Resources.Load("Prefabs/UI/ItemPanel")) as GameObject).GetComponent<ItemPanel>();
    }
}
