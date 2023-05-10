using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitCard : MonoBehaviour, IPointerClickHandler
{
    [field: Header("Fill in Preefab")]
    [SerializeField] Slider[] _healthSlider;
    [SerializeField] Image _image;
    [SerializeField] Image _x_image;
    [SerializeField] CanvasGroup _canvasGroup;
    Unit _unit;

    bool active;

    public void SetUnitCard(Unit unit)
    {
        _unit = unit;
        _image.sprite = unit.UnitType.Icon;
        _x_image.enabled = false;

        foreach (var slider in _healthSlider)
        {
            slider.fillRect.gameObject.GetComponent<Image>().color = unit.Color;
            slider.maxValue = unit.UnitType.MaxHealth;
        }

        UpdateHealthData(unit.WarriorsHealath);

        unit.SelectedAction += Highlite;
        unit.UnitTakeDamageAction += UpdateHealthData;

        active = true;
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
        _canvasGroup.alpha = (value) ? 1f : 0.5f;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (active)
        {
            _unit.ClickAction(_unit);
        }
    }

    public void OnDefeated(Unit unit)
    {
        _x_image.enabled = true;
        active = false;
    }

}
