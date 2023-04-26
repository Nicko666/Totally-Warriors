using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    private List<Warrior> _warriors;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private bool _holdPositions;
    [SerializeField]
    private string _team;
    [SerializeField]
    private Color _color;

    private IUnitPositionGenerator _positionGenerator;


    private void OnEnable()
    {
        _positionGenerator = GetComponent<IUnitPositionGenerator>();
        
        _target.GetOrAddComponent<Target>().OnChangePosition += (target) => SetWariorsTarget(target.position);
        

        foreach (var warior in _warriors)
        {
            warior.Defeated += OnWariorDefeated;

            warior.AttackedBy += OnAttaked;

            warior.SetWarrior(_team, _color);

        }

    }

    public void OnWariorDefeated(Warrior warior)
    {
        _warriors.Remove(warior);

        if(IsDefeated)
        {
            Debug.Log($"{gameObject.name} was defeated");
            IsSelected = false;
        }

    }

    public void SetUnitTarget(Vector3 position)
    {
        _target.transform.position = position;
    }

    public void SetWariorsTarget(Vector3 center)
    {
        Debug.Log("SetWariorsTarget");
        var positions = _positionGenerator.GetPosition(_warriors.Count);
        for (int i = 0; i < positions.Length; i++)
        {
            _warriors[i].Target = center + (positions[i] * 1);
        }
    }

    public void SetWariorsTarget(Unit enemy)
    {
        List<Vector3> positions = new();
            
        foreach(Warrior warior in enemy._warriors)
            positions.Add(warior.transform.position);

        int enemyNumber = Mathf.Clamp(0, 0, positions.Count);

        foreach (var warior in _warriors)
        {
            warior.Target = (positions[enemyNumber]);
            enemyNumber++;
        }
    }

    public void SetWariorsTarget(Warrior warrior)
    {
        foreach (var war in _warriors)
        {
            war.Target = (warrior.transform.position);
        }
    }

    public Vector3 GetUnitPosition()
    {
        Vector3 center = Vector3.zero;

        foreach (var warior in _warriors)
        {
            center += warior.transform.position;
        }

        center /= _warriors.Count;

        return center;
    }

    public bool IsSelected
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

    public bool IsPlayer() => PlayerManager.Instance.PlayerUnits.Any(u => u == this);

    public void OnSelected()
    {
        if (PlayerManager.Instance.PlayerUnits.Any(u => u == this))
        {
            IsSelected = true;
        }
        else
        {
            Attacked();
        }
    }

    public void Attacked()
    {
        foreach(var unit in PlayerManager.Instance.SelectedUnits)
        {
            unit.Attack(this);
        }
    }

    public void OnAttaked(Warrior warrior)
    {
        if (_holdPositions) return;

        foreach(var war in _warriors)
        {
            SetWariorsTarget(warrior);
        }
    }

    public void Attack(Unit unit)
    {
        SetWariorsTarget(unit);
    }

    public bool IsDefeated => _warriors.Count < 1;

}


    //private void Start()
    //{
    //    WarriorsStartPosition();

    //}

    //private void WarriorsStartPosition()
    //{
    //    if( _warriors.Count > 0 )
    //    {
    //        var positions = _positionGenerator.GetPosition(_warriors.Count);

    //        for (int i = 0; i < positions.Length; i++)
    //        {
    //            _warriors[i].gameObject.transform.position = transform.position + (positions[i] * 1);
    //        }

    //    }
    //}