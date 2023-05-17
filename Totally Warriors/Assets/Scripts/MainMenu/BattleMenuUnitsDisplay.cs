using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleMenuUnitsDisplay : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    [SerializeField] Color _color;
    [SerializeField] CharacterManager _characterManager;
    [SerializeField] BattleMenuUnitCard _unitCardPreefab;
    [SerializeField] TMP_Text _characterName;

    private void OnEnable()
    {
        _characterManager.UnitsChange += UpdateUnits;
        _characterManager.CharacterChange += UpdateCharacter;
        UpdateUnits(_characterManager.Units);
        UpdateCharacter(_characterManager.Character);
    }

    private void OnDisable() 
    { 
        _characterManager.UnitsChange -= UpdateUnits;
        _characterManager.CharacterChange -= UpdateCharacter;

    }

    public Action UpdateCards;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            _characterManager.Units.Add(eventData.pointerDrag.GetComponent<UnitMenu>().GetUnit());
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Character currentCharacter = _characterManager.Character;
        _characterManager.ChangeCharacter(BattleManager.Instance.GetNextCharacter(currentCharacter));

    }

    void UpdateUnits(List<Unit> units)
    {
        UpdateCards?.Invoke();

        for (int i = 0; i < units.Count; i++)
        {

            var temp = Instantiate(_unitCardPreefab, transform.position, transform.rotation, transform).GetComponent<BattleMenuUnitCard>();
            temp.Inst(units[i], _characterManager.Character.Color);
            temp.Clicked = RemoveCard;
        }

    }

    void UpdateCharacter(Character character)
    {
        _characterName.text = _characterManager.Character.Name;

    }

    public void RemoveCard(BattleMenuUnitCard unitCard)
    {
        _characterManager.RemoveUnit(unitCard.Unit);
        Destroy(unitCard.gameObject);
    }

}
