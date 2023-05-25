using UnityEngine;

public class AwakeSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }
    public static bool isApplicationQuit;

    private void Awake()
    {
        if (Instance != null)
        {
            if (Instance != this)
                Destroy(this);
        }
        else
        {
            if (isApplicationQuit)
                Instance = null;

            Instance = this.gameObject.GetComponent<T>();
            DontDestroyOnLoad(this);
        }

    }

    private void OnApplicationQuit()
    {
        //isApplicationQuit = true;

    }

}
