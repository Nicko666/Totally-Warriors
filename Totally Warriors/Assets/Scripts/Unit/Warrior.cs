using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Warrior : MonoBehaviour
{
    [SerializeField] ColorSprites _colorSprites;
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] FloatingNunber _floatingNumber;
    [SerializeField] LandMarck destanationLandMarck;
    [SerializeField] WarriorSpeedControl _warriorSpeedControl;

    Color _color;    
    float _lastAttacTime;

    public int Health { get; private set; }
    public string Name { get; private set; }
    public UnitType UnitType { get; private set; }
    public List<Warrior> DetectedEnemies
    {
        get
        {
            var hits = Physics.OverlapSphere(transform.position, UnitType.Distance);

            List<Warrior> enemies = new();

            if (hits is null)
            {
                return enemies;
            }

            foreach (var hit in hits)
            {
                Warrior warior;
                if (hit.gameObject.TryGetComponent<Warrior>(out warior))
                {
                    if (warior.Name != Name && Health > 0)
                    {
                        enemies.Add(warior);

                    }
                }
            }

            return enemies;
        }
    }
    public bool GotDestination => _agent.remainingDistance < 0.1f;

    public Action TakeDamageAction;

    public Action<Warrior> AttackedBy;

    public void SetWarrior(string name, Color color, UnitType unitType, int health)
    {
        Name = name;
        _color = color;
        UnitType = unitType;
        Health = health;

        ColorWarrior();

        _agent.speed = UnitType.Speed;
        _warriorSpeedControl.Inst(UnitType.Speed);

        void ColorWarrior()
        {
            if (_colorSprites is null)
            {
                _colorSprites = GetComponentInChildren<ColorSprites>();
            }
            foreach (var item in _colorSprites.Sprites)
            {
                item.color = color;
            }
        }
    }

    public Warrior SelectClosest(List<Warrior> warriors)
    {
        if (warriors.Count < 1) return null;

        var result = warriors[0];

        foreach (var warrior in warriors)
        {
            if (
                (warrior.transform.position - this.transform.position).sqrMagnitude <
                (result.transform.position - warrior.transform.position).sqrMagnitude
                )
                result = warrior;

        }

        return result;
    }

    public void MoveTo(Vector3 target)
    {
        _agent.SetDestination(target); 
    }

    public void StopAndAttack(Warrior enemy)
    {
        if (enemy == null) return;

        _agent.destination = this.transform.position;

        RotateTo(enemy.transform.position);

        if (_lastAttacTime + UnitType.StrikeInterval < Time.time)
        {
            AttackAnimation();
            enemy.TakeDamage(this, UnitType.Strength);
            _lastAttacTime = Time.time;
        }

        void RotateTo(Vector3 position)
        {
            Vector3 direction = position - transform.position;

            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            _agent.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);

        }

    }    

    void TakeDamage(Warrior warrior, int strength)
    {
        Health -= strength;
        TakeDamageAnimation(-strength);
        AttackedBy?.Invoke(warrior);
        TakeDamageAction?.Invoke();

        if (Health <= 0)
        {
            Health = 0;
            DeathAnimation();
        }

    }

    void TakeDamageAnimation(int number)
    {
        InstantiateFloatingNumber(number);

        void InstantiateFloatingNumber(int number)
        {
            FloatingNunber fn = Instantiate(_floatingNumber, transform.position, new()).GetComponent<FloatingNunber>();
            fn.SetNumber(number, _color);
        }

    }

    void DeathAnimation()
    {
        gameObject.SetActive(false);
    }

    void AttackAnimation()
    {


    }

    public void ShowDestanation() 
    {
        Instantiate(destanationLandMarck, _agent.destination, destanationLandMarck.transform.rotation);

    }

    float Distance(Vector3 position)
    {
        return (this.transform.position - position).magnitude;
    }


}
