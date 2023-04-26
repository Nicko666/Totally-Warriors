using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField]
    private Unit[] _units;

    [SerializeField]
    private Transform _target;

    private void Update()
    {
        foreach (var unit in _units)
        {
            unit.SetUnitTarget(_target.position);
        }
    }
}
