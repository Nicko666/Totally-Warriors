using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    private List<Warrior> _warriors;

    private IWarriorsPositions _warriorsPositions;
    
    public string Name { get; private set; }
    public Color Color { get; private set; }
    public Warrior[] Warriors => _warriors.ToArray();


    public Action Created;

    public Action<bool> Selected;

    public Action Defeated;

    public Action CurrentMod;

    private void OnEnable()
    {
        foreach (var warior in _warriors)
        {
            warior.Defeated += OnWarriorDefeated;

        }

        _warriorsPositions = GetComponent<CirclePositions>();

        var positions = _warriorsPositions.GetPositions(_warriors.Count);

        for (int i = 0; i < positions.Length; i++)
        {
            _warriors[i].transform.position += (positions[i] * 1);
        }

        CurrentMod = DefaultMod;

        Debug.Log("Default Mod");

    }

    private void Update() => CurrentMod();

    public void SetUnit(string team, Color color)
    {
        Name = team;
        Color = color;

        foreach (var warior in _warriors)
        {
            warior.SetWarrior(Name, Color);
        }

        Created();
    }
        
    public void MoveTo(Vector3 position)
    {
        var positions = _warriorsPositions.GetPositions(_warriors.Count);

        for (int i = 0; i < positions.Length; i++)
        {
            _warriors[i].MoveTo(position + (positions[i] * 1));
        }
    }



    void DefaultMod()
    {
        List<Warrior> detectedEnemyes = new();

        foreach (Warrior warrior in _warriors)
        {
            detectedEnemyes.AddRange(warrior.DetectedEnemies);
        }

        if (detectedEnemyes.Count < 1)
        {
            //MoveTo(GetUnitCenter);
            return;
        }

        foreach(Warrior warrior in _warriors)
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



    public void Destination(Vector3 target)
    {
        MoveTo(target);
        CurrentMod = DestinationMod;

        Debug.Log("Destination Mod");
    }

    void DestinationMod()
    {
        if (_warriors.All(w => w.GotDestination))
        {
            Debug.Log("Default Mod");

            CurrentMod = DefaultMod;
            return;
        }

        foreach(var warrior in _warriors)
        {
            if(warrior.GotDestination && warrior.DetectedEnemies.Count > 0)
            {
                warrior.StopAndAttack(warrior.SelectClosest(warrior.DetectedEnemies));
            }
        }

    }


    Unit _enemy;

    public void Enemy(Unit enemy)
    {
        _enemy = enemy;
        Debug.Log("Enemy Mod");
        CurrentMod = EnemyMod;
    }

    void EnemyMod()
    {
        if (_enemy.Warriors.Length < 1)
        {
            Debug.Log("Default Mod");
            CurrentMod = DefaultMod;
            return;
        }

        foreach (var warrior in _warriors)
        {
            if (warrior.DetectedEnemies.Any(e => _enemy.Warriors.Contains(e)))
            {
                warrior.StopAndAttack(warrior.SelectClosest(_enemy.Warriors));
            }
            else
            {
                warrior.MoveTo(warrior.SelectClosest(_enemy.Warriors).transform.position);
            }

        }
    }


    
    void OnWarriorDefeated(Warrior warior)
    {
        _warriors.Remove(warior);

        if(_warriors.Count < 1)
        {
            Defeated();
        }

    }
    
    public Vector3 GetUnitCenter
    {
        get
        {
            Vector3 center = Vector3.zero;

            foreach (var warior in _warriors)
            {
                center += warior.transform.position;
            }

            center /= _warriors.Count;

            return center;
        }
        
    }

}