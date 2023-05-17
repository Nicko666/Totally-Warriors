using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [field: SerializeField] public Character Character { get; private set; }
    [field: SerializeField] public List<Unit> Units { get; private set; }
    [field: SerializeField] public bool Player { get; private set; }

    public Action<List<Unit>> UnitsChange;
    public Action<Character> CharacterChange;

    public void AddUnit(Unit unit)
    {
        if (Units.Count < 3)
        {
            Units.Add(unit);
            UnitsChange?.Invoke(Units);
        }

    }

    public void RemoveUnit(Unit unit)
    {
        if (Units.Contains(unit))
        {
            Units.Remove(unit);
            Destroy(unit.gameObject);
            UnitsChange?.Invoke(Units);
        }

    }

    public void ChangeCharacter(Character character)
    {
        Character = character;
        CharacterChange?.Invoke(Character);
    }

}

