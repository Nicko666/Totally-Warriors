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
            unit.SetPosition(_target.position);
        }
    }
}
