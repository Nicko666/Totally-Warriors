using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            if (Instance != this)
                Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

    }

    [field: SerializeField] public CharacterManager Player { get; private set; }
    [field: SerializeField] public CharacterManager AI { get; private set; }
    [field: SerializeField] public float Time { get; private set; }

    public void LoadNewScene(string sceneToLoad)
    {
        SceneManager.LoadScene("Loading");
        SceneManager.LoadScene(sceneToLoad);

    }

}
