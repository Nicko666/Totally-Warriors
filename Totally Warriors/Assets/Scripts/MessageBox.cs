using System;
using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    [SerializeField] TMP_Text _text;

    public Action Click;

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void MessageText(string text) => _text.text = text;

    public void OnClick() => Click?.Invoke();

}
