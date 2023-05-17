using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;

public class TacticalSceneManager : MonoBehaviour
{
    [SerializeField] TacticalMap _map;
    [SerializeField] TacticalUI _ui;
    [SerializeField] UnitT _unitPreefab;
    [SerializeField] UnitIcon _unitIcon;
    [SerializeField] UnitCardSelection _unitCard;
    [SerializeField] PlayerBehaviourT _playerBefaviour;
    [SerializeField] MessageBox _messageBox;

    public Character Player { get; private set; }
    public Character AI { get; private set; }

    public List<UnitT> playerUnits { get; private set; }
    public List<UnitT> aiUnits { get; private set; }

    float _time;
    MessageBox CurrentMessageBox;

    public float PowerBallance
    {
        get
        {
            float playerPowers = 0;
            foreach (var unit in playerUnits) playerPowers += unit.Warriors.Count;
            float aiPowers = 0;
            foreach (var unit in aiUnits) aiPowers += unit.Warriors.Count;

            return playerPowers / (playerPowers + aiPowers);
        }
    }
    public Character Lider
    {
        get
        {
            if (PowerBallance > 0.5)
            {
                return Player;
            }
            else if (PowerBallance < 0.5)
            {
                return AI;
            }

            return null;
        }
        
    } 


    private void Update()
    {
        if (_time > 0)
        {
            _time -= Time.deltaTime;
            _ui.tacticalStatus.ShowTime(_time);
        }
        else if(CurrentMessageBox == null)
        {
            OutOfTime();
        }
    }

    public void InstPvCScene(CharacterManager player, CharacterManager ai, float time)
    {
        _time = time;

        Player = player.Character;
        AI = ai.Character;
        
        playerUnits = new List<UnitT>();
        aiUnits = new List<UnitT>();
        InstUnits(player, _map.LowPositions, _ui.LowUnitsCanvas);
        InstUnits(ai, _map.UpPositions, _ui.UpUnitsCanvas);

        _map.MapClick += _playerBefaviour.OnMapClick;

        _ui.SetStatus(this);

        var AIBehaviour = Instantiate(ai.Character.AIBehaviour.gameObject).GetComponent<AIBehaviour>();
        AIBehaviour.Inst(this);

        void InstUnits(CharacterManager characterManager, Transform[] mapTransforms, UnitsCanvas unitsCanvas)
        {
            for (int i = 0; i < characterManager.Units.Count; i++)
            {
                var unitTO = Instantiate(_unitPreefab.gameObject, _map.transform).GetComponent<UnitT>();
                unitTO.Inst(characterManager.Units[i], characterManager.Character, mapTransforms[i]);

                var unitIcon = Instantiate(_unitIcon.gameObject, unitTO.transform).GetComponent<UnitIcon>();
                unitIcon.Inst(characterManager.Units[i].UnitType, unitTO);

                var unitCard = Instantiate(_unitCard.gameObject, unitsCanvas.transform).GetComponent<UnitCardSelection>();
                unitCard.transform.localPosition = unitsCanvas.Positions(characterManager.Units.Count)[i];
                unitCard.Inst(characterManager.Units[i].UnitType, unitTO);

                if (characterManager.Player)
                {
                    playerUnits.Add(unitTO);
                    unitTO.DefeatedAction += RemovePlayerUnit;
                    unitIcon.Click += _playerBefaviour.OnPlayerUnitClick;
                    unitCard.Click += _playerBefaviour.OnPlayerUnitClick;
                }
                else
                {
                    aiUnits.Add(unitTO);
                    unitTO.DefeatedAction += RemoveAIUnit;
                    unitIcon.Click += _playerBefaviour.OnAIUnitClick;
                    unitCard.Click += _playerBefaviour.OnAIUnitClick;
                }
            }
        }

    }

    void RemovePlayerUnit(UnitT unitTObject)
    {
        unitTObject.DefeatedAction -= RemovePlayerUnit;
        playerUnits.Remove(unitTObject);
        WinnerStatusCheck();

    }

    void RemoveAIUnit(UnitT unitTObject)
    {
        unitTObject.DefeatedAction -= RemoveAIUnit;
        aiUnits.Remove(unitTObject);
        WinnerStatusCheck();

    }

    public void WinnerStatusCheck()
    {
        if (playerUnits.Count <= 0)
        {
            WinMessage($"{AI.Name} wins\n", false);

        }
        else if (aiUnits.Count <= 0)
        {
            WinMessage("Enemy Defeated\n", true);

        }
    }

    public void OutOfTime()
    {
        bool winStatus;
        if (Lider == null)
        {
            winStatus = Random.Range(0, 2) == 0;
        }
        else
        {
            winStatus = Lider == Player;
        }
        
        WinMessage("Out of time\n", winStatus);

    }

    public void WinMessage(string stage, bool win)
    {
        string status = win ? "Wins" : "Loss";
        string text = stage + $"{Player.Name} {status}!";
        Time.timeScale = 0f;
        CurrentMessageBox = Instantiate(_messageBox.gameObject, _ui.transform).GetComponent<MessageBox>();
        CurrentMessageBox.MessageText(text);
    }

}
