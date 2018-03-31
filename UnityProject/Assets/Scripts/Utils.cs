using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Utils
{
    public static bool SetObject<T>(out T obj) where T : Component
    {
        obj = UnityEngine.Object.FindObjectOfType<T>();
        if (obj == null)
        {
            Debug.LogError("Cannot find object of type " + typeof(T));
            return false;
        }
        return true;
    }

    public static bool SetObjectWithTag(out GameObject obj, string tag)
    {
        obj = GameObject.FindWithTag(tag);
        if(obj == null)
        {
            Debug.LogError("Cannot find object with tag " + tag);
            return false;
        }
        return true;
    }

    public static bool SetComponent<T>(out T obj, Transform t, bool needWarning = false) where T : Component
    {
        obj = t.GetComponent<T>();
        if (obj == null && needWarning)
            Debug.LogError("Cannot find component " + typeof(T) + " on " + t.name);
        return obj != null;
    }

    public static IEnumerable<string> SplitInParts(this string s, Int32 partLength)
    {
        if (s == null)
            throw new ArgumentNullException("s");
        if (partLength <= 0)
            throw new ArgumentException("Part length has to be positive.", "partLength");

        for (var i = 0; i < s.Length; i += partLength)
            yield return s.Substring(i, Math.Min(partLength, s.Length - i));
    }

    // The Random object this method uses.
    private static System.Random Rand = null;

    // Return num_items random values.
    public static List<T> PickRandom<T>(
        this T[] values, int num_values)
    {
        // Create the Random object if it doesn't exist.
        if (Rand == null) Rand = new System.Random();

        // Don't exceed the array's length.
        if (num_values >= values.Length)
            num_values = values.Length - 1;

        // Make an array of indexes 0 through values.Length - 1.
        int[] indexes =
            Enumerable.Range(0, values.Length).ToArray();

        // Build the return list.
        List<T> results = new List<T>();

        // Randomize the first num_values indexes.
        for (int i = 0; i < num_values; i++)
        {
            // Pick a random entry between i and values.Length - 1.
            int j = Rand.Next(i, values.Length);

            // Swap the values.
            int temp = indexes[i];
            indexes[i] = indexes[j];
            indexes[j] = temp;

            // Save the ith value.
            results.Add(values[indexes[i]]);
        }

        // Return the selected items.
        return results;
    }
}
