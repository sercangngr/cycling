using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{

    public static T Last<T>(this List<T> list)
    {
        return list[list.Count - 1];
    }

    public static void RemoveLast<T>(this List<T> list)
    {
        list.RemoveAt(list.Count - 1);
    }

    public static List<T> Copy<T>(this List<T> list)
	{
        List<T> copy = new List<T>();
        for (int i = 0; i < list.Count; i ++)
        {
            copy.Add(list[i]);
        }
        return copy;
	}

}
