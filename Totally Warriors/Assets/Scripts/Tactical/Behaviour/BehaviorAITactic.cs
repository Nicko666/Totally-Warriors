using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BehaviorAITactic : MonoBehaviour, IBehaviorTactical
{
    Character character;


    public void Enable(List<Unit> units)
    {
        character = GetComponent<Character>();
        
        foreach (Unit unit in units)
        {
            unit.UnitObj.HoldPositions = false;
        }
    }

    public void OnMapClick(Vector3 position)
    {
        
    }

    public void OnUnitClick(Unit unit)
    {
        
    }
}
