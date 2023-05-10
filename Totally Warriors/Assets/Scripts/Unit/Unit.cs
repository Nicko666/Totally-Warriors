using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Unit : MonoBehaviour
{
    [field: Header("Fill in Preefab")]
    [field: SerializeField] public List<int> WarriorsHealath { get; private set; }
    [field: SerializeField] public UnitType UnitType;
    [field: SerializeField] public Warrior WarriorPreefab;
    [SerializeField] UnitTacticalObject _tacticalPreefab;
    [SerializeField] UnitIcon _unitIconPreefab;

    [field: Header("InGame Fields")]
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Color Color { get; private set; }
    [field: SerializeField] public UnitTacticalObject UnitObj { get; private set; }
    [field: SerializeField] public UnitIcon UnitIco { get; private set; }


    public Action<Unit> ClickAction;
    public Action<bool> SelectedAction;
    public Action<List<int>> UnitTakeDamageAction;
    public Action<Unit> UnitDefeated;

    public void SetWarriorNumber(int number)
    {
        number = (number > UnitType.MaxNumber)? UnitType.MaxNumber : number;

        WarriorsHealath = new List<int>(Enumerable.Repeat(UnitType.MaxHealth, number));

    }

    public void SetUnit(string name, Color color)
    {
        if (WarriorsHealath.Count < 1)
        {
            SetWarriorNumber(3);
        }

        Name = name;
        Color = color;

    }

    public void InstantiateUnitsObjects(Vector3 position)
    {
        UnitObj = Instantiate(_tacticalPreefab.gameObject).GetComponent<UnitTacticalObject>();
        UnitObj.SetUnitObject(this, position);

        UnitIco = Instantiate(_unitIconPreefab.gameObject).GetComponent<UnitIcon>();
        UnitIco.SetUnitIcon(this);

        UnitObj.UnitTakeDamageAction = OnTakeDamage;
        UnitObj.Defeated += OnDefeated;

    }

    public void OnTakeDamage(List<int> healthRamain)
    {
        WarriorsHealath = healthRamain;
        UnitTakeDamageAction(WarriorsHealath);
    }

    void OnDefeated()
    {
        UnitDefeated.Invoke(this);
        UnitObj = null;
        Destroy(UnitIco.gameObject);
    }

}
