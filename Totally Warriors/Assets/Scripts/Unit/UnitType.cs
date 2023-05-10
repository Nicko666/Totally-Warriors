using UnityEngine;

[CreateAssetMenu(fileName = "New UnitType", menuName = "UnitType", order = 51)]
public class UnitType : ScriptableObject
{
    [field: SerializeField] public Type Type;
    [field: SerializeField] public int MaxHealth;
    [field: SerializeField] public int MaxNumber;
    [field: SerializeField] public int Strength;
    [field: SerializeField] public float Speed;
    [field: SerializeField] public float Distance;
    [field: SerializeField] public float StrikeInterval;
    [field: SerializeField] public Sprite Icon;

}

