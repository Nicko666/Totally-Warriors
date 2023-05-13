using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMarck : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] SpriteRenderer spriteRenderer;
    float ratio;

    private void OnEnable()
    {
        float opacity = spriteRenderer.color.a;
        ratio = opacity / time;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        spriteRenderer.color = new(spriteRenderer.color.a, spriteRenderer.color.g, spriteRenderer.color.b, time * ratio);

        if (time <= 0)
        {
            Destroy(gameObject);
        }
    }
}
