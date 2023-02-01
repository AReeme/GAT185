using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RollerGameManager : Singleton<RollerGameManager>
{
	[Header("Events")]
	[SerializeField] EventRouter startGameEvent;
	[SerializeField] EventRouter stopGameEvent;

	[SerializeField] Slider healthMeter;
    [SerializeField] TMP_Text scoreUI;
    [SerializeField] GameObject titleUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject gameWinUI;

    [SerializeField] AudioSource gameMusic;
    [SerializeField] AudioSource healthPickup;
    [SerializeField] AudioSource coinPickup;
    [SerializeField] AudioSource jumpPickup;

	[SerializeField] EventRouter winGameEvent;
	[SerializeField] GameObject playerPrefab;
    [SerializeField] Transform playerStart;

    public enum State
    { 
        TITLE,
        START_GAME,
        PLAY_GAME,
        GAME_OVER,
        GAME_WIN
    }
    public State state = State.TITLE;
    float stateTimer = 0;

    private void Start()
    {
		winGameEvent.onEvent += SetWin;
	}

	public void Update()
	{
        switch (state)
        {
            case State.TITLE:
                titleUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case State.START_GAME:
                startGameEvent.Notify();
                titleUI.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Instantiate(playerPrefab, playerStart.position, playerStart.rotation);
                gameMusic.Play();
                state = State.PLAY_GAME;
                break;
            case State.PLAY_GAME:
                break;
            case State.GAME_OVER:
                stateTimer -= Time.deltaTime;
                if (stateTimer <= 0)
                {
                    gameOverUI.SetActive(false);
                    state = State.TITLE;
                }
                break;
            case State.GAME_WIN:
				stateTimer -= Time.deltaTime;
				if (stateTimer <= 0)
				{
					gameWinUI.SetActive(false);
					state = State.TITLE;
				}
				break;
            default:
                break;
        }
    }

	public void SetHealth(int health)
    {
        healthPickup.Play();
        healthMeter.value = Mathf.Clamp(health, 0, 100);
    }

    public void SetScore(int score) 
    {
        coinPickup.Play();
        scoreUI.text = "Score: " + score.ToString();
    }

    public void SetJump(int jump)
    {
        jumpPickup.Play();
        jump *= 5;
    }

    public void SetGameOver()
    {
        stopGameEvent.Notify();
        gameOverUI.SetActive(true);
        gameMusic.Stop();
        state = State.GAME_OVER;
        stateTimer = 3;
    }

    public void SetWin()
    {
        gameWinUI.SetActive(true);
        gameMusic.Stop();
        state = State.GAME_WIN;
        stateTimer = 3;
    }

    public void OnStartGame()
    {
        state = State.START_GAME;
    }
}
