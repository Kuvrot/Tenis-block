using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int score;
    public int Goal;
    public int playerHP = 1;
    public Transform target;
    public GameObject player;
    public BallController ball;
    static public Transform g_ball;
    public bool endLevel , lostLevel;
    public GameObject victoryScreen, LooseScreen;
    public AudioClip clapping;
    AudioSource asrce;

    public Texture2D crosshair;

    public List<GameObject> HP_balls;

    public string nextLevelName = "Level_";

    public AudioSource whistler;

    [HideInInspector] public bool bulletHitPlayer;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.SetCursor(crosshair, new Vector2(32, 32), CursorMode.ForceSoftware);

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (ball == null)
        {
            ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallController>();
        }

        g_ball = ball.transform;

        asrce = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        if (score >= Goal && !endLevel)
        {
            if (!asrce.isPlaying)
            {
                asrce.PlayOneShot(clapping);
                victoryScreen.SetActive(true);
                ball.Restart();
                endLevel = true;
            }
        }

        for (int i = 0; i <= playerHP; i++)
        {
            HP_balls[i].SetActive(true);
        }

        if (lostLevel)
        {
            LooseScreen.SetActive(true);
        }

    }

    public void NextLevel() {

        SceneManager.LoadScene(nextLevelName);
       // PauseMenu.paused = false;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //PauseMenu.paused = false;
    }

    public void LostBall()
    {
        if (playerHP >= 0)
        {
            HP_balls[playerHP].SetActive(false);
            HP_balls.Remove(HP_balls[playerHP]);
            playerHP--;
            whistler.Play();

        }
        else
        {
            lostLevel = true;

        }
    }
}
