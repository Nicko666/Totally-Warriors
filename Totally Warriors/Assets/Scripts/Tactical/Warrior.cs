using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Warrior : MonoBehaviour
{
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

    private float _lastAttacTime;

    private NavMeshAgent _agent;

    [SerializeField]
    private string _team;
    [SerializeField]
    private Warrior _enemy;

    public int Health { get => _health; }

    public Vector3 Target
    {
        get
        {
            return _agent.transform.position;
        }
        set
        {
            _agent.SetDestination(value);
            _agent.speed = _speed;
        }
    }


    public Action CurrentAction;

    public Action<Warrior> Defeated;

    public Action<Warrior> AttackedBy;


    private void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speed;
        CurrentAction = DetectEnemy;
        CurrentAction += AttackOnSight;
        CurrentAction += Move;
    }


    private void Update()
    {
        CurrentAction();
    }

    public void SetWarrior(string name, Color color)
    {
        _team = name;

        if (_armor is null) return;

        foreach (var armor in _armor)
        {
            armor.color = color;
        }
    }

    public string GetWarriorTeam()
    {
        return _team;
    }

    public void DetectEnemy()
    {
        var hits = Physics.SphereCastAll(transform.position, _radius, Vector3.forward, _radius);

        if(hits is null) return;

        List<Warrior> enemies = new();

        foreach (var hit in hits)
        {
            Warrior warior;
            if (hit.collider.tag == "Warrior")
            {
                warior = hit.collider.GetComponent<Warrior>();
                if (warior.GetWarriorTeam() != this.GetWarriorTeam())
                    enemies.Add(warior);
            }
                
        }

        if (enemies.Count == 0)
        {
            _enemy = null;
            return; 
        }


        //select closest

        _enemy = enemies[0];

        foreach (var enemy in enemies)
        {
            if (
                (enemy.transform.position - gameObject.transform.position).sqrMagnitude <
                (_enemy.transform.position - gameObject.transform.position).sqrMagnitude
                )
            {
                _enemy = enemy;
            }
        }

    }

    public void AttackOnSight()
    {
        if (_enemy == null)
        {
            return;
        }

        RotateTo(_enemy.transform.position);

        if (_lastAttacTime + _attacInterval < Time.time)
        {
            _enemy.TakeDamage(this, _strength);
            _lastAttacTime = Time.time;
        }

    }

    public void Move()
    {
        if (_enemy == null)
        {
            _agent.speed = _speed;

            return;
        }


        if (Target == _enemy.transform.position)
        {
            _agent.speed = 0;
        }

    }

    public void RotateTo(Vector3 position)
    {
        Vector3 direction = position - transform.position;

        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        _agent.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);

    }

    public void TakeDamage(Warrior warior, int strength)
    {
        _health -= strength;

        SceneManager.Instance.InstantiateFloatingNumber(-strength, transform.position, _armor[0].color);

        if (_health <= 0)
        {
            Defeated(this);
            
            gameObject.SetActive(false);
        }

        AttackedBy(warior);
    }


    //private void OnDrawGizmos()
    //{
    //    UnityEngine.Gizmos.DrawWireSphere(transform.position, _radius);

    //}

}
