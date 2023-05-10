using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitTacticalObject : MonoBehaviour
{
    [field: SerializeField] public List<Warrior> Warriors { get; private set; }
    [SerializeField] string mod;
    
    public bool HoldPositions;

    private IUnitFormation _unitFormation;

    public Vector3 GetUnitCenter
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

    public Action<bool> Selected;

    public Action Defeated;

    public Action CurrentMod;

    public Action<List<int>> UnitTakeDamageAction;


    private void Update()
    {
        if (Warriors.All(w => w.enabled))
        {
            CurrentMod.Invoke();
        }
    }


    public void SetUnitObject(Unit unit, Vector3 position)
    {
        SetCircleFormation();

        var positions = _unitFormation.GetPositions(unit.WarriorsHealath.Count);

        Warriors = new List<Warrior>();

        for (int i = 0; i < unit.WarriorsHealath.Count; i++)
        {
            Warriors.Add(Instantiate(unit.WarriorPreefab, position + positions[i], unit.WarriorPreefab.gameObject.transform.rotation, gameObject.transform));
            Warriors.Last().SetWarrior(unit.Name, unit.Color, unit.UnitType, unit.WarriorsHealath[i]);
            Warriors.Last().TakeDamageByAction += UnitObjectTakeDamage;
            Warriors.Last().DefeatedAction += OnWarriorDefeated;

        }

        CurrentMod = DefaultMod;

    }

    public void UnitObjectTakeDamage(Warrior enemy)
    {
        List<int> health = new List<int>();

        foreach (var warrior in Warriors)
        {            
            health.Add(warrior.Health);
        }

        if (!HoldPositions && DetectedEnemyes().Count < 1 && CurrentMod == DefaultMod)
        {
            foreach (var warrior in Warriors)
            {
                warrior.MoveTo(enemy.transform.position);
            }
            
        }

        UnitTakeDamageAction(health);

    }

    void OnWarriorDefeated(Warrior warior)
    {
        Warriors.Remove(warior);

        if (Warriors.Count < 1)
        {
            Defeated();
        }

    }



    public void SetCircleFormation()
    {
        _unitFormation = GetComponent<CircleFormation>();

    }

    public void Moving(Vector3 position)
    {
        var positions = _unitFormation.GetPositions(Warriors.Count);

        for (int i = 0; i < positions.Length; i++)
        {
            Warriors[i].MoveTo(position + (positions[i] * 1));
        }
    }



    void DefaultMod()
    {
        mod = "DefaultMod";

        List<Warrior> detectedEnemyes = DetectedEnemyes();

        if (detectedEnemyes.Count < 1)
        {
            //Moving(GetUnitCenter);
            return;
        }

        foreach(Warrior warrior in Warriors)
        {
            if(warrior.DetectedEnemies.Count > 0)
            {
                warrior.StopAndAttack(warrior.SelectClosest(warrior.DetectedEnemies));
            }
            else
            {
                warrior.MoveTo(warrior.SelectClosest(detectedEnemyes).gameObject.transform.position);
            }
        }
    }

    List<Warrior> DetectedEnemyes()
    {
        List<Warrior> detectedEnemyes = new();

        foreach (Warrior warrior in Warriors)
        {
            detectedEnemyes.AddRange(warrior.DetectedEnemies);
        }

        return detectedEnemyes;
    }


    public void Destination(Vector3 target)
    {
        Moving(target);
        CurrentMod = DestinationMod;
    }

    void DestinationMod()
    {
        mod = "DestinationMod";

        if (Warriors.Any(w => w.GotDestination))
        {
            CurrentMod = DefaultMod;
            return;
        }

        foreach(var warrior in Warriors)
        {
            if(warrior.GotDestination && warrior.DetectedEnemies.Count > 0)
            {
                warrior.StopAndAttack(warrior.SelectClosest(warrior.DetectedEnemies));
            }
        }

    }


    UnitTacticalObject _enemy;

    public void Enemy(UnitTacticalObject enemy)
    {
        _enemy = enemy;
        CurrentMod = EnemyMod;
    }

    void EnemyMod()
    {
        mod = "EnemyMod";

        if (_enemy == null | _enemy.Warriors.Count < 1)
        {
            CurrentMod = DefaultMod;
            return;
        }

        foreach (var warrior in Warriors)
        {
            if (warrior.DetectedEnemies.Any(e => _enemy.Warriors.Contains(e)))
            {
                warrior.StopAndAttack(warrior.SelectClosest(_enemy.Warriors));
            }
            else
            {
                var enemy = warrior.SelectClosest(_enemy.Warriors);
                if (enemy == null) return;
                warrior.MoveTo(enemy.transform.position);
            }

        }
    }


}