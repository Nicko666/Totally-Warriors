using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] BattleManager _battleManager;
    [SerializeField] GameManager _gameManager;

    private void OnEnable()
    {
        //Instantiate(_gameManager.gameObject);
    }

    private void Start()
    {
        //_battleManager.Inst(GameManager.Instance.Player, GameManager.Instance.AI);

    }

    public void Exit() => Application.Quit();
}
