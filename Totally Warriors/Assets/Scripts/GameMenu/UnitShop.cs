using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitShop : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] int currentUnit;
    [SerializeField] Color _color;
    [SerializeField] UnitCard _card;
    [SerializeField] Unit _unit;
    [SerializeField] UnitInfo _unitInfo;
    [SerializeField] Button _addButton;
    [SerializeField] Button _removeButton;

    private void OnEnable()
    {
        updateData();

        _card.BeginDrag = OnBeginDragCard;
        _card.Drag = OnDragCard;
        _card.EndDrag = OnEndDragCard;
        _card.PointerClick = ChangeUnit;
    }

    void OnBeginDragCard(UnitCard card) => card.canvasGroup.blocksRaycasts = false;

    void OnDragCard(UnitCard card, PointerEventData eventData)
    {
        card.rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    void OnEndDragCard(UnitCard card)
    {
        card.canvasGroup.blocksRaycasts = true;
        card.rectTransform.localPosition = Vector3.zero;

    }

    public void ChangeUnit(UnitCard card)
    {
        currentUnit = Mathf.RoundToInt(Mathf.Repeat(++currentUnit, BattleMenu.Instance.UnitTypes.Count));
        _unit.UnitType = BattleMenu.Instance.UnitTypes[currentUnit];
        _unit.ResetWarriors(new(1) { _unit.UnitType.MaxHealth });
        updateData();

    }

    public void AddWarrior()
    {
        _unit.AddWarriors(1);
        updateData();

    }

    public void RemoveWarrior()
    {
        _unit.RemoveWarrior(_unit.WarriorsHealth.Last());
        updateData();

    }

    void updateData()
    {
        _card.Inst(_unit, _color);
        _unitInfo.InstUnitInfo(_unit);

        _addButton.interactable = _unit.WarriorsHealth.Count < _unit.UnitType.MaxNumber;
        _removeButton.interactable = _unit.WarriorsHealth.Count > 1;
        
    }

}
