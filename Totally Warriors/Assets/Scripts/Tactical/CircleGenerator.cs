using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGenerator : MonoBehaviour, IUnitPositionGenerator
{
    public Vector3[] GetPosition(int count)
    {
        float step = (Mathf.Deg2Rad * 360) / count;
        List<Vector3> result = new List<Vector3>();

        for(int i = 0; i < count; i++)
        {
            result.Add(new Vector3(Mathf.Sin(i * step), 0, Mathf.Cos(i * step)));
        }

        return result.ToArray();
    }
}
