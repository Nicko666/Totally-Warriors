using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<T>();
                
                if (_instance == null)
                {
                    var singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";
                }

                DontDestroyOnLoad(_instance);

                SceneManager.sceneUnloaded += (scene) => 
                {
                    if (_instance != null)
                        Destroy(_instance.gameObject);
                };

            }
            return _instance;
        }

    }


}
