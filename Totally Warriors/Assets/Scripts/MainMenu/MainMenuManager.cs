using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] BattleManager _battleManager;

    private void OnEnable()
    {
        _battleManager.Inst(GameManager.Instance.Player, GameManager.Instance.AI);

    }

    public void Exit() => Application.Quit();
}
