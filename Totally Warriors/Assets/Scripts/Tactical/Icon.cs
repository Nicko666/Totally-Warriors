using UnityEngine;
using UnityEngine.UI;

public class Icon : MonoBehaviour
{
    private Unit _unit;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    private float _baseHighlite;

    [SerializeField]
    private Slider[] _healthSlider;

    private void OnEnable()
    {
        _unit = GetComponentInParent<Unit>();
        _baseHighlite = _spriteRenderer.color.a;

        foreach (Warrior warrior in _unit.Warriors)
        {
            warrior.Damage = SetHealth;
        }

        SetHealth();

    }

    private void Update()
    {
        if (!_unit.IsDefeated)
        {
            transform.position = new(_unit.GetUnitCenter().x, transform.position.y, _unit.GetUnitCenter().z);
        }

    }

    private void OnMouseUpAsButton() => Selected();

    public void Selected()
    {
        _unit.OnSelected();

        Highlite(true);

    }

    public void Highlite(bool value)
    {
        _spriteRenderer.color = new(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, (value)? _baseHighlite * 2 : _baseHighlite);
    }

    void SetHealth()
    {
        foreach (Slider slider in _healthSlider) slider.gameObject.SetActive(false);
        
        for (int n = 0; n < _unit.Warriors.Length; n++)
        {
            _healthSlider[n].gameObject.SetActive(true);
            _healthSlider[n].maxValue = _unit.Warriors[n].MaxHealth;
            _healthSlider[n].value = _unit.Warriors[n].Health;
        }
        

    }
}
