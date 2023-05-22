using UnityEngine;

public class AwakeSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            if (Instance != this)
                Destroy(this);
        }
        else
        {
            Instance = this.gameObject.GetComponent<T>();
            DontDestroyOnLoad(this);
        }

    }
}
