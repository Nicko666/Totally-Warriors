using System.Collections.Generic;
using UnityEngine;

public class UnitsCanvas : MonoBehaviour
{
    float _size = 300;
    //float _hight = 80;

    [SerializeField] GameObject _cardPrefab;

    public List<Vector2> Positions(int count)
    {
        List<Vector2> result = new List<Vector2>();

        float step = _size / 2;
        float min = -_size / 2;

        for(int i = 0; i < count; i++)
        {
            result.Add(new(min + (step * i), 0) );
    }

        return result;
    
    }

}
