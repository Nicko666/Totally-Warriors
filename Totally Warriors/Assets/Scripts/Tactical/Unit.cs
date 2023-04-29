using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    private List<Warrior> _warriors;
    [SerializeField]
    private string _team;
    [SerializeField]
    private Color _color;

    private IUnitPositionGenerator _positionGenerator;

    bool IsSelected
    {
        get
        {
            return PlayerManager.Instance.SelectedUnits.Any(u => u == this);
        }

        set
        {
            if (value)
            {
                if (!PlayerManager.Instance.SelectedUnits.Any(u => u == this))
                    PlayerManager.Instance.SelectedUnits.Add(this);
            }
            else
            {
                if (PlayerManager.Instance.SelectedUnits.Any(u => u == this))
                    PlayerManager.Instance.SelectedUnits.Remove(this);
            }
        }
    }


    public Warrior[] Warriors => _warriors.ToArray();

    private void OnEnable()
    {
        _positionGenerator = GetComponent<IUnitPositionGenerator>();

        foreach (var warior in _warriors)
        {
            warior.SetWarrior(_team, _color);

            warior.DetectEnemy += OnEnemyDetection;
            warior.Defeated += OnWarriorDefeated;

        }

    }

    public void OnSelected()
    {
        if (PlayerManager.Instance.PlayerUnits.Any(u => u == this))
        {
            IsSelected = true;
        }
        else
        {
            foreach (Unit unit in PlayerManager.Instance.SelectedUnits)
                unit.SetEnemy(this);
        }
    }

    public bool TargetReached => _warriors.All(w => w.TargetReached);

    [SerializeField]
    private Unit _enemy;

    public Unit Enemy
    {
        get
        {
            if (_enemy != null)
                return (_enemy._warriors.Count < 1) ? null : _enemy;
            else return null;
        }
    }

    public void SetEnemy(Unit enemy)
    {
        List<Vector3> positions = new();

        _enemy = enemy;

        foreach (Warrior warrior in _warriors)
        {
            warrior.TargetReached = true;
            warrior.MoveTo(warrior.SelectClosest(enemy._warriors.ToArray()));
        }
            
    }

    public void SetPosition(Vector3 position)
    {
        var positions = _positionGenerator.GetPosition(_warriors.Count);
        for (int i = 0; i < positions.Length; i++)
        {
            _warriors[i].MoveTo(position + (positions[i] * 1));
        }

        _enemy = null;
    }

    public void OnEnemyDetection(Warrior[] enemies)
    {
        List<Warrior> specialEnemies = new();
        if (Enemy != null)
            foreach (var enemy in enemies)
                if (Enemy._warriors.Any(w => w = enemy)) specialEnemies.Add(enemy);

        foreach (var warrior in _warriors)
        {
            var targetEnemy = warrior.SelectClosest(enemies);

            if (TargetReached)
            {
                warrior.MoveTo(targetEnemy);
                if (Enemy == null) warrior.StopMovingOnDetect(targetEnemy);

            }

            if (Enemy != null)
            {
                warrior.MoveTo(warrior.SelectClosest(Enemy._warriors.ToArray()));
                warrior.StopMovingOnDetect(warrior.SelectClosest(Enemy._warriors.ToArray()));
            }

            warrior.Attack(targetEnemy);

            if (specialEnemies.Count > 0)
                warrior.Attack(warrior.SelectClosest(specialEnemies.ToArray()));
        }

    }



    public void OnWarriorDefeated(Warrior warior)
    {
        _warriors.Remove(warior);

        if(_warriors.Count == 0)
        {
            Debug.Log($"{gameObject.name} was defeated");
            IsSelected = false;
        }

    }
    
    public Vector3 GetUnitCenter()
    {
        Vector3 center = Vector3.zero;

        foreach (var warior in _warriors)
        {
            center += warior.transform.position;
        }

        center /= _warriors.Count;

        return center;
    }

    public bool IsDefeated => _warriors.Count == 0;

}