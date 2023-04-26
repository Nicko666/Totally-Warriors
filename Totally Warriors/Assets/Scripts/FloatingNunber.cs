using UnityEngine;
using TMPro;

public class FloatingNunber : MonoBehaviour
{
    [SerializeField]
    TMP_Text _text;

    float _time = 1.0f;

    private void Update()
    {
        _time -= Time.deltaTime;

        transform.position += Vector3.forward * Time.deltaTime;

        if (_time <= 0) Destroy(gameObject);
    }

    public void SetNumber(int number, Color color)
    {
        _text.text = number.ToString();
        _text.color = color;
    }

}
