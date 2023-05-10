using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BehaviourPlayerTactic : MonoBehaviour, IBehaviorTactical
{
    Character _character;
    List<Unit> _selectedUnits;
    IUnitFormation _unitFormation;

    public void Enable(List<Unit> units)
    {
        _character = GetComponent<Character>();
        if (_selectedUnits == null) _selectedUnits = new List<Unit>();
        _unitFormation = GetComponent<CircleFormation>();

        foreach (Unit unit in units)
        {
            unit.UnitObj.HoldPositions = true;
        }
    }

    public void OnMapClick(Vector3 position)
    {
        if (_selectedUnits.Count > 0)
        {
            var positions = _unitFormation.GetPositions(_selectedUnits.Count);
            for (int i = 0; i < positions.Length; i++)
            {
                _selectedUnits[i].UnitObj.Destination(position + (positions[i] * 1));
            }
        }
        
    }

    public void OnUnitClick(Unit unit)
    {
        if (unit.Name == _character.Name)
        {
            OnSelectAlly();
        }
        else
        {
            OnSelectEnemy();
        }

        void OnSelectEnemy()
        {
            foreach (var myunit in _selectedUnits)
            {
                myunit.UnitObj.Enemy(unit.UnitObj);
            }
        }

        void OnSelectAlly()
        {
            if (_selectedUnits.Contains(unit))
            {
                SelectUnit(unit, false);
            }
            else
            {
                SelectUnit(unit, true);
            }
        }

    }

    public void OnUnitDefeated(Unit unit)
    {
        if (_selectedUnits.Contains(unit))
        {
            SelectUnit(unit, false);
        }
    }

    void SelectUnit(Unit unit, bool value)
    {
        unit.SelectedAction(value);

        if(value)
        {
            _selectedUnits.Add(unit);
            unit.UnitDefeated += OnUnitDefeated;
        }
        else
        {
            _selectedUnits.Remove(unit);
            unit.UnitDefeated -= OnUnitDefeated;
        }

    }

}
