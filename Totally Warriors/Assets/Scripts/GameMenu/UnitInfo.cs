using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    public void InstUnitInfo(Unit unit)
    {
        text.text =
@$"Type - {unit.UnitType.Type}
Strength - {unit.UnitType.Strength}
Speed - {unit.UnitType.Speed}
Distance - {unit.UnitType.Distance}
Interval - {unit.UnitType.StrikeInterval}";

//        [field: SerializeField] public Type Type { get; private set; }
//[field: SerializeField] public int MaxHealth { get; private set; }
//[field: SerializeField] public int MaxNumber { get; private set; }
//[field: SerializeField] public int Strength { get; private set; }
//[field: SerializeField] public float Speed { get; private set; }
//[field: SerializeField] public float Distance { get; private set; }
//[field: SerializeField] public float StrikeInterval { get; private set; }
//[field: SerializeField] public Sprite Icon { get; private set; }
//[field: SerializeField] public Warrior WarriorPreefab { get; private set; }
    }
}
