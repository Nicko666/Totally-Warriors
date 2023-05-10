using System.Collections.Generic;
using UnityEngine;

public class UnitsCanvas : MonoBehaviour
{
    float _size = 300;
    float _hight = 80;

    [SerializeField]
    GameObject _cardPrefab;

    public void SetDeck(List<Unit> units)
    {
        float min = -_size / 2;
        float step = _size / 2;

        for(int i = 0;  i < units.Count; i++)
        {
            GameObject card = Instantiate(_cardPrefab, gameObject.transform);
            card.transform.localPosition = new (min + step * i, _hight);
            card.GetComponent<UnitCard>().SetUnitCard(units[i]);

        }

    }



}
