using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOtherOnEscapeMenu : MonoBehaviour
{
    [SerializeField] GameObject toEneble;

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
            if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Menu))
            {
                toEneble.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }

    }

}
