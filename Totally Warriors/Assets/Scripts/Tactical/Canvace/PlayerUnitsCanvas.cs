using UnityEngine;

public class PlayerUnitsCanvas : MonoBehaviour
{
    Character _character;

    [SerializeField]
    float _size;

    [SerializeField]
    GameObject _card;

    private void OnEnable()
    {
        _character = SceneTactical.Instance.CharacterPlayer;

        //SetDeck();
    }

    public void SetDeck()
    {
        float min = -_size / 2;
        float step = _size / 2;
        for(int i = 0;  i <= 2; i++)
        {
            GameObject card = Instantiate(_card, gameObject.transform);
            card.transform.localPosition = Vector2.right * (min + step * i);
        }

    }
}
