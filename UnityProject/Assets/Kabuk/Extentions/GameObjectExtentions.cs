using UnityEngine;
using System;

public static class GameObjectExtentions
{
    public static T AddComponentOnce<T>(this GameObject go) where T:class
	{
        T c = go.GetComponent<T>();
        if(c == null)
        {
            return go.AddComponent(typeof(T)) as T;
        }
        return c;
	}

    public static GameObject NewGameObject(string name)
    {
        GameObject go = new GameObject(name);
        // set position and rotation to (0,0,0)
        // set scale to (1,1,1)
        go.transform.Reset();
        return go;
    }
}
