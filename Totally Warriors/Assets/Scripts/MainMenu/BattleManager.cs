using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : Singleton<BattleManager>
{
    [field: SerializeField] public List<Character> Characters{ get; private set; }
    [field: SerializeField] public List<UnitType> UnitTypes { get; private set; }


    public Button StartBattleButton;

    [SerializeField] UnitsDisplay _playerUnitsDisplay;
    [SerializeField] UnitsDisplay _aiUnitsDisplay;

    private void Start()
    {
        _playerUnitsDisplay.Inst(GameManager.Instance.Player);
        _aiUnitsDisplay.Inst(GameManager.Instance.AI);
    }

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

    public void StartBattle()
    {
        GameManager.Instance.LoadNewScene("Tactical");
    }

}
