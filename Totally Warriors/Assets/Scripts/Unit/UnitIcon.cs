using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitIcon : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] Slider[] _healthSlider;
    [SerializeField] Image _image;
    UnitTacticalObject _followTarget;
    Unit _unit;

    private void Update()
    {
        if (_followTarget != null)
        transform.position = _followTarget.GetUnitCenter + Vector3.up;    
    }

    public void SetUnitIcon(Unit unit)
    {
        _followTarget = unit.UnitObj;
        _image.sprite = unit.UnitType.Icon;
        _unit = unit;

        foreach (var slider in _healthSlider)
        {
            slider.fillRect.gameObject.GetComponent<Image>().color = unit.Color;
            slider.maxValue = unit.UnitType.MaxHealth;
        }

        UpdateHealthData(unit.WarriorsHealath);

        unit.SelectedAction += Highlite;
        unit.UnitTakeDamageAction += UpdateHealthData;
        unit.UnitDefeated += OnDefeated;
    }

    public void UpdateHealthData(List<int> health)
    {
        foreach (Slider slider in _healthSlider) slider.gameObject.SetActive(false);
        
        if (health.Count < 1)
        {
            return;
        }
        
        for (int n = 0; n < health.Count; n++)
        {
            _healthSlider[n].gameObject.SetActive(true);
            _healthSlider[n].value = health[n];
        }

    }

    public void Highlite(bool value)
    {
        _canvasGroup.alpha = (value) ? 0.5f : 0.25f;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _unit.ClickAction(_unit);
    }

    public void OnDefeated(Unit unit)
    {
        Destroy(gameObject);
    }

}
