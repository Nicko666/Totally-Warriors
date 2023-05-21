using UnityEngine;

public class WarriorRadius : MonoBehaviour
{
    [SerializeField] Warrior _warrior;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Color _protect;
    [SerializeField] Color _move;
    [SerializeField] Color _attack;

    private void Start()
    {
        _warrior.UnitT.ChangeBehavior += ChangeBehavior;

        float radius = _warrior.UnitT.UnitType.Distance;
        transform.localScale = Vector3.one * radius * 2;

        ChangeBehavior(_warrior.UnitT.UnitBehavior);

    }

    private void OnDestroy()
    {
        _warrior.UnitT.ChangeBehavior -= ChangeBehavior;

    }

    public void ChangeBehavior(UnitTBehavior behavior)
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


    
}
