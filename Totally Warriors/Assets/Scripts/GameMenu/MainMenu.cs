using UnityEngine;

public class MainMenu : Singleton<MainMenu>
{
    public void OnCompaign()
    {

    }

    public void OnBattle()
    {
        GameMenuManager.Instance.SwitchMenu("BattleMenu");
    }

    public void OnExit()
    {
        Debug.Log(message: "Application Quit");
        Application.Quit();
    }

    #region OnEnable/OnDisable

    private void OnEnable()
    {
        InputManager.Instance.OnEscape += OnExit;

    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnEscape -= OnExit;
        }

    }

    #endregion
}
