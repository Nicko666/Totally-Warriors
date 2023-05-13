using System.Collections.Generic;
using UnityEngine;

public class TacticalUI : MonoBehaviour
{
    [field: SerializeField] public UnitsCanvas UpUnitsCanvas { get; private set; }
    [field: SerializeField] public UnitsCanvas LowUnitsCanvas { get; private set; }
    [field: SerializeField] public TacticalStatus tacticalStatus { get; private set; }

    List<UnitTObject> _playerUnits;
    List<UnitTObject> _aiUnits;

    public void SetStatus(List<UnitTObject> playerUnits, List<UnitTObject> aiUnits)
    {
        _playerUnits = playerUnits;// ?? new List<UnitTObject>();
        _aiUnits = aiUnits;// ?? new List<UnitTObject>();

        foreach (var unit in _playerUnits) unit.DefeatedAction += UpdateStatus;
        foreach (var unit in _aiUnits) unit.DefeatedAction += UpdateStatus;

        tacticalStatus.SetColors(playerUnits[0].Color, aiUnits[0].Color);

        tacticalStatus.SetNumber(_playerUnits.Count, _aiUnits.Count);

    }

    void UpdateStatus(UnitTObject unitTObject)
    {
        tacticalStatus.SetNumber(_playerUnits.Count, _aiUnits.Count);

    }

}
