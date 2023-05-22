using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleMenu : Singleton<BattleMenu>
{
    [field: SerializeField] public List<Character> Characters{ get; private set; }
    [field: SerializeField] public List<UnitType> UnitTypes { get; private set; }

    public Button StartBattleButton;

    [SerializeField] UnitsDisplay _playerUnitsDisplay;
    [SerializeField] UnitsDisplay _aiUnitsDisplay;

    private void Update()
    {
        StartBattleButton.interactable = (GameManager.Instance.Player.Units.Count > 0 && GameManager.Instance.AI.Units.Count > 0);
    }

    public Character GetNextCharacter(Character current)
    {
        int currentNumber = Characters.IndexOf(current);

        currentNumber = Mathf.RoundToInt(Mathf.Repeat(++currentNumber, Characters.Count));

        return Characters[currentNumber];

    }

    public void OnBattle()
    {
        GameManager.Instance.LoadNewScene("Tactical");

    }

    public void OnBack() 
    {
        GameMenuManager.Instance.SwitchMenu("MainMenu");

    }

    void OnEscape()
    {
        OnBack();

    }

    #region OnEnable/OnDisable

    private void OnEnable()
    {
        _playerUnitsDisplay.Inst(GameManager.Instance.Player);

        _aiUnitsDisplay.Inst(GameManager.Instance.AI);
        InputManager.Instance.OnEscape += OnEscape;
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnEscape -= OnEscape;
        }

    }

    #endregion

}
