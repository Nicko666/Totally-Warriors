using System;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private Controls controls;

    private void Awake()
    {
        controls = new();
        controls.Enable();
        controls.AndroidInput.Escape.performed += OnEscapeNotify;

    }

    public void OnEscapeNotify(InputAction.CallbackContext context) => OnEscape?.Invoke();

    //public Action OnHome;
    public Action OnEscape;
    //public Action OnMenu;

}

//float enableTime;
//float spaceTime = 0.1f;

//private void OnEnable()
//{
//    enableTime = Time.time;
//}

//private void Update()
//{
//    if (enableTime + spaceTime < Time.time & Application.platform == RuntimePlatform.Android)
//    {
//        if (Input.GetKey(KeyCode.Home))
//        {
//            OnHome?.Invoke();
//        }
//        if (Input.GetKey(KeyCode.Escape))
//        {
//            OnEscape?.Invoke();
//        }
//        if (Input.GetKey(KeyCode.Menu))
//        {
//            OnHome?.Invoke();
//        }

//    }

//}
