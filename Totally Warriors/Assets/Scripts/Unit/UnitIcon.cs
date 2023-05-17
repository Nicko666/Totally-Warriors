using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public class UnitIcon : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] Slider[] _healthSlider;
    [SerializeField] Image _image;
    
    UnitT _unitTO;

    public Action<UnitT> Click;

    private void Update()
    {
        if (_unitTO != null & _unitTO.Warriors.Count > 0)
            transform.position = _unitTO.UnitCenter + Vector3.up;
    }

    public void Inst(UnitType unitType, UnitT unitTO)
    {
        _unitTO = unitTO;
        _image.sprite = unitType.Icon;

        foreach (var slider in _healthSlider)
        {
            slider.fillRect.gameObject.GetComponent<Image>().color = unitTO.Color;
            slider.maxValue = unitType.MaxHealth;
        }

        _unitTO.Selected += Highlite;
        _unitTO.TakeDamageAction += UpdateHealthData;
        _unitTO.DefeatedAction += OnDefeated;

        UpdateHealthData(unitTO.WarriorsHealth);

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
        _canvasGroup.alpha = (value) ? 0.5f : 0.25f;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Click?.Invoke(_unitTO);
    }

    public void OnDefeated(UnitT unitTObject)
    {
        foreach (Slider slider in _healthSlider) slider.gameObject.SetActive(false);

        unitTObject.Selected -= Highlite;
        unitTObject.TakeDamageAction -= UpdateHealthData;
        unitTObject.DefeatedAction -= OnDefeated;

        Destroy(gameObject);

    }

}
