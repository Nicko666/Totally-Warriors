using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleMenuUnitCard : MonoBehaviour, IPointerClickHandler
{
    public Unit Unit { get; private set; }
    [SerializeField] Slider[] _healthSlider;
    [SerializeField] Image _image;

    public Action<BattleMenuUnitCard> Clicked;
    public void OnPointerClick(PointerEventData eventData) => Clicked?.Invoke(this);

    public void Inst(Unit unit, Color color)
    {
        foreach (Slider slider in _healthSlider)
        {
            slider.fillRect.gameObject.GetComponent<Image>().color = color;
            slider.maxValue = unit.UnitType.MaxHealth;
            slider.gameObject.SetActive(false);
        }

        for (int n = 0; n < unit.WarriorsHealath.Count; n++)
        {
            _healthSlider[n].gameObject.SetActive(true);
            _healthSlider[n].value = unit.WarriorsHealath[n];
        }

        _image.sprite = unit.UnitType.Icon;

    }

}
