using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [field: SerializeField] public Character Character { get; private set; }
    [field: SerializeField] public int UnitsLimit { get; private set; }
    [field: SerializeField] public List<Unit> Units { get; private set; }
    [field: SerializeField] public bool Player { get; private set; }

    public Action ChangeAction;

    public void AddUnit(Unit unit)
    {
        if (Units.Count < UnitsLimit)
        {
            var temp = Instantiate(unit.gameObject, transform).GetComponent<Unit>();
            Units.Add(temp);
        }

        ChangeAction?.Invoke();

    }

    public void RemoveUnit(Unit unit)
    {
        if (Units.Contains(unit))
        {
            Destroy(unit.gameObject);
            Units.Remove(unit);
        }

        ChangeAction?.Invoke();

    }

    public void ChangeCharacter(Character character)
    {
        Character = character;

        ChangeAction?.Invoke();

    }

}

