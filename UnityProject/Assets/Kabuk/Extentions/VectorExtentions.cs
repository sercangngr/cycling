using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtentions 
{
    public static Vector3 ElementWise (this Vector3 vec, Vector3 other)
    {
        return new Vector3(vec.x * other.x, vec.y * other.y, vec.z * other.z);
    }

    public static Vector2 ElementWise(this Vector2 vec, Vector2 other)
    {
        return new Vector2(vec.x * other.x, vec.y * other.y);
    }
	
}
