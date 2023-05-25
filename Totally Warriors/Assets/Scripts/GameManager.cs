using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : AwakeSingleton<GameManager>
{
    [field: SerializeField] public CharacterManager Player { get; private set; }
    [field: SerializeField] public CharacterManager AI { get; private set; }
    [field: SerializeField] public float Time { get; private set; }

    public void ChangeTime(float time) => Time = time;

}
