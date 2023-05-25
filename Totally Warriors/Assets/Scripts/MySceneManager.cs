using UnityEngine.SceneManagement;

public class MySceneManager //: AwakeSingleton<MySceneManager>
{
    //public static bool IsLoadingScene { get; private set; }

    public static void LoadScene(string scene)
    {
        //IsLoadingScene = true;
        SceneManager.LoadScene("Loading");
        //IsLoadingScene = false;
        SceneManager.LoadScene(scene);
    }

}
