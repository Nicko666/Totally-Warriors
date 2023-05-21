using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitCard : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Unit Unit { get; private set; }

    [SerializeField] Slider[] _healthSlider;
    [SerializeField] Image _image;
    public RectTransform rectTransform { get; private set; }
    public CanvasGroup canvasGroup { get; private set; }

    public Action<UnitCard> PointerClick;
    public Action<UnitCard> BeginDrag;
    public Action<UnitCard, PointerEventData> Drag;
    public Action<UnitCard> EndDrag;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

    }

    public void OnPointerClick(PointerEventData eventData) => PointerClick?.Invoke(this);
    public void OnBeginDrag(PointerEventData eventData) => BeginDrag?.Invoke(this);
    public void OnDrag(PointerEventData eventData) => Drag?.Invoke(this, eventData);
    public void OnEndDrag(PointerEventData eventData) => EndDrag?.Invoke(this);

    public void Inst(Unit unit, Color color)
    {
        Unit = unit;

        foreach (Slider slider in _healthSlider)
        {
            slider.fillRect.gameObject.GetComponent<Image>().color = color;
            slider.maxValue = unit.UnitType.MaxHealth;
            slider.gameObject.SetActive(false);
        }

        for (int n = 0; n < unit.WarriorsHealth.Count; n++)
        {
            _healthSlider[n].gameObject.SetActive(true);
            _healthSlider[n].value = unit.WarriorsHealth[n];
        }

        _image.sprite = unit.UnitType.Icon;

    }



}
