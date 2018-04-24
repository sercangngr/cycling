using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kabuk;

[UnitySingleton(UnitySingletonAttribute.Type.ExistsInScene)]
public class Marks : UnitySingleton<Marks> 
{

    public Transform[] marks;

    Vector3 closestProj;

    public float GetDistance(Vector3 pos)
    {
        int refPoint = -1;
        float minDistance = float.MaxValue;

        for (int i = 0; i < marks.Length - 1; i++)
        {

            Vector3 proj = MathUtils.ProjectPointToLineSegment(pos, marks[i].position, marks[i + 1].position);
            float dis = (pos - proj).magnitude;
            if (dis < minDistance)
            {
                closestProj = proj;
                minDistance = dis;
                refPoint = i + 1;
            }
        }

 
        float distance = (pos - marks[refPoint].position).magnitude;
        for (int i = refPoint; i < marks.Length - 1; i++)
        {
            distance += (marks[i].position - marks[i+1].position).magnitude;
        }



        return distance;
        
    }


	private void OnDrawGizmos()
	{
        for (int i = 0; i < marks.Length - 1; i++)
        {
            Gizmos.DrawLine(marks[i].position, marks[i + 1].position);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(closestProj, 10);
        }
	}


}
