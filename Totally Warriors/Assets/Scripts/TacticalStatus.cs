using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TacticalStatus : MonoBehaviour
{
    [SerializeField] Image _player;
    [SerializeField] Image _ai;
    [SerializeField] Slider _slider;
    [SerializeField] TMP_Text _text;

    private void OnEnable()
    {
        SceneTActions.Instance.OnUnitsCreated += SetColors;
        SceneTActions.Instance.OnUnitsCreated += SetNumber;

    }

    private void Update()
    {
        ShowTime(SceneTManager.Instance.Time);
    }

    public void SetColors()
    {
        _player.color = SceneTManager.Instance.Player.Character.Color;
        _ai.color = SceneTManager.Instance.AI.Character.Color;

    }    

    public void SetNumber() => _slider.value = SceneTManager.Instance.PowerBallance;

    public void ShowTime(float time)
    {
        int timeLeft = Mathf.FloorToInt(time);

        _text.text = timeLeft.ToString();

    }

}
