using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    Unit[] _units;
    public Unit[] Units => _units;

    [SerializeField]
    string _name;
    public string Name => _name;
    [SerializeField]
    Color _color;
    public Color Color => _color;

    IBehaviorTactical _behaviour;

    private void OnEnable()
    {
        _behaviour = GetComponent<IBehaviorTactical>();

    }

}
