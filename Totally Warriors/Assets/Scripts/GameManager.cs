using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Character _charactersPreefab;
    [SerializeField] List<Character> _characters;

    [SerializeField] Unit[] _units;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        CreateCharacters(4);

        _characters[0].AddUnit(_units[0]);
        _characters[0].AddUnit(_units[1]);
        _characters[0].AddUnit(_units[2]);
        _characters[1].AddUnit(_units[0]);
        _characters[1].AddUnit(_units[1]);
        _characters[1].AddUnit(_units[2]);


        var tacticalScene = FindObjectOfType<TacticalScene>();
        tacticalScene.SetTacticalScene(_characters[0], _characters[1]);

    }

    void CreateCharacters(int num)
    {
        _characters = new List<Character>();

        _characters.Add(Instantiate(_charactersPreefab.gameObject, transform).GetComponent<Character>());
        _characters.Last().SetCharacter(Color.blue, "Player", true);

        for (int i = 1; i < num; i++)
        {
            Color color = Random.ColorHSV();

            _characters.Add(Instantiate(_charactersPreefab.gameObject, transform).GetComponent<Character>());
            _characters.Last().SetCharacter(color, $"Enemy {i}", false);
        }

    }

}
