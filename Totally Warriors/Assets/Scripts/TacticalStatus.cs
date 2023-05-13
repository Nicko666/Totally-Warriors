using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TacticalStatus : MonoBehaviour
{
    [SerializeField] Image _player;
    [SerializeField] Image _ai;
    [SerializeField] Slider _slider;
    [SerializeField] TMP_Text _text;

    public void SetColors(Color playerColor, Color aiColor)
    {
        _player.color = playerColor;
        _ai.color = aiColor;

    }

    public void SetNumber(float playerUnits, float aiUnits)
    {
        _slider.value = playerUnits / (playerUnits + aiUnits);

    }

    public void ShowTime(float time)
    {
        int timeLeft = Mathf.FloorToInt(time);

        _text.text = timeLeft.ToString();

    }

}
