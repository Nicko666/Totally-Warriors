using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour
{
    [field: SerializeField] public List<Unit> Units { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Color Color { get; private set; } 

    public IBehaviorTactical _tacticalBehaviour { get; private set; }

    public void SetCharacter(Color color, string name, bool playeble)
    {
        Color = color; Name = name;
        _tacticalBehaviour = playeble ? GetComponent<BehaviourPlayerTactic>() : GetComponent<BehaviorAITactic>();
    }

    public void AddUnit(Unit unit)
    {
        if (Units.Count + 1 <= 3)
        {
            Units.Add(Instantiate(unit.gameObject, transform).GetComponent<Unit>());
            Units.Last().SetUnit(Name, Color);
        }

    }

    public void RemoveUnit(Unit unit)
    {
        if (Units.Contains(unit))
        {
            Units.Remove(unit);
            Destroy(unit.gameObject);
        }

    }

    public void EnableTacticalBehaviour()
    {
        _tacticalBehaviour.Enable(Units);
    }


}
