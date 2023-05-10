using System.Collections.Generic;
using UnityEngine;

public class CircleFormation : MonoBehaviour, IUnitFormation
{
    public float distance;

    public Vector3[] GetPositions(int count)
    {
        if (count == 1)
        {
            return new Vector3[1] { Vector3.zero };
        }

        float step = (Mathf.Deg2Rad * 360) / count;
        List<Vector3> result = new List<Vector3>();

        for(int i = 0; i < count; i++)
        {
            result.Add(new Vector3(Mathf.Sin(i * step), 0, Mathf.Cos(i * step)) * distance);
        }

        return result.ToArray();
    }
}
