using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleTimeSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text time;


    private void Start()
    {
        slider.onValueChanged.AddListener(UpdateData);
        UpdateData(slider.value);

    }

    void UpdateData(float value)
    {
        time.text = Mathf.RoundToInt(slider.value).ToString();
        GameManager.Instance.ChangeTime(value);
    }
}
