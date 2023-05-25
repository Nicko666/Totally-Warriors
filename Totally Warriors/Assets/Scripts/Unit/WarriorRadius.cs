using UnityEngine;

public class WarriorRadius : MonoBehaviour
{
    [SerializeField] Warrior _warrior;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Color _protect;
    [SerializeField] Color _move;
    [SerializeField] Color _attack;

    private void OnEnable()
    {
        SceneTActions.Instance.OnGismosSwitch += OnGismosSwitch;
        SceneTActions.Instance.OnUnitsTCreated += OnUnitsTCreated;
    }

    public void OnGismosSwitch(bool value) => _spriteRenderer.enabled = value;

    public void OnUnitsTCreated()
    {
        var unit = _warrior.UnitT;
        transform.localScale = Vector3.one * unit.UnitType.Distance * 2;
        OnChangeBehavior(unit.UnitBehavior);

        unit.ChangeBehavior += OnChangeBehavior;
        SceneTActions.Instance.OnUnitsTCreated -= OnUnitsTCreated;
    
    }

    public void OnChangeBehavior(UnitTBehavior behavior)
    {
        switch (behavior)
        {
            case UnitTBehavior.Protect:
                _spriteRenderer.color = _protect;
                break; 
            case UnitTBehavior.Move:
                _spriteRenderer.color = _move;
                break; 
            case UnitTBehavior.Attack:
                _spriteRenderer.color = _attack;
                break; 
        }
    }

    private void OnDisable()
    {
        _warrior.UnitT.ChangeBehavior -= OnChangeBehavior;
        SceneTActions.Instance.OnGismosSwitch -= OnGismosSwitch;

    }

}
