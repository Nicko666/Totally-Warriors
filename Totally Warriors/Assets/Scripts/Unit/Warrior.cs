using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Warrior : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] _armor;
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] FloatingNunber _floatingNumber;
    [SerializeField] LandMarck destanationLandMarck;

    Color _color;
    UnitType _unitType;
    float _lastAttacTime;
    public string Name { get; private set; }
    public int Health { get; private set; }
    public List<Warrior> DetectedEnemies
    {
        get
        {
            var hits = Physics.OverlapSphere(transform.position, _unitType.Distance);

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
    public bool GotDestination => _agent.remainingDistance < 0.01f;

    public Action TakeDamageAction;

    public void SetWarrior(string name, Color color, UnitType unitType, int health)
    {
        Name = name;
        _color = color;
        _unitType = unitType;
        Health = health;

        //paint armor
        if (_armor is null) return;
        foreach (var armor in _armor)
        {
            armor.color = color;
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

        if (_lastAttacTime + _unitType.StrikeInterval < Time.time)
        {
            AttackAnimation();
            enemy.TakeDamage(this, _unitType.Strength);
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

}
