using UnityEngine;

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
                
            }

            return _instance;
        }
    }

}
