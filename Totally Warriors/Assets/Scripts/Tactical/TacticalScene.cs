using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TacticalScene : MonoBehaviour
{
    //[field: SerializeField] public List<Unit> Character1_Units { get; private set; }
    //[field: SerializeField] public List<Unit> Character2_Units { get; private set; }

    [SerializeField] TacticalMap _mapPreefab;
    [SerializeField] TacticalUI _tacticalUI;
    [SerializeField] EventSystem _eventSystem;

    [SerializeField] Character _character1;
    [SerializeField] Character _character2;

    public void SetTacticalScene(Character character1, Character character2)
    {
        _character1 = character1;
        _character2 = character2;

        var map = Instantiate(_mapPreefab);
        
        

        InstantiateUnits(character1.Units, map.DownPositions);
        InstantiateUnits(character2.Units, map.UpPositions);

        character1.EnableTacticalBehaviour();
        character2.EnableTacticalBehaviour();

        map.MapClick = character1._tacticalBehaviour.OnMapClick;
        map.MapClick += character2._tacticalBehaviour.OnMapClick;

        foreach (var unit in character1.Units)
        {
            unit.ClickAction += character1._tacticalBehaviour.OnUnitClick;
        }
        foreach (var unit in character2.Units)
        {
            unit.ClickAction += character1._tacticalBehaviour.OnUnitClick;
        }

        void InstantiateUnits(List<Unit> units, Transform[] transform)
        {
            for (int i = 0; i < units.Count; i++)
            {
                units[i].InstantiateUnitsObjects(transform[i].transform.position);
            }
        }
        
        var ui = Instantiate(_tacticalUI.gameObject).GetComponent<TacticalUI>();
        ui.SetCanvace(character1.Units, character2.Units);

    }

}
