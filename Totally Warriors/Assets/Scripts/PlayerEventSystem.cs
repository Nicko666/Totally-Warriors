using System.Collections.Generic;
using UnityEngine;

public class PlayerEventSystem : MonoBehaviour
{
    List<UnitTObject> SelectedUnits = new List<UnitTObject>();
    string _name;
    IUnitFormation _unitFormation;

    public void Inst(string name)
    {
        _name = name;

        _unitFormation = GetComponent<CircleFormation>();
    }


    public void OnPlayerUnitClick(UnitTObject unitTObject)
    {
        if (SelectedUnits.Contains(unitTObject))
        {
            Remove(unitTObject);
        }
        else
        {
            Add(unitTObject);
        }

    }

    public void OnAIUnitClick(UnitTObject unitTObject)
    {
        foreach (var unit in SelectedUnits)
        {
            unit.Attack(unitTObject.Warriors);
        }
    }

    void Add(UnitTObject unitTObject)
    {
        SelectedUnits.Add(unitTObject);
        unitTObject.Selected?.Invoke(true);
        unitTObject.DefeatedAction += Remove;
    }

    void Remove(UnitTObject unitTObject)
    {
        SelectedUnits.Remove(unitTObject);
        unitTObject.Selected?.Invoke(false);
        unitTObject.DefeatedAction -= Remove;
    }


    public void OnMapClick(Vector3 position)
    {
        if (SelectedUnits.Count < 1)
        {
            return;
        }
        else if (SelectedUnits.Count == 1)
        {
            SelectedUnits[0].SetDestination(position);
            SelectedUnits[0].ShowWarriorsDestanations();
            return;
        }

        var positions = _unitFormation.GetPositions(SelectedUnits.Count);

        for (int i = 0; i < positions.Length; i++)
        {
            SelectedUnits[i].SetDestination(position + (positions[i] * 1));
            SelectedUnits[i].ShowWarriorsDestanations();
        }

    }

}
