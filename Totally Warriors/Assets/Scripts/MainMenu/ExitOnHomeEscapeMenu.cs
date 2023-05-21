using UnityEngine;

public class ExitOnHomeEscapeMenu : MonoBehaviour
{
    float enableTime;
    float spaceTime = 0.1f;

    private void OnEnable()
    {
        enableTime = Time.time;
    }

    private void Update()
    {
        if (enableTime + spaceTime < Time.time & Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Home) || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Menu))
            {
                Application.Quit();
                return;
            }
        }

    }
    
}
