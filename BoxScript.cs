using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameController gc;
    public GameObject particles;
    public SpriteRenderer sr;

    public float gravityScale = 0.2f;
    public float moveSpeed = 3.25f;
    public int rotateSpeed = 200;
    public float forceStrength = 2f;
    private readonly float minX = -1.8f;
    private readonly float maxX = 1.8f;

    public bool isDestructible;
    private bool ignoreCollision;
    private bool gameOver;
    [HideInInspector]
    public bool canMove;

    void Awake()
    {
        canMove = true;
        gameOver = false;
        ignoreCollision = false;

        gc = GameController.instance;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        rb.gravityScale = 0.0f;
        gc.currentBox = this;
        
    }
    // Start is called before the first frame update
    void Start()
    {

        if (Random.Range(0, 2) == 1)
            moveSpeed *= -1;

    }

    // Update is called once per frame
    void Update()
    {
        MoveBox();
    }

    void MoveBox()
    {
        if(canMove)
        {

            Vector2 pos = rb.position;
            pos.x += moveSpeed * Time.deltaTime;

            if (pos.x > maxX)
            {
                moveSpeed *= -1;
                pos.x += moveSpeed * Time.deltaTime;
            }

            if (pos.x < minX)
            {
                moveSpeed *= -1;
                pos.x += moveSpeed * Time.deltaTime;
            }

            rb.position = pos;

        }
    }

    public void DropBox()
    {
        if (rb != null && gameObject.CompareTag("Bomb"))
            forceStrength *= 3;

        if(rb != null && rb.velocity.y == 0f)
        {
            rb.gravityScale = gravityScale;
            rb.AddForce(new Vector2(0, -forceStrength), ForceMode2D.Impulse);
            gc.scoreBonusOnCollision = 3;
            gc.moveScore = 0;
            gc.OneTime = true;
        }

        
        
    }

    public void Rotate()
    {
        if (rb.velocity.y < -0.1f)
        {
            if (Input.GetKey(KeyCode.A))
                rb.AddTorque(rotateSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.D))
                rb.AddTorque(-rotateSpeed * Time.deltaTime);
        }
    }


    void Landed()
    {
        if (gameOver) return;

        gc.SpawnNewBox();

        Invoke("makeDestructible", 0.75f);

    }

    void makeDestructible()
    {
        isDestructible = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Sidebar")) return;

        if (!ignoreCollision)
        {
            Invoke("Landed", 0); // creates new gameObject for the player to shoot
            ignoreCollision = true;
        }

        if (gameObject.CompareTag("Bomb") && collision.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
            gc.PlaySound(1);
        }

        if(collision.gameObject.CompareTag("Bomb"))
        {
            Destroy(collision.gameObject);
            gc.PlaySound(1);

            GameObject[] objects = GameObject.FindGameObjectsWithTag(gameObject.tag);
            foreach (GameObject obj in objects)
            {
                ParticleEffect();
                Destroy(obj);
                Score(true);
                gc.totalObjectCount--;
            }

            gc.totalObjectCount--;

        }

        if (collision.gameObject.CompareTag(gameObject.tag) && isDestructible)
        {
            Score();
            ParticleEffect();
            gc.PlaySound(0);
            Destroy(gameObject);
            gc.totalObjectCount--;

        }

    }

    private void Score(bool isBomb = false)
    {
        gc.score += gc.scoreBonusOnCollision;
        gc.moveScore += gc.scoreBonusOnCollision;

        if (gc.scoreBonusOnCollision < 40)
            gc.scoreBonusOnCollision *= 2;
        else
            gc.scoreBonusOnCollision += Random.Range(5, 20);

        if(!isBomb)
            gc.scoreBonusOnCollision += Random.Range(2, 4);
       
    }

    private void ParticleEffect()
    {
        ParticleSystem.MainModule settings = particles.GetComponent<ParticleSystem>().main;

        if (sr != null)
            settings.startColor = new ParticleSystem.MinMaxGradient(sr.color);
        else
            settings.startColor = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 0.8f);

        Instantiate(particles, transform.position, transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Over"))
        {
            CancelInvoke("Landed");
            gameOver = true;
            Invoke("DisplayGameOver", 0f);
        }    
    }
   
    void DisplayGameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
