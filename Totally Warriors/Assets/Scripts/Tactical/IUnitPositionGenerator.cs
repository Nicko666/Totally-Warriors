using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitPositionGenerator
{
    Vector3[] GetPosition(int count);
}
