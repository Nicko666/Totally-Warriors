using System.Collections.Generic;
using UnityEngine;

public class LinePositions : MonoBehaviour, IWarriorsPositions
{
    public Vector3[] GetPositions(int count)
    {
        List<Vector3> result = new List<Vector3>();

        float lenght = 2;

        float step = lenght / count - 1; 

        for (int i = 0; i < count; i++)
        {
            result.Add(new Vector3((-lenght / 2) + (i * step), 0, 0));
        }

        return result.ToArray();

    }
}
