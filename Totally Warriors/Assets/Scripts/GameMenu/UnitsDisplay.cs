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
    [SerializeField] TMP_Text _characterImage;
    [SerializeField] RectTransform _rectTransform;

    List<UnitCard> _unitCards;

    float _size = 300;

    public void Inst(CharacterManager characterManager)
    {
        _characterManager = characterManager;
        _characterManager.ChangeAction += UpdateData;
        UpdateData();
        
    }

    private void OnDisable()
    {
        if ( _characterManager != null )
        {
            _characterManager.ChangeAction -= UpdateData;

        }
        //ClearData();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ChangeCharacter();
    }

    public void OnDrop(PointerEventData eventData)
    {
        UnitCard card;

        if (eventData.pointerDrag.TryGetComponent<UnitCard>(out card))
        {
            AddUnit(card);
        }

    }

    void ChangeCharacter()
    {
        Character currentCharacter = _characterManager.Character;
        _characterManager.ChangeCharacter(BattleMenu.Instance.GetNextCharacter(currentCharacter));

    }

    public void AddUnit(UnitCard card)
    {
        if (!_characterManager.Units.Contains(card.Unit))
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

        _characterName.text = _characterManager.Character.Name;
        _characterImage.color = _characterManager.Character.Color;
        
        for (int i = 0; i < _characterManager.Units.Count; i++)
        {
            Vector3 position = _rectTransform.position + (Vector3.right * ( minPos + (step * i))) * _canvas.scaleFactor;
            var temp = Instantiate(_unitCardPreefab.gameObject, position, transform.rotation, transform).GetComponent<UnitCard>();
            temp.Inst(_characterManager.Units[i], _characterManager.Character.Color);
            temp.PointerClick = RemoveUnit;
            _unitCards.Add(temp);
        }

    }

    void ClearData()
    {
        if (_unitCards != null)
        {
            foreach (UnitCard card in _unitCards)
            {
                Destroy(card.gameObject);
            }
        }
        _unitCards = new List<UnitCard>();
    }



}
