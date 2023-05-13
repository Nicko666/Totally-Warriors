using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitCard : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Slider[] _healthSlider;
    [SerializeField] Image _image;
    [SerializeField] Image _x_image;
    [SerializeField] CanvasGroup _canvasGroup;
    
    UnitTObject _unitTO;
    bool active;

    public Action<UnitTObject> Click;

    public void Inst(UnitType unitType, UnitTObject unitTO)
    {
        _unitTO = unitTO;
        _image.sprite = unitType.Icon;

        foreach (var slider in _healthSlider)
        {
            slider.fillRect.gameObject.GetComponent<Image>().color = unitTO.Color;
            slider.maxValue = unitType.MaxHealth;
        }

        UpdateHealthData(unitTO.WarriorsHealth);
        _unitTO.Selected += Highlite;
        _unitTO.TakeDamageAction += UpdateHealthData;
        _unitTO.DefeatedAction += OnDefeated;

        active = true;
    }

    public void UpdateHealthData(List<int> health)
    {
        foreach (Slider slider in _healthSlider) slider.gameObject.SetActive(false);

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
            Click?.Invoke(_unitTO);
        }

    }

    public void OnDefeated(UnitTObject unitTObject)
    {
        _canvasGroup.alpha = 0.25f;

        unitTObject.Selected -= Highlite;
        unitTObject.TakeDamageAction -= UpdateHealthData;
        unitTObject.DefeatedAction -= OnDefeated;

        _x_image.enabled = true;
        active = false;

    }

}
