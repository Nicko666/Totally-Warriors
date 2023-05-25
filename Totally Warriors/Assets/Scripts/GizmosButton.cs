using UnityEngine;
using UnityEngine.UI;

public class GizmosButton : MonoBehaviour
{
    [SerializeField] Button _button;
    bool _value;

    private void Start()
    {
        SceneTActions.Instance.OnUnitsTCreated += OnUnitsTCreated;
        _button.onClick.AddListener(OnClick);
    }

    void OnUnitsTCreated()
    {
        OnClick();
    }

    void OnClick()
    {
        SceneTActions.Instance.OnGismosSwitchNotify(_value);
        _value = !_value;
    }


}
