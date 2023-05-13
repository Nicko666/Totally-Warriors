using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TacticalSceneManager : MonoBehaviour
{
    [SerializeField] TacticalMap _map;
    [SerializeField] TacticalUI _ui;
    [SerializeField] UnitTObject _unitPreefab;
    [SerializeField] UnitIcon _unitIcon;
    [SerializeField] UnitCard _unitCard;
    [SerializeField] PlayerEventSystem _playerEventSystem;

    Character _player;
    Character _ai;

    List<UnitTObject> playerUnits;
    List<UnitTObject> aiUnits;

    float _time;

    private void Update()
    {
        if (_time > 0)
        {
            _time -= Time.deltaTime;
            _ui.tacticalStatus.ShowTime(_time);
        }
        else
        {
            OutOfTime();
        }
    }

    public void SetPvCScene(CharacterManager player, CharacterManager ai, float time)
    {
        _time = time;
        _player = player.Character;
        _ai = ai.Character;
        
        playerUnits = new List<UnitTObject>();
        aiUnits = new List<UnitTObject>();

        _playerEventSystem.Inst(player.Character.Name);
        var AIBehaviour = Instantiate(ai.Character.AIBehaviour.gameObject).GetComponent<AIBehaviour>();

        _map.MapClick += _playerEventSystem.OnMapClick;

        InstUnits(player, _map.LowPositions, _ui.LowUnitsCanvas);
        InstUnits(ai, _map.UpPositions, _ui.UpUnitsCanvas);

        _ui.SetStatus(playerUnits, aiUnits);

        void InstUnits(CharacterManager characterManager, Transform[] mapTransforms, UnitsCanvas unitsCanvas)
        {
            for (int i = 0; i < characterManager.Units.Count; i++)
            {
                var unitTO = Instantiate(_unitPreefab.gameObject, mapTransforms[i].position, _unitPreefab.gameObject.transform.rotation, _map.transform).GetComponent<UnitTObject>();
                unitTO.Inst(characterManager.Units[i], characterManager.Character);

                var unitIcon = Instantiate(_unitIcon.gameObject, unitTO.transform).GetComponent<UnitIcon>();
                unitIcon.Inst(characterManager.Units[i].UnitType, unitTO);

                var unitCard = Instantiate(_unitCard.gameObject, unitsCanvas.transform).GetComponent<UnitCard>();
                unitCard.transform.localPosition = unitsCanvas.Positions(characterManager.Units.Count)[i];
                unitCard.Inst(characterManager.Units[i].UnitType, unitTO);

                if (characterManager.Player)
                {
                    playerUnits.Add(unitTO);
                    unitTO.DefeatedAction += RemovePlayerUnit;
                    unitIcon.Click += _playerEventSystem.OnPlayerUnitClick;
                    unitCard.Click += _playerEventSystem.OnPlayerUnitClick;
                }
                else
                {
                    aiUnits.Add(unitTO);
                    unitTO.DefeatedAction += RemoveAIUnit;
                    unitIcon.Click += _playerEventSystem.OnAIUnitClick;
                    unitCard.Click += _playerEventSystem.OnAIUnitClick;
                }
            }
        }

    }

    void RemovePlayerUnit(UnitTObject unitTObject)
    {
        unitTObject.DefeatedAction -= RemovePlayerUnit;
        playerUnits.Remove(unitTObject);
        WinnerStatus();

    }

    void RemoveAIUnit(UnitTObject unitTObject)
    {
        unitTObject.DefeatedAction -= RemoveAIUnit;
        aiUnits.Remove(unitTObject);
        WinnerStatus();

    }

    public void WinnerStatus()
    {
        if (playerUnits.Count <= 0)
        {
            Debug.Log($"{_ai.Name} Win");
        }
        else if (aiUnits.Count <= 0)
        {
            Debug.Log($"{_player.Name} Win");
        }
    }

    public void OutOfTime()
    {
        Time.timeScale = 0f;
        Debug.Log($"Out of time");

    }

    public Character GetLider()
    {
        if (playerUnits.Count > aiUnits.Count)
        {
            return _player;
        }
        else if (playerUnits.Count < aiUnits.Count)
        {
            return _ai;
        }
        
        return null;
        
    } 

}
