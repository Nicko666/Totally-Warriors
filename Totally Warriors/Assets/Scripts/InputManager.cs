using System;
using UnityEngine.InputSystem;

public class InputManager : AwakeSingleton<InputManager>
{
    public Controls controls;

    void OnEscapeNotify(InputAction.CallbackContext context) => OnEscape?.Invoke();

    public Action OnEscape;
    //public Action OnHome;
    //public Action OnMenu;

    private void OnEnable()
    {
        controls = new();
        controls.Enable();
        controls.AndroidInput.Escape.performed += OnEscapeNotify;
    }

    private void OnDisable()
    {
        if (controls != null) 
        {
            controls.AndroidInput.Escape.performed -= OnEscapeNotify;
            controls.Disable();
            controls = null;
        }
    }

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
