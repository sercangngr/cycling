using UnityEngine;

public static class ResourceManager
{
    public static GameObject GetStartPos()
    {
        return Object.Instantiate(Resources.Load<GameObject>("Prefabs/StartPos"));
    }

    public static GameObject GetBoulder()
    {
        return GetBoulder(Vector3.zero);
    }

    public static GameObject GetBoulder(Vector3 pos)
    {
        return Object.Instantiate(Resources.Load<GameObject>("Prefabs/Boulder"), pos, Quaternion.identity);
    }

    public static ItemPanel GetItemPanel()
    {
        return (Object.Instantiate(Resources.Load("Prefabs/UI/ItemPanel")) as GameObject).GetComponent<ItemPanel>();
    }
}
