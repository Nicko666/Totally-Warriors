using System.Collections.Generic;
using UnityEngine;

public class BehaviourPlayerTactic : MonoBehaviour, IBehaviorTactical
{
    private List<Unit> SelectedUnits;

    private SceneTactical sceneTactical;

    private void OnEnable()
    {
        sceneTactical = SceneTactical.Instance;

        SelectedUnits = new List<Unit>();
        sceneTactical.MapClick += OnMapMouseUp;
        sceneTactical.UnitClick += OnUnitPointerClick;
    }

    private void OnDisable()
    {
        if (sceneTactical != null)
        {
            sceneTactical.MapClick -= OnMapMouseUp;
            sceneTactical.UnitClick -= OnUnitPointerClick;

        }

    }

    void OnMapMouseUp(Vector3 position)
    {
        foreach (var unit in SelectedUnits)
        {
            unit.Destination(position);
        }
    }

    void OnUnitPointerClick(Unit unit)
    {
        if (unit.Name == SceneTactical.Instance.CharacterPlayer.Name)
        {
            OnSelectAlly(unit);
        }
        else
        {
            OnSelectEnemy(unit);
        }

    }

    void OnSelectEnemy(Unit enemy)
    {
        foreach (var unit in SelectedUnits)
        {
            unit.Enemy(enemy);
        }

    }

    void OnSelectAlly(Unit unit)
    {
        if (SelectedUnits.Contains(unit))
        {
            unit.Selected(false);
            SelectedUnits.Remove(unit);
            return;
        }

        unit.Selected(true);
        SelectedUnits.Add(unit);

    }
}
