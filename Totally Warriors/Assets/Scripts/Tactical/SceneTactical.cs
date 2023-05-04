using System;
using UnityEngine;

public class SceneTactical : Singleton<SceneTactical>
{
    [SerializeField]
    Character _characterPlayer;
    public Character CharacterPlayer => _characterPlayer;
    [SerializeField]
    Character _characterAI;
    public Character CharacterAI => _characterAI;
    [SerializeField]
    MapTactical map;

    [SerializeField]
    GameObject _floatingNumber;
    public GameObject FloatingNumber => _floatingNumber;

    public void InstantiateFloatingNumber(int number, Vector3 position, Color color)
    {
        FloatingNunber fn = Instantiate(FloatingNumber, position, new()).GetComponent<FloatingNunber>();
        fn.SetNumber(number, color);

    }

    private void OnEnable()
    {
        PlaceCharacter(_characterPlayer, map.DownPositions);
        PlaceCharacter(_characterAI, map.UpPositions);

    }

    public void PlaceCharacter(Character character, Transform[] transform)
    {
        Unit[] units = character.Units;

        for (int i = 0; i < units.Length; i++)
        {
            Unit unit = Instantiate(units[i], transform[i].position, transform[i].rotation);
            unit.SetUnit(character.Name, character.Color);
        }

    }

    public Action<Vector3> MapClick;

    public Action<Unit> UnitClick;

}
