using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
	public static void Reset(this Transform trans)
	{
		trans.position = Vector3.zero;
		trans.localRotation = Quaternion.identity;
		trans.localScale = new Vector3(1, 1, 1);
	}

	public static void DeactivateChildren(this Transform trans)
	{
		foreach (Transform t in trans)
		{
			t.gameObject.SetActive(false);
		}
	}

	public static void ActivateChildren(this Transform trans)
	{
		foreach (Transform t in trans)
		{
			t.gameObject.SetActive(true);
		}
	}

	public static void SetX(this Transform trans, float x)
	{
        trans.position = new Vector3(x, trans.position.y, trans.position.z);
	}

	public static void SetY(this Transform trans, float y)
	{
        trans.position = new Vector3(trans.position.x, y, trans.position.z);
	}

	public static void SetZ(this Transform trans, float z)
	{
        trans.position = new Vector3(trans.position.x, trans.position.y, z);
	}

    public static void SetXY(this Transform trans, float x, float y)
    {
        trans.position = new Vector3(x, y, trans.position.z);
    }

    public static void SetXZ(this Transform trans, float x, float z)
    {
        trans.position = new Vector3(x, trans.position.y, z);
    }

    public static void SetYZ(this Transform trans, float y, float z)
    {
        trans.position = new Vector3(trans.position.x, y, z);
    }

	public static void SetLocalX(this Transform trans, float x)
	{
        trans.localPosition = new Vector3(x, trans.localPosition.y, trans.localPosition.z);
	}

	public static void SetLocalY(this Transform trans, float y)
	{
        trans.localPosition = new Vector3(trans.localPosition.x, y, trans.localPosition.z);
	}

	public static void SetLocalZ(this Transform trans, float z)
	{
        trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y, z);
	}

    public static void SetLocalXY(this Transform trans, float x, float y)
    {
        trans.localPosition = new Vector3(x, y, trans.localPosition.z);
    }

    public static void SetLocalXZ(this Transform trans, float x, float z)
    {
        trans.localPosition = new Vector3(x, trans.localPosition.y, z);
    }

    public static void SetLocalYZ(this Transform trans, float y, float z)
    {
        trans.localPosition = new Vector3(trans.localPosition.x, y, z);
    }
}
