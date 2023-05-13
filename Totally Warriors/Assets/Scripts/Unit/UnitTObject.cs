using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitTObject : MonoBehaviour
{
    [field: SerializeField] public List<Warrior> Warriors { get; private set; }

    public string Name { get; private set; }
    public Color Color { get; private set; }


    private IUnitFormation _unitFormation;


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


    public Action CurrentMod;

    public Action<bool> Selected;

    public Action<List<int>> TakeDamageAction;

    public Action<UnitTObject> DefeatedAction;


    private void Update() => CurrentMod?.Invoke();

    public void Inst(Unit unit, Character character)
    {
        Name = character.Name;
        Color = character.Color;

        SetCircleFormation();
        var positions = _unitFormation.GetPositions(unit.WarriorsHealath.Count);

        Warriors = new List<Warrior>();

        for (int i = 0; i < unit.WarriorsHealath.Count; i++)
        {
            InstWarrior(unit.WarriorsHealath[i], transform.position + positions[i], unit.UnitType, character);
        }

        CurrentMod = DefaultMod;

    }

    public void InstWarrior(int health, Vector3 position, UnitType unitType, Character character)
    {
        Warriors.Add(Instantiate(unitType.WarriorPreefab, position, unitType.WarriorPreefab.gameObject.transform.rotation, gameObject.transform));
        Warriors.Last().SetWarrior(character.Name, character.Color, unitType, health);
        
        Warriors.Last().TakeDamageAction += OnTakeDamage;

    }

    public void OnTakeDamage()
    {
        List<Warrior> toRemove = new List<Warrior>();
        
        for (int i = 0; i < Warriors.Count; i++)
        {
            if (Warriors[i].Health <= 0) toRemove.Add(Warriors[i]);
        }

        foreach (Warrior warrior in toRemove) Warriors.Remove(warrior);

        TakeDamageAction?.Invoke(WarriorsHealth);

        if (Warriors.Count < 1) DefeatedAction?.Invoke(this);

    }

    public void SetCircleFormation()
    {
        _unitFormation = GetComponent<CircleFormation>();

    }

    
    void DefaultMod()
    {
        List<Warrior> detectedEnemyes = DetectedEnemyes;

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

    public void SetDestination(Vector3 target)
    {
        Moving(target);
        CurrentMod = DestinationMod;

        void DestinationMod()
        {
            if (Warriors.Any(w => w.GotDestination))
            {
                CurrentMod = DefaultMod;
                return;
            }

            foreach (var warrior in Warriors)
            {
                if (warrior.GotDestination && warrior.DetectedEnemies.Count > 0)
                {
                    warrior.StopAndAttack(warrior.SelectClosest(warrior.DetectedEnemies));
                }
            }

        }

        void Moving(Vector3 position)
        {
            var positions = _unitFormation.GetPositions(Warriors.Count);

            for (int i = 0; i < positions.Length; i++)
            {
                Warriors[i].MoveTo(position + (positions[i] * 1));
            }
        }

    }

    public void Attack(List<Warrior> enemy)
    {
        List<Warrior> _enemies = enemy;
        CurrentMod = EnemyMod;

        void EnemyMod()
        {
            if (_enemies == null | _enemies.Count < 1)
            {
                CurrentMod = DefaultMod;
                return;
            }

            foreach (var warrior in Warriors)
            {
                if (warrior.DetectedEnemies.Any(e => _enemies.Contains(e)))
                {
                    warrior.StopAndAttack(warrior.SelectClosest(_enemies));
                }
                else
                {
                    var enemy = warrior.SelectClosest(_enemies);
                    if (enemy == null) return;
                    warrior.MoveTo(enemy.transform.position);
                }

            }
        }
    }


    public void ShowWarriorsDestanations()
    {
        foreach (var warrior in Warriors)
        {
            warrior.ShowDestanation();
        }
    }
}