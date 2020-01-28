using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Controller : MonoBehaviour
{
    public Text scoreText;
    public Text livesText;

    public Transform level2Spawn;
    public GameObject Player;
    public GameObject mainCamera;
    public GameObject canvas;
    public Player_Controller playerController;
    public AudioClip deathSound;



    public GameObject loseScreen;
    public GameObject winScreen;
    public bool gameOver;
    public bool winGame;

    private float startLives = 3;
    private AudioSource source;

    public float score;
    public float cloudCount;
    public float lives;

    public bool isStage2 = false;
    public bool gotoStage2;
    
    void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        
    }
    void Start()
    {
        score = 0;
        livesText.text = "";
        scoreText.text = "";

        lives = 3;
        SetLivesText();
        SetScoreText();

        lives = startLives;
        source = GetComponent<AudioSource>();
    }


        
    void Update()
    {
        SetScoreText();

        if( lives <= 0)
        {
            if(!gameOver)
            {
                gameOver = true;
                loseScreen.SetActive(true);
                source.PlayOneShot(deathSound, 1f);


            }
        }

        if(score >= 110)
        {
            if(!isStage2)
            Level2();
            if(isStage2 && score >= 404 && !winGame)
            {
                winGame = true;
                winScreen.SetActive(true);
            }
        }
        
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
    }

    public void EnemyHit()
    {
        lives--;
        SetLivesText();
       
    }
    public void CloudCollected()
    {
        cloudCount++;
        SetScoreText();
    }

    public void AddScore(float scorem)
    {
        score += scorem;
    }

    public void Level2()
    {
        Player.transform.position = level2Spawn.transform.position;
        isStage2 = true;
        mainCamera.transform.position = new Vector3(420, 0, -50);
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

}
