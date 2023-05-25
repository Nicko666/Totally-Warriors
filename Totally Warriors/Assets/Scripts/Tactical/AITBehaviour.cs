using System.Linq;
using UnityEngine;

public class AITBehaviour : MonoBehaviour
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

            if (characterManager.EnemyUnits == null || characterManager.EnemyUnits.Count < 1) return;

            UnitT mainEnemy = characterManager.EnemyUnits.First();
            foreach (UnitT unit in characterManager.EnemyUnits)
            {
                if (mainEnemy.WarriorsHealth.Sum() > unit.WarriorsHealth.Sum())
                {
                    mainEnemy = unit;
                }
            }

            foreach (UnitT unit in characterManager.MyUnits)
            {
                unit.SetAttackBehavior(mainEnemy);
            }

        }

        void ProtectBehavior()
        {
            //Debug.Log("AI Protect mod");

            foreach (UnitT unit in characterManager.MyUnits)
            {
                unit.SetProtectBehavior(unit.UnitCenter);
            }

        }
    }


    #region OnEneble/OnDisable

    private void OnEnable()
    {
        SceneTActions.Instance.OnUnitTDefeated += OnAnyUnitDefited;
        SceneTActions.Instance.OnUnitsTCreated += OnUnitsCreated;

    }

    //private void OnDisable()
    //{
    //    SceneTActions.Instance.OnUnitTDefeated -= OnAnyUnitDefited;
    //    SceneTActions.Instance.OnUnitsTCreated -= OnUnitsCreated;

    //}

    #endregion

}
