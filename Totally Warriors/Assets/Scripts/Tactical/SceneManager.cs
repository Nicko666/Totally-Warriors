using UnityEngine;

public class SceneManager : Singleton<SceneManager>
{
    public GameObject FloatinNumber;

    public void InstantiateFloatingNumber(int number, Vector3 position, Color color)
    {
        FloatingNunber fn = Instantiate(FloatinNumber, position, new()).GetComponent<FloatingNunber>();
        fn.SetNumber(number, color);

    }
}
