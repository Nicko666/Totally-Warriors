using UnityEngine;

[CreateAssetMenu(fileName = "New UnitType", menuName = "UnitType", order = 51)]
public class UnitType : ScriptableObject
{
    [field: SerializeField] public Type Type { get; private set; }
    [field: SerializeField] public int MaxHealth { get; private set; }
    [field: SerializeField] public int MaxNumber { get; private set; }
    [field: SerializeField] public int Strength { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float Distance { get; private set; }
    [field: SerializeField] public float StrikeInterval { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public Warrior WarriorPreefab { get; private set; }

}

