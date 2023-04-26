using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField]
    private Unit[] _units;
    [SerializeField]
    private List<Unit> _selectedUnits = new();

    public Unit[] PlayerUnits => _units;

    public List<Unit> SelectedUnits
    {
        get { return _selectedUnits; }
        set { _selectedUnits = value; }
    }
}
