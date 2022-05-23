using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowFade : MonoBehaviour
{
    public GameObject rainbow;
    private bool isDisplaying = false;
    private float fadeTime = 1.5f;
    

    public void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.moveScore > 40 && !isDisplaying)
        {
            Invoke("DisplayRainbow", 0);
            isDisplaying = true;
        }
    }

    public void DisplayRainbow()
    {
        StartCoroutine(FadeGameObjectFullAlpha(fadeTime, rainbow.GetComponent<SpriteRenderer>()));
    }

    public IEnumerator FadeGameObjectFullAlpha(float fadeTime, SpriteRenderer sr)
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);

        while(sr.color.a < 0.7f)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b,
                sr.color.a + (Time.deltaTime / fadeTime));
            yield return null;
        }

        StartCoroutine(FadeGameObjectZeroAlpha(fadeTime, sr));
    }

    public IEnumerator FadeGameObjectZeroAlpha(float fadeTime, SpriteRenderer sr)
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.7f);

        while (sr.color.a > 0.0f)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b,
                sr.color.a - (Time.deltaTime / fadeTime));
            yield return null;
        }

        isDisplaying = false;
    }
}
