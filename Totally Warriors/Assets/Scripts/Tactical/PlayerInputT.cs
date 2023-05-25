using System.Collections.Generic;
using UnityEngine;

public class PlayerInputT : MonoBehaviour
{
    [SerializeField] CharacterManagerT characterManager;
    List<UnitT> SelectedUnits = new List<UnitT>();
    IUnitFormation _unitFormation;

    private void Awake()
    {
        _unitFormation = GetComponent<CircleFormation>();

    }

    private void OnEnable()
    {
        SceneTActions.Instance.OnUnitsTCreated += OnUnitCreated;
        SceneTManager.Instance.Map.Click += OnMapClick;
    }

    void OnUnitCreated()
    {
        foreach (var unit in characterManager.MyUnits)
        {
            unit.ClickAction += OnPlayerUnitClick;
            SceneTActions.Instance.OnUnitTDefeated += Deselect;
        }
        foreach (var unit in characterManager.EnemyUnits)
        {
            unit.ClickAction += OnAIUnitClick;
            SceneTActions.Instance.OnUnitTDefeated += Deselect;
        }

    }

    void OnPlayerUnitClick(UnitT unitT)
    {
        if (SelectedUnits.Contains(unitT))
        {
            Deselect(unitT);
        }
        else
        {
            Select(unitT);
        }

    }

    void OnAIUnitClick(UnitT unitT)
    {
        foreach (var unit in SelectedUnits)
        {
            unit.SetAttackBehavior(unitT);
        }
    }

    void Select(UnitT unitT)
    {
        SelectedUnits.Add(unitT);
        unitT.Selected?.Invoke(true);
    }

    void Deselect(UnitT unitT)
    {
        if (!SelectedUnits.Contains(unitT)) return;

        SelectedUnits.Remove(unitT);
        unitT.Selected?.Invoke(false);
    }

    public void OnMapClick(Vector3 position)
    {
        if (SelectedUnits.Count < 1)
        {
            return;
        }
        else if (SelectedUnits.Count == 1)
        {
            SelectedUnits[0].SetMoveBehavior(position);
            SelectedUnits[0].ShowWarriorsDestanations();
            return;
        }

        var positions = _unitFormation.GetPositions(SelectedUnits.Count);

        for (int i = 0; i < positions.Length; i++)
        {
            SelectedUnits[i].SetMoveBehavior(position + (positions[i] * 1));
            SelectedUnits[i].ShowWarriorsDestanations();
        }

    }

    //private void OnDisable()
    //{
    //    SelectedUnits.Clear();

    //    foreach (var unit in characterManager.MyUnits)
    //    {
    //        unit.ClickAction -= OnPlayerUnitClick;
    //        SceneTActions.Instance.OnUnitDefeated -= Deselect;
    //    }
    //    foreach (var unit in characterManager.EnemyUnits)
    //    {
    //        unit.ClickAction -= OnAIUnitClick;
    //        SceneTActions.Instance.OnUnitDefeated -= Deselect;
    //    }

    //    SceneTManager.Instance.Map.Click -= OnMapClick;

    //}


}
