using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI highscore;
    private int highScoreValue = 0;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        score.text = GameController.instance.score.ToString();

        if (GameController.instance.score > highScoreValue)
        {
            highscore.text = score.text;
            highScoreValue = GameController.instance.score;
        }
    }
}
