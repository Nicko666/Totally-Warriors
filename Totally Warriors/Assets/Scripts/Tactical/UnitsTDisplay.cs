using System.Collections.Generic;
using UnityEngine;

public class UnitsTDisplay : MonoBehaviour
{
    float _size = 300;
    [SerializeField] Canvas _canvas;
    [SerializeField] RectTransform _rectTransform;
    [SerializeField] CharacterManagerT _characterManager;
    [SerializeField] GameObject _cardPrefab;

    [SerializeField] List<UnitTCard> _cards;
    public List<Vector2> Positions(int count)
    {
        List<Vector2> result = new();

        float step = _size / 2;
        float min = -_size / 2;

        for (int i = 0; i < count; i++)
        {
            result.Add( new Vector2(min + (step * i), 0));
        }

        return result;

    }
    
    void UpdateCards()
    {        
        if (_cards != null)
        {
            foreach (var card in _cards) Destroy(card.gameObject);
        }
        _cards = new();

        int spacesNumber = 2;
        float step = _size / spacesNumber;
        float minPos = -_size / spacesNumber;

        for (int i = 0; i < _characterManager.MyUnits.Count; i++)
        {
            Vector3 position = _rectTransform.position + (Vector3.right * (minPos + (step * i))) * _canvas.scaleFactor;
            var unitCard = Instantiate(_cardPrefab.gameObject, position, transform.rotation, transform).GetComponent<UnitTCard>();
            unitCard.Inst(_characterManager.MyUnits[i]);
        }

    }

    void RemoveCard(UnitT unit)
    {
        UpdateCards();
    }

    #region OnEnable/OnDisable

    private void OnEnable()
    {
        SceneTActions.Instance.OnUnitsCreated += UpdateCards;
        SceneTActions.Instance.OnUnitDefeated += RemoveCard;

    }

    private void OnDisable()
    {
        SceneTActions.Instance.OnUnitsCreated -= UpdateCards;
        SceneTActions.Instance.OnUnitDefeated -= RemoveCard;
    }

    #endregion

}
