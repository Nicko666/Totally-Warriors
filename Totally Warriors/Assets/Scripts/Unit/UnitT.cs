using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class UnitT : MonoBehaviour
{
    [field: SerializeField] public List<Warrior> Warriors { get; private set; }
    [field: SerializeField] public UnitTBehavior UnitBehavior { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Color Color { get; private set; }
    [field: SerializeField] public UnitType UnitType { get; private set; }

    private IUnitFormation _unitFormation;

    [SerializeField] UnitT Enemy;


    public Vector3 UnitCenter
    {
        get
        {
            Vector3 center = Vector3.zero;

            foreach (var warior in Warriors)
            {
                center += warior.transform.position;
            }

            center /= Warriors.Count;

            return center;
        }
        
    }
    public List<Warrior> DetectedEnemyes
    {
        get
        {
            List<Warrior> detectedEnemyes = new();

            foreach (Warrior warrior in Warriors)
            {
                detectedEnemyes.AddRange(warrior.DetectedEnemies);
            }

            return detectedEnemyes;
        }
        
    }
    public List<int> WarriorsHealth
    {
        get
        {
            var result = new List<int>();
            foreach (var warrior in Warriors)
            {
                result.Add(warrior.Health);
            }
            return result;
        }

    }

    public Action<UnitT> ClickAction; 

    public Action<bool> Selected;

    public Action<List<int>> UnitTakeDamageAction;

    public Action<UnitT> AttackedBy;

    public Action<UnitTBehavior> ChangeBehavior;


    private void Update()
    {
        if (Warriors.Count < 1) return;

        switch (UnitBehavior)
        {
            case UnitTBehavior.Protect:
                ProtectBehavior();
                break;

            case UnitTBehavior.Move:
                MoveBehavior();
                break;

            case UnitTBehavior.Attack:
                AttackBehavior();
                break;

        }
    }

    

    public void Inst(Unit unit, Character character, Transform transform)
    {
        Name = character.Name;
        Color = character.Color;
        UnitType = unit.UnitType;

        SetCircleFormation();

        var positions = _unitFormation.GetPositions(unit.WarriorsHealth.Count);

        Warriors = new List<Warrior>();

        for (int i = 0; i < positions.Length; i++)
        {
            CreateWarrior(unit.WarriorsHealth[i], transform.position + positions[i], transform.rotation, unit.UnitType, character);
        }
              

    }

    void CreateWarrior(int health, Vector3 position, Quaternion rotation, UnitType unitType, Character character)
    {
        Warriors.Add(Instantiate(unitType.WarriorPreefab, position, rotation, this.transform).GetComponent<Warrior>());
        Warriors.Last().Inst(this, health);

        Warriors.Last().OnWarriorAttackedBy += OnAttackedBy;
        Warriors.Last().OnWarriorTakeDamage += OnTakeDamage;
        Warriors.Last().OnWarriorDefeated += RemoveWarrior;

    }

    public void OnTakeDamage()
    {       
        UnitTakeDamageAction?.Invoke(WarriorsHealth);
    }

    void RemoveWarrior(Warrior warrior)
    {
        warrior.OnWarriorTakeDamage -= OnTakeDamage;
        warrior.OnWarriorAttackedBy -= OnAttackedBy;
        warrior.OnWarriorDefeated -= RemoveWarrior;

        Warriors.Remove(warrior);
        Destroy(warrior.gameObject);

        if (Warriors == null || Warriors.Count < 1)
        {
            SceneTActions.Instance.OnUnitDefeatedNotify(this);
        }

    }

    public void OnAttackedBy(UnitT unitT)
    {
        if (UnitBehavior == UnitTBehavior.Protect)
        {
            SetAttackBehavior(unitT);
            AttackedBy?.Invoke(unitT);
        }

    }

    public void SetCircleFormation() => _unitFormation = GetComponent<CircleFormation>();

    public void ShowWarriorsDestanations()
    {
        foreach (var warrior in Warriors)
        {
            warrior.ShowDestanation();
        }
    }


    public void SetProtectBehavior(Vector3 position)
    {
        foreach (var warrior in Warriors)
        {
            warrior.MoveTo(warrior.transform.position);
        }

        UnitBehavior = UnitTBehavior.Protect;
        ChangeBehavior!.Invoke(UnitTBehavior.Protect);
    }

    void ProtectBehavior()
    {
        foreach (var warrior in Warriors)
        {
            var enemies = warrior.DetectedEnemies;
            if (enemies.Count > 0)
            {
                SetAttackBehavior(warrior.SelectClosest(enemies).UnitT);
                return;
            }
        }
                
    }


    public void SetMoveBehavior(Vector3 target)
    {
        var positions = _unitFormation.GetPositions(Warriors.Count);

        for (int i = 0; i < Warriors.Count; i++)
        {
            Warriors[i].MoveTo(target + (positions[i] * 1));
        }

        UnitBehavior = UnitTBehavior.Move;
        ChangeBehavior!.Invoke(UnitTBehavior.Move);

    }

    void MoveBehavior()
    {
        if (Warriors.Any(w => w.GotDestination))
        {
            SetProtectBehavior(UnitCenter);
            return;
        }

        foreach (var warrior in Warriors)
        {
            if (warrior.GotDestination && warrior.DetectedEnemies.Count > 0)
            {
                //warrior.Attack(warrior.SelectClosest(warrior.DetectedEnemies));
                SetAttackBehavior(warrior.SelectClosest(warrior.DetectedEnemies).UnitT);
                return;
            }
        }

    }


    public void SetAttackBehavior(UnitT enemy)
    {
        Enemy = enemy;
        UnitBehavior = UnitTBehavior.Attack;
        ChangeBehavior!.Invoke(UnitTBehavior.Attack);

    }

    void AttackBehavior()
    {
        if (Enemy == null || Enemy.Warriors.Count < 1)
        {
            Enemy = null;
            SetProtectBehavior(UnitCenter);
            return;
        }

        foreach (var warrior in Warriors)
        {
            if (warrior.DetectedEnemies.Any(e => Enemy.Warriors.Contains(e)))
            {
                warrior.Attack(warrior.SelectClosest(Enemy.Warriors));
            }
            else
            {
                warrior.MoveTo(warrior.SelectClosest(Enemy.Warriors).transform.position);
            }
        }

    }

}