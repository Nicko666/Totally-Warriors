using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitTCard : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Slider[] _healthSlider;
    [SerializeField] Image _image;
    [SerializeField] Image _x_image;
    [SerializeField] CanvasGroup _canvasGroup;
    public RectTransform _rectTransform;

    public UnitT UnitT { get; private set; }

    public void Inst(UnitT unit)
    {
        UnitT = unit;

        _image.sprite = UnitT.UnitType.Icon;

        foreach (var slider in _healthSlider)
        {
            slider.fillRect.gameObject.GetComponent<Image>().color = UnitT.Color;
            slider.maxValue = UnitT.UnitType.MaxHealth;
        }

        UpdateHealthData(UnitT.WarriorsHealth);

        UnitT.Selected += Highlite;
        UnitT.UnitTakeDamageAction += UpdateHealthData;

    }

    public void UpdateHealthData(List<int> health)
    {
        foreach (Slider slider in _healthSlider) slider.gameObject.SetActive(false);

        if (UnitT == null || health.Count < 1 || health.Sum() <= 0)
        {
            OnDefeated();
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
        if (UnitT != null)
            UnitT.ClickAction?.Invoke(UnitT);

    }

    void OnDefeated()
    {
        _x_image.enabled = true;
        _canvasGroup.alpha = 0.5f;

    }

}
