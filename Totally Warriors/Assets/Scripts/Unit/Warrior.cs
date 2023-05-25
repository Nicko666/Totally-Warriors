using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Warrior : MonoBehaviour
{
    [SerializeField] ColorSprites _colorSprites;
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] NavMeshObstacle _obstacle;
    [SerializeField] FloatingNunber _floatingNumberPrefab;
    [SerializeField] LandMarck _destanationLandMarckPrefab;
    [SerializeField] WarriorSpeedControl _warriorSpeedControl;

    public UnitT UnitT { get; private set; }
    public int Health { get; private set; }
    public List<Warrior> DetectedEnemies
    {
        get
        {
            var hits = Physics.OverlapSphere(transform.position, UnitT.UnitType.Distance);

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
                    if (warior.UnitT.Name != this.UnitT.Name && Health > 0)
                    {
                        enemies.Add(warior);

                    }
                }
            }

            return enemies;
        }
    }
    public bool GotDestination
    {
        get
        {
            bool result = false;
            if (_agent.remainingDistance < 0.1f)
            {
                result = true;
                
            }
            return result;
        }
    }

    float _lastAttacTime;

    public Action<UnitT> OnWarriorAttackedBy;
    public Action OnWarriorTakeDamage;
    public Action<Warrior> OnWarriorDefeated;


    public void Inst(UnitT unitT, int health)
    {
        UnitT = unitT;
        Health = health;

        ColorWarrior();

        _agent.speed = UnitT.UnitType.Speed;
        _warriorSpeedControl.Inst(UnitT.UnitType.Speed);

        void ColorWarrior()
        {
            if (_colorSprites is null)
            {
                _colorSprites = GetComponentInChildren<ColorSprites>();
            }
            foreach (var item in _colorSprites.Sprites)
            {
                item.color = UnitT.Color;
            }
        }
    }

    public Warrior SelectClosest(List<Warrior> warriors)
    {
        if (warriors.Count < 1) return null;

        Vector3 currentDestanation = _agent.destination;

        Warrior result = warriors[0];
        _agent.SetDestination(warriors[0].transform.position);
        float minDistance = _agent.remainingDistance;
        for (int i = 1; i < warriors.Count; i++)
        {
            _agent.SetDestination(warriors[i].transform.position);
            float distance = _agent.remainingDistance;
            if (distance < minDistance) result = warriors[i];
        }

        _agent.destination = currentDestanation;

        return result;
    }

    public void MoveTo(Vector3 target)
    {
        Debug.Log("change to Target");
        _obstacle.enabled = false;
        _agent.enabled = true;
        _agent.SetDestination(target);

        //if (GotDestination) Stop();
    }

    public void Stop()
    {
        //_agent.SetDestination(_agent.transform.position);
        _agent.enabled = false;
        _obstacle.enabled = true;
        Debug.Log("stop");
    }

    public void RotateTo(Vector3 position)
    {
        Vector3 direction = position - transform.position;

        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        _agent.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);

    }

    public void Attack(Warrior enemy)
    {
        if (enemy == null) return;

        _agent.destination = this.transform.position;

        RotateTo(enemy.transform.position);

        if (_lastAttacTime + UnitT.UnitType.StrikeInterval < Time.time)
        {
            enemy.TakeDamage(this, UnitT.UnitType.Strength);
            _lastAttacTime = Time.time;
        }

    }

    public void ShowDestanation() 
    {
        Instantiate(_destanationLandMarckPrefab, _agent.destination, _destanationLandMarckPrefab.transform.rotation);

    }

    void TakeDamage(Warrior warrior, int strength)
    {
        Health -= strength;
        TakeDamageAnimation(-strength);
        OnWarriorAttackedBy?.Invoke(warrior.UnitT);
        OnWarriorTakeDamage?.Invoke();
        if (Health <= 0)
        {
            OnWarriorDefeated?.Invoke(this);

        }

    }

    void TakeDamageAnimation(int number)
    {
        InstantiateFloatingNumber(number);

        void InstantiateFloatingNumber(int number)
        {
            FloatingNunber fn = Instantiate(_floatingNumberPrefab, transform.position, new()).GetComponent<FloatingNunber>();
            fn.SetNumber(number, UnitT.Color);
        }

    }

}
