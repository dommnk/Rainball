using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI moveScore;
    public GameObject moveScoreGUI;
    private GameController gc;
    private float fadeTime;

    private Vector2 initialPos;

    // Use this for initialization
    void Start()
    {
        initialPos = moveScoreGUI.transform.position;
        fadeTime = 0.7f;

        gc = GameController.instance;
        gc.score = 0;

        StartCoroutine(FadeTextToZeroAlpha(0f, moveScoreGUI.GetComponent<TextMeshProUGUI>()));

    }

    // Update is called once per frame
    void Update()
    {
        if (gc.moveScore != 0) 
        {
            if(gc.OneTime)
            {
                StartCoroutine(FadeTextToFullAlpha(fadeTime, moveScoreGUI.GetComponent<TextMeshProUGUI>()));
                StartCoroutine(MoveUp());
                gc.OneTime = false;
            }

            moveScore.text = "+" + gc.moveScore;
        }

        score.text = gc.score.ToString();

    }
     
    public IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }

        StartCoroutine(FadeTextToZeroAlpha(fadeTime, moveScoreGUI.GetComponent<TextMeshProUGUI>()));

    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }

        moveScoreGUI.transform.position = initialPos;
        gc.moveScore = 0;
    }

    public IEnumerator MoveUp()
    {
        float t = fadeTime;
        while(t > 0)
        {
            t -= Time.deltaTime;

            Vector2 pos = moveScoreGUI.transform.position;
            pos.y += 9f * Time.deltaTime;
            moveScoreGUI.transform.position = pos;
            yield return null;
        }

    }
}
