using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Warrior : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private int _health;
    [SerializeField]
    private int _strength;
    [SerializeField]
    private float _radius;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private SpriteRenderer[] _armor;
    [SerializeField]
    private float _attacInterval;
    [SerializeField]
    private NavMeshAgent _agent;
    private string _team;
    private float _lastAttacTime;

    public int Health { get => _health; }
    public int MaxHealth { get => _maxHealth; }

    private void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public string GetWarriorTeam => _team;
    
    public void SetWarrior(string team, Color color)
    {
        _team = team;

        if (_armor is null) return;

        foreach (var armor in _armor)
        {
            armor.color = color;
        }
    }

    public bool GotDestination => _agent.remainingDistance < 0.01f;

    public List<Warrior> DetectedEnemies
    {
        get
        {
            var hits = Physics.OverlapSphere(transform.position, _radius);

            List<Warrior> enemies = new();

            if (hits is null) enemies.ToArray();

            foreach (var hit in hits)
            {
                Warrior warior;
                if (hit.tag == "Warrior")
                {
                    warior = hit.GetComponent<Warrior>();
                    if (warior.GetWarriorTeam != _team)
                        enemies.Add(warior);
                }

            }

            return enemies;
        }
    }

    public Warrior SelectClosest(Warrior[] warriors)
    {
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

    public Warrior SelectClosest(List<Warrior> warriors)
    {
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

    public void StopMovingOnDetect(Warrior enemy)
    {
        if ((enemy.transform.position - transform.position).sqrMagnitude < _radius * _radius)
        {
            _agent.SetDestination(this.transform.position);
        }
    }

    public void StopMoving()
    {
        _agent.SetDestination(this.transform.position);
    }

    public void Attack(Warrior enemy)
    {
        RotateTo(enemy.transform.position);

        if (_lastAttacTime + _attacInterval < Time.time)
        {
            enemy.TakeDamage(this, _strength);
            _lastAttacTime = Time.time;
        }

    }

    public void StopAndAttack(Warrior enemy)
    {
        _agent.destination = this.transform.position;

        RotateTo(enemy.transform.position);

        if (_lastAttacTime + _attacInterval < Time.time)
        {
            enemy.TakeDamage(this, _strength);
            _lastAttacTime = Time.time;
        }

    }

    void RotateTo(Vector3 position)
    {
        Vector3 direction = position - transform.position;

        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        _agent.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);

    }

    void TakeDamage(Warrior warior, int strength)
    {
        _health -= strength;

        SceneTactical.Instance.InstantiateFloatingNumber(-strength, transform.position, _armor[0].color);

        if (_health <= 0)
        {
            Defeated(this);
            
            gameObject.SetActive(false);
        }

        Damage();
    }

    public Action<Warrior> Defeated;
    public Action Damage;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

}
