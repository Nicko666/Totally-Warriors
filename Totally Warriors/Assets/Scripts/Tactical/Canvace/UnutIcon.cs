using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnutIcon : MonoBehaviour, IPointerClickHandler
{
    private Unit _unit;

    [SerializeField]
    private CanvasGroup _canvasGroup;
    private float _baseHighlite;

    [SerializeField]
    private Slider[] _healthSlider;

    [SerializeField]
    bool follow;

    private void OnEnable()
    {
        _unit = GetComponentInParent<Unit>();        

        _baseHighlite = _canvasGroup.alpha;

        foreach (Warrior warrior in _unit.Warriors)
        {
            warrior.Damage += SetHealth;
        }

        _unit.Created += SetColor;
        _unit.Created += SetHealth;
        _unit.Defeated += OnDefeated;
        _unit.Selected += Highlite;

    }

    private void OnDisable()
    {
        foreach (Warrior warrior in _unit.Warriors)
        {
            warrior.Damage -= SetHealth;
        }

        _unit.Created -= SetColor;
        _unit.Created -= SetHealth;
        _unit.Defeated -= OnDefeated;

    }

    private void Update()
    {
        if (follow)
        transform.position = new(_unit.GetUnitCenter.x, transform.position.y, _unit.GetUnitCenter.z);    
    }

    public void OnDefeated()
    {
        //List<Unit> units = SceneTactical.Instance.CharacterPlayer.SelectedUnits;
        
        //if (units.Any(u => u == _unit))
        //{
        //    units.Remove(_unit);
        //    Highlite(false);
        //}

        //_unit.gameObject.SetActive(false);
    }

    public void Highlite(bool value)
    {
        _canvasGroup.alpha = (value)? _baseHighlite * 2 : _baseHighlite;

    }

    void SetColor()
    {
        foreach(var item in _healthSlider)
        {
            item.fillRect.gameObject.GetComponent<Image>().color = _unit.Color;
        }
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

    public void OnPointerClick(PointerEventData eventData)
    {
        SceneTactical.Instance.UnitClick(_unit);
    }

}
