using System;
using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    [SerializeField] TMP_Text _text;

    public Action Click;

    public void MessageText(string text) => _text.text = text;

    public void OnClick() => Click?.Invoke();

}
