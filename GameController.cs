using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public static GameController instance;
    public BoxSpawner boxSpawner;
    public AudioSource audioSource;
    public AudioClip[] sounds;
    public GameObject music;

    [HideInInspector] public BoxScript currentBox;
    [HideInInspector] public int score;
    [HideInInspector] public int moveScore;
    [HideInInspector] public bool OneTime;
    public int totalObjectCount;

    public  float scaleTime = 1.2f;
    public int scoreBonusOnCollision;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        boxSpawner.SpawnBox();
        totalObjectCount = 0;

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DetectInput();
        if (currentBox != null)
            currentBox.Rotate();

        Time.timeScale = scaleTime;

    }

    void DetectInput()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentBox.canMove = false;
            currentBox.DropBox();
        }
    }

    public void SpawnNewBox()
    {
        Invoke("NewBox", 1f);
        totalObjectCount++;
    }

    void NewBox()
    {
        boxSpawner.SpawnBox();
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void PlaySound(int i)
    {
        audioSource.clip = sounds[i];
        audioSource.Play();
    }
}
