using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    // UI : Pause Menu
    public GameObject pauseMenu;
    public GameObject pauseTip;
    public GameObject pauseDarkenBackground;
    
    public Boolean isPaused;

    private float prevTimeScale;
    private WinManager wm;
    private GameOverManager gom;
    private GameButtonHandler gbh;

    // Controller support
    private Controls controls;

    // Pause menu time vars to ensure pressing Q does not pause and quit game at the same time
    private float pausePressTime = 0f;
    private float quitDelay = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        wm = GetComponent<WinManager>();
        gom = GetComponent<GameOverManager>();
        gbh = GetComponent<GameButtonHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        pauseMenuButtons();
    }

    // ----- For controller support -----
    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Pause.performed += OnPausePress;
    }

    private void OnDisable()
    {
        controls.Player.Pause.performed -= OnPausePress;
        controls.Disable();
    }

    private void OnPausePress(InputAction.CallbackContext context)
    {
        togglePause();
    }

    private void togglePause()
    {
        if (!wm.isWin && !gom.isGameOver)
        {
            if (!isPaused)
            {
                pauseGame();
                isPaused = true;
                pausePressTime = Time.unscaledTime;
            }
            else
            {
                resumeGame();
                isPaused = false;
            }
        }
    }

    // --------------------------------------

    public void pauseMenuButtons()
    {
        // Only allow pausing when the game is not over
        if ((Input.GetKeyDown(KeyCode.Q)) && !wm.isWin && !gom.isGameOver)
        {
            if (!isPaused)
            {
                // Pause game
                pauseGame();
                isPaused = true;
                pausePressTime = Time.unscaledTime;
            }

            // Quit game after delay
            else if (isPaused && (Time.unscaledTime - pausePressTime) > quitDelay)
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("MainMenu");
            }
        }

        if (isPaused && Input.GetKeyDown(KeyCode.Space))
        {
            // Resume game
            resumeGame();
            isPaused = false;
        }

        if (isPaused && Input.GetKeyDown(KeyCode.R))
        {
            // Restart game
            gbh.playAgain();
        }
    }

    public void pauseGame()
    {
        // Save current time scale
        prevTimeScale = Time.timeScale;

        isPaused = true;

        // Pause game
        Time.timeScale = 0;
        showPauseMenu();
    }

    // Pause Menu Functions
    public void resumeGame()
    {
        // Restore previous time scale
        Time.timeScale = prevTimeScale;

        isPaused = false;

        // Resume game
        hidePauseMenu();
    }

    public void showPauseMenu()
    {
        pauseMenu.SetActive(true);

        // Hide the tip
        pauseTip.SetActive(false);

        // Darken the background
        pauseDarkenBackground.gameObject.SetActive(true);
    }

    public void hidePauseMenu()
    {
        pauseMenu.SetActive(false);

        // Show the tip
        pauseTip.SetActive(true);

        // Lighten the background
        pauseDarkenBackground.gameObject.SetActive(false);
    }
}
