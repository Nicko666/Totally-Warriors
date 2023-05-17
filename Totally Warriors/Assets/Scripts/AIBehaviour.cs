using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AIBehaviour : MonoBehaviour
{
    TacticalSceneManager _sceneManager;
    IUnitFormation _unitFormation;

    private void Awake()
    {
        _unitFormation = GetComponent<CircleFormation>();

    }


    bool IsWinning => _sceneManager.Lider == _sceneManager.AI;

    public void Inst(TacticalSceneManager sceneManager)
    {
        _sceneManager = sceneManager;

        foreach (UnitT unit in sceneManager.aiUnits)
        {
            unit.AttackedBy += OnAttackedBy;
            unit.DefeatedAction += OnAnyUnitDefited;
        }
        foreach (UnitT unit in sceneManager.playerUnits)
        {
            unit.DefeatedAction += OnAnyUnitDefited;
        }

        ChangeMod();

    }

    void OnAnyUnitDefited(UnitT unit)
    {
        unit.DefeatedAction -= OnAnyUnitDefited;

        ChangeMod();
    
    }

    void ChangeMod()
    {
        if (IsWinning)
        {
            ProtectMod();
            return;
        }
        
        AttackMod();

    }

    void OnAttackedBy(UnitT unit, Warrior attacker)
    {
        foreach (var unitT in _sceneManager.aiUnits)
        {
            if (unitT.CurrentMod == unitT.DefaultMod)
            {
                unitT.Attack(attacker);
            }
        }

    }

    void AttackMod()
    {
        foreach(UnitT unit in _sceneManager.aiUnits) 
        {
            List<Warrior> targets = _sceneManager.playerUnits.First().Warriors;
            unit.Attack(targets);
        }

    }

    void ProtectMod()
    {
        Vector3 newPosition = Vector3.zero;

        foreach (UnitT unit in _sceneManager.aiUnits)
        {
            newPosition += _sceneManager.aiUnits[0].UnitCenter;

        }

        newPosition /= _sceneManager.aiUnits.Count;

        var positions = _unitFormation.GetPositions(_sceneManager.aiUnits.Count);

        for (int i = 0; i < positions.Length; i++)
        {
            _sceneManager.aiUnits[i].SetDestination(newPosition + (positions[i] * 1));
            
        }

    }


}
