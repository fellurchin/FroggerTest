using UnityEngine.SceneManagement;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Spawner))]
public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    Spawner frogSpawn;
    [Min(0)] int playerLives;
    [Min(0)] int frogsLeft;
    float timeLimit;
    float timeLeft;
    private int score;
    [SerializeField] Text scoreText;
    [SerializeField] Text livesText;
    [SerializeField] Text timeText;
    [SerializeField] GameObject gameOverPanel;
    public event Action OnDeadEvent;
    public event Action OnWinEvent;

    public int Score { get => score;
        set {
            score = value;
            scoreText.text = string.Format("Score: {00000}", score);
        }
    }

    private void Awake()
    {
        gm = this;
        frogSpawn = GetComponent<Spawner>();
    }

    private void Start()
    {
        playerLives = 5;
        frogsLeft = 5;
        Score = 0;
        timeLimit = 30f;
        livesText.text = "x" + playerLives;
        Invoke("ResetGame", 1f);
    }

    private void Update()
    {
        if (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
            timeText.text = string.Format("{0:#0,0}", timeLeft);
            if (timeLeft <= 0f)
            {
                DeathEvent();
            }
        }
    }

    private void ResetGame()
    {
        timeLeft = timeLimit;
        
        if (playerLives <= 0 || frogsLeft <= 0)
        {
            timeLeft = 0;
            gameOverPanel.SetActive(true);
            Text gameOverText = gameOverPanel.GetComponentInChildren<Text>();
            gameOverText.text += frogsLeft <= 0? "\n YOU WIN": "\n YOU WIN't";
        }
        else
        {
            frogSpawn.SpawnObject();
        }
    }

    public void DeathEvent()
    {
        OnDeadEvent?.Invoke();
        playerLives--;
        livesText.text = "x" + playerLives;
        ResetGame();
    }

    public void WinEvent()
    {
        OnWinEvent?.Invoke();
        frogsLeft--;
        ResetGame();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
