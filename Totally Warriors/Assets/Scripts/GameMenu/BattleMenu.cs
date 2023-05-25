using System.Collections.Generic;
using UnityEngine;
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
        int newCharNum = Characters.IndexOf(current);

        do
        {
            newCharNum = Mathf.RoundToInt(Mathf.Repeat(++newCharNum, Characters.Count));
        }
        while (Characters[newCharNum] == GameManager.Instance.Player.Character || Characters[newCharNum] == GameManager.Instance.AI.Character);

        return Characters[newCharNum];

    }

    public void OnBattle()
    {
        MySceneManager.LoadScene("Tactical");

    }

    public void OnBack() 
    {
        MySceneManager.LoadScene("MainMenu");

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
