using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitIcon : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] UnitT _unitT;
    [Header("Fill in prefab")]
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] Slider[] _healthSlider;
    [SerializeField] Image _image;


    private void Update()
    {
        if (_unitT != null & _unitT.Warriors.Count > 0)
            transform.position = _unitT.UnitCenter + Vector3.up;
    }

    private void OnEnable()
    {
        _unitT.Selected += Highlite;
        _unitT.UnitTakeDamageAction += UpdateHealthData;

    }

    private void Start()
    {
        _image.sprite = _unitT.UnitType.Icon;

        foreach (var slider in _healthSlider)
        {
            slider.fillRect.gameObject.GetComponent<Image>().color = _unitT.Color;
            slider.maxValue = _unitT.UnitType.MaxHealth;
        }

        UpdateHealthData(_unitT.WarriorsHealth);

    }

    private void OnDisable()
    {
        _unitT.Selected -= Highlite;
        _unitT.UnitTakeDamageAction -= UpdateHealthData;
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

    public void Highlite(bool value) => _canvasGroup.alpha = (value) ? 0.5f : 0.25f;

    public void OnPointerClick(PointerEventData eventData) => _unitT.ClickAction?.Invoke(_unitT);

}
