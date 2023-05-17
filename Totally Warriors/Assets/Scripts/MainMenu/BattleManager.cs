using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : Singleton<BattleManager>
{
    [field: SerializeField] public List<Character> Characters{ get; private set; }
    [field: SerializeField] public List<Unit> Units { get; private set; }

    public CharacterManager Player;
    public CharacterManager AI;

    public Button StartBattleButton;

    private void Update()
    {
        StartBattleButton.interactable = (Player.Units.Count > 0 && AI.Units.Count > 0);
    }

    public Character GetNextCharacter(Character current)
    {
        int currentNumber = Characters.IndexOf(current);

        currentNumber = Mathf.RoundToInt(Mathf.Repeat(++currentNumber, Characters.Count));

        return Characters[currentNumber];

    }

    public void StartBattle()
    {

    }

}
