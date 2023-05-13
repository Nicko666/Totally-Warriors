using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] CharacterManager Player;
    [SerializeField] CharacterManager AI;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        var tacticalScene = FindObjectOfType<TacticalSceneManager>();
        tacticalScene.SetPvCScene(Player, AI, 60);
    }

}
