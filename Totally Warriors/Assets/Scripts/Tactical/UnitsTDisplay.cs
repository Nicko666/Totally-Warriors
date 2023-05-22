using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitsTDisplay : MonoBehaviour
{
    float _size = 300;
    [SerializeField] Canvas _canvas;
    [SerializeField] RectTransform _rectTransform;
    [SerializeField] CharacterManagerT _characterManager;
    [SerializeField] GameObject _cardPrefab;

    [SerializeField] List<UnitTCard> _cards;
    
    void UpdateCards()
    {
        Clear();

        int spacesNumber = 2;
        float step = _size / spacesNumber;
        float minPos = -_size / spacesNumber;

        for (int i = 0; i < _characterManager.MyUnits.Count; i++)
        {
            Vector3 position = _rectTransform.position + (Vector3.right * (minPos + (step * i))) * _canvas.scaleFactor;
            _cards.Add ( Instantiate(_cardPrefab.gameObject, position, transform.rotation, transform).GetComponent<UnitTCard>());
            _cards.Last().Inst(_characterManager.MyUnits[i]);
        }

        SceneTActions.Instance.OnUnitsCreated -= UpdateCards;
    }

    void Clear()
    {
        foreach (var card in _cards)
        {
            Debug.Log(card.name + " is destroyed");
            Destroy(card.gameObject);
        }

        _cards = new();

    }

    #region OnEnable/OnDisable

    private void OnEnable()
    {
        SceneTActions.Instance.OnUnitsCreated += UpdateCards;
        //SceneTActions.Instance.OnUnitsChanged += UpdateCards;
    }

    private void OnDisable()
    {
        //SceneTActions.Instance.OnUnitsChanged -= UpdateCards;
    }

    #endregion

}
