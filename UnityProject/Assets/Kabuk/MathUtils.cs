using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtils 
{
    public static Vector3 ProjectPointToLine(Vector3 point, Vector3 lineP1, Vector3 lineP2)
    {
        // P point
        // AB line

        // A + dot(AP, AB) / dot(AB, AB) * AB

        Vector3 p1p2 = lineP2 - lineP1;
        Vector3 lineP1Point = point - lineP1;

        return lineP1 + Vector3.Dot(lineP1Point, p1p2) / Vector3.Dot(p1p2, p1p2) * p1p2;
    }


    public static Vector3 ProjectPointToLineSegment(Vector3 point, Vector3 lineP1, Vector3 lineP2)
    {
        Vector3 projection = ProjectPointToLine(point, lineP1, lineP2);
        bool between = Vector3.Dot(lineP1 - projection, lineP2 - projection) < 0;
        if (between)
        {
            return projection;
        }

        float disP1 = (point - lineP1).sqrMagnitude;
        float disP2 = (point - lineP2).sqrMagnitude;

        if (disP1 < disP2)
        {
            return lineP1;

        }
        else
        {
            return lineP2;
        }
    }
	
}
