using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    [SerializeField] CharacterManagerT characterManager;
    IUnitFormation _unitFormation;
    bool IsWinning => SceneTManager.Instance.Lider == characterManager.Character;
    
    void OnUnitsCreated()
    {
        _unitFormation = GetComponent<CircleFormation>();

        ChangeBehavior();
    }

    void OnAnyUnitDefited(UnitT unit)
    {
        ChangeBehavior();
    
    }

    void ChangeBehavior()
    {
        if (IsWinning)
        {
            ProtectBehavior();
            return;
        }
        
        AttackBehavior();

        void AttackBehavior()
        {
            //Debug.Log("AI Attack mod");
            //UnitT mainEnemy = _sceneManager.PlayerUnits.First();
            //foreach (UnitT unit in _sceneManager.PlayerUnits)
            //{
            //    //if (mainEnemy.WarriorsHealth[0] > unit.WarriorsHealth[0])
            //    //{
            //    //    mainEnemy = unit;
            //    //}
            //}

            //foreach (UnitT unit in _sceneManager.AIUnits)
            //{
            //    unit.SetAttackBehavior(mainEnemy);
            //}

        }

        void ProtectBehavior()
        {
            //Debug.Log("AI Protect mod");
            //foreach (UnitT unit in _sceneManager.AIUnits)
            //{
            //    unit.SetProtectBehavior(unit.UnitCenter);
            //}

        }
    }


    #region OnEneble/OnDisable

    private void OnEnable()
    {
        SceneTActions.Instance.OnUnitDefeated += OnAnyUnitDefited;
        SceneTActions.Instance.OnUnitsCreated += OnUnitsCreated;

    }

    private void OnDisable()
    {
        SceneTActions.Instance.OnUnitDefeated -= OnAnyUnitDefited;
        SceneTActions.Instance.OnUnitsCreated -= OnUnitsCreated;

    }

    #endregion

}
