using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneTManager : Singleton<SceneTManager>
{
    [field: SerializeField] public MapT Map { get; private set; }
    [field: SerializeField] public CharacterManagerT Player { get; private set; }
    [field: SerializeField] public CharacterManagerT AI { get; private set; }
    [field: SerializeField] public float _time { get; private set; }

    [SerializeField] UnitT _unitPreefab;

    [SerializeField] Canvas _canvas;

    [SerializeField] MessageBox _messageBoxPrefab;

    [SerializeField] List<UnitT> _playerUnits;
    [SerializeField] List<UnitT> _aiUnits;

    private void Start()
    {
        Debug.Log("Scene Manager Start");

        GameManager gm = GameManager.Instance;

        _time = gm.Time;

        //create map

        _playerUnits = CreateUnitsT(gm.Player, Map.LowPositions);
        _aiUnits = CreateUnitsT(gm.AI, Map.UpPositions);
        Player.Inst(gm.Player.Character, _playerUnits, _aiUnits);
        AI.Inst(gm.AI.Character, _aiUnits, _playerUnits);
        
        SceneTActions.Instance.OnUnitsCreatedNotify();
        
    }

    List<UnitT> CreateUnitsT(CharacterManager characterManager, Transform[] mapTransforms)
    {
        List<UnitT> units = new();
        for (int i = 0; i < characterManager.Units.Count; i++)
        {
            units.Add(Instantiate(_unitPreefab.gameObject, Map.transform).GetComponent<UnitT>());
            units.Last().Inst(characterManager.Units[i], characterManager.Character, mapTransforms[i]);
        }

        return units;

    }

    
    public void OnUnitDefeated(UnitT unitT)
    {
        if (_playerUnits.Contains(unitT))
        {
            _playerUnits.Remove(unitT);
        }
        else if (_aiUnits.Contains(unitT))
        {
            _aiUnits.Remove(unitT);
        }

        Debug.Log("Destroy " + unitT.gameObject.name);
        Destroy(unitT);

        WinnerStatusCheck();
    }

    private void Update()
    {
        if (_time > 0)
        {
            _time -= Time.deltaTime;
        }
        else
        {
            OutOfTime();
        }

    }

    public void OutOfTime()
    {
        bool winStatus;
        if (Lider == null)
        {
            winStatus = UnityEngine.Random.Range(0, 100) < 50;
        }
        else
        {
            winStatus = Lider == Player;
        }

        WinMessage("Out of time\n", winStatus);

    }

    public float PowerBallance
    {
        get
        {
            float playerPowers = 0;
            foreach (var unit in Player.MyUnits) playerPowers += unit.Warriors.Count;
            float aiPowers = 0;
            foreach (var unit in AI.MyUnits) aiPowers += unit.Warriors.Count;

            return playerPowers / (playerPowers + aiPowers);
        }
    }

    public Character Lider
    {
        get
        {
            if (PowerBallance > 0.5)
            {
                return Player.Character;
            }
            else if (PowerBallance < 0.5)
            {
                return AI.Character;
            }

            return null;
        }
        
    } 

    public void WinnerStatusCheck()
    {
        if (Player.MyUnits.Count <= 0)
        {
            WinMessage($"{AI.Character.Name} wins\n", false);

        }
        else if (AI.MyUnits.Count <= 0)
        {
            WinMessage("Enemy Defeated\n", true);

        }
    }

    public void WinMessage(string stage, bool win)
    {
        string status = win ? "Wins" : "Loss";
        string text = stage + $"{Player.Character.Name} {status}!";
        MessageBox messageBox = Instantiate(_messageBoxPrefab.gameObject, _canvas.transform).GetComponent<MessageBox>();
        messageBox.MessageText(text);
        messageBox.Click += OnEnd;

    }

    void OnEnd()
    {
        GameManager.Instance.LoadNewScene("MainMenu");
        //CurrentMessageBox.Click -= OnEnd;

    }

    #region OnEnable/OnDisable

    private void OnEnable()
    {
        SceneTActions.Instance.OnUnitDefeated += OnUnitDefeated; ;
    }

    private void OnDisable()
    {
        SceneTActions.Instance.OnUnitDefeated += OnUnitDefeated; ;
    }

    #endregion

}
