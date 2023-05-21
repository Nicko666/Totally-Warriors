using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitsDisplay : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    [SerializeField] Canvas _canvas;
    [SerializeField] CharacterManager _characterManager;
    [SerializeField] UnitCard _unitCardPreefab;
    [SerializeField] TMP_Text _characterName;
    [SerializeField] RectTransform _rectTransform;

    List<UnitCard> _unitCards;

    float _size = 300;

    public void Inst(CharacterManager characterManager)
    {
        _characterManager = characterManager;
    }

    private void OnEnable()
    {
        _unitCards = new List<UnitCard>();
        _characterManager.ChangeAction = UpdateData;
        UpdateData();
    }

    private void OnDisable()
    {
        _characterManager.ChangeAction -= UpdateData;
        ClearData();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ChangeCharacter();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            var card = eventData.pointerDrag.GetComponent<UnitCard>();
            AddUnit(card);

        }

    }

    void ChangeCharacter()
    {
        Character currentCharacter = _characterManager.Character;
        _characterManager.ChangeCharacter(BattleManager.Instance.GetNextCharacter(currentCharacter));

    }

    public void AddUnit(UnitCard card)
    {
        _characterManager.AddUnit(card.Unit);

    }

    public void RemoveUnit(UnitCard card)
    {
        _characterManager.RemoveUnit(card.Unit);

    }

    void UpdateData()
    {
        ClearData();

        int spacesNumber = _characterManager.UnitsLimit - 1;
        float step = _size / spacesNumber;
        float minPos = -_size / spacesNumber;

        for (int i = 0; i < _characterManager.Units.Count; i++)
        {
            _characterName.text = _characterManager.Character.Name;
            Vector3 position = _rectTransform.position + (Vector3.right * ( minPos + (step * i))) * _canvas.scaleFactor;
            var temp = Instantiate(_unitCardPreefab.gameObject, position, transform.rotation, transform).GetComponent<UnitCard>();
            temp.Inst(_characterManager.Units[i], _characterManager.Character.Color);
            temp.PointerClick = RemoveUnit;
            _unitCards.Add(temp);
        }

    }

    void ClearData()
    {
        foreach (UnitCard card in _unitCards)
        {
            Destroy(card.gameObject);
        }
        _unitCards = new List<UnitCard>();
    }



}