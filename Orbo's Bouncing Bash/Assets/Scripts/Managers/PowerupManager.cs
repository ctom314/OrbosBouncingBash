using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerupManager : MonoBehaviour
{
    public bool powerupActive;
    public bool powerupSpawned;
    public int powerupType = 1;
    public float duration = 5;
    public float powerupChance = 0.25f;

    public PaddleMovement paddle;
    public Ball ball;

    // Powerup UI
    public GameObject powerupUI;
    public Slider powerupDuration;
    public TextMeshProUGUI powerupTitle;

    private List<string> powerupNames = new List<string>();
    private PauseManager pm;
    private GameManager gm;

    // Timer
    private float time;

    // Default ball/paddle vars
    private float defaultBallSpeed;
    private float defaultPaddleSpeed;
    private int defaultBallDamage;

    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<GameManager>();
        pm = GetComponent<PauseManager>();

        powerupActive = false;
        powerupSpawned = false;

        // Get default ball/paddle vars
        defaultBallSpeed = ball.speed;
        defaultPaddleSpeed = paddle.speed;
        defaultBallDamage = gm.ballDamage;

        // Set powerup names
        powerupNames.Add("Slow Clock");
        powerupNames.Add("Coffee Rush");
        powerupNames.Add("Moon Power");
    }

    // Update is called once per frame
    void Update()
    {
        if (powerupActive && !pm.isPaused)
        {
            powerupEffect(duration, powerupType);
        }
    }

    public void cancelPowerup()
    {
        if (powerupActive)
        {
            powerupActive = false;

            // Revert powerup effects based on type
            if (powerupType == 1)
            {
                // Slow Clock
                paddle.speed = defaultPaddleSpeed;
            }
            else if (powerupType == 2)
            {
                // Coffee Rush
                ball.speed = defaultBallSpeed;
                paddle.speed = defaultPaddleSpeed;
            }
            else if (powerupType == 3)
            {
                // Moon Power
                gm.ballDamage = defaultBallDamage;
            }

            // Cancel powerup timers
            StopAllCoroutines();

            // Hide powerup display
            hidePowerupDisplay();
        }
    }

    private void powerupEffect(float duration, int type)
    {
        // Show powerup display
        showPowerupDisplay();
        
        // Setup slider
        powerupDuration.maxValue = duration;

        // Update powerup title (powerupType starts from 1)
        powerupTitle.text = powerupNames[type - 1];

        // Increment timer
        time += Time.unscaledDeltaTime;

        // Update powerup duration
        powerupDuration.value = duration - time;

        // If powerup duration is over, hide UI
        if (time >= duration)
        {
            powerupActive = false;
            time = 0;
            hidePowerupDisplay();
        }
    }

    private void showPowerupDisplay()
    {
        powerupUI.SetActive(true);
    }

    private void hidePowerupDisplay()
    {
        powerupUI.SetActive(false);
    }

    // Powerup effects
    // 1. Slow Clock
    public void slowClock(float slowFactor, int duration)
    {
        StartCoroutine(slowPaddle(slowFactor, duration));
    }

    private IEnumerator slowPaddle(float slowFactor, int duration)
    {
        // Slow paddle
        paddle.speed *= slowFactor;

        // Wait for duration
        yield return new WaitForSeconds(duration);

        // Reset paddle speed
        paddle.speed /= slowFactor;
    }

    // 2. Coffee Rush
    public void coffeeRush(float speedMult, int duration)
    {
        StartCoroutine(boostBallSpeed(speedMult, duration));
    }

    private IEnumerator boostBallSpeed(float speedMult, int duration)
    {
        // Boost ball and paddle speed
        ball.speed *= speedMult;
        paddle.speed *= speedMult;

        // Wait for duration
        yield return new WaitForSeconds(duration);

        // Reset ball and paddle speed
        ball.speed /= speedMult;
        paddle.speed /= speedMult;
    }

    // 3. Moon Power
    public void moonPower(int damageMult, int duration)
    {
        StartCoroutine(boostBallDamage(damageMult, duration));
    }

    private IEnumerator boostBallDamage(int damageMult, int duration)
    {
        // Boost ball damage
        gm.ballDamage *= damageMult;

        // Wait for duration
        yield return new WaitForSeconds(duration);

        // Reset ball damage
        gm.ballDamage /= damageMult;
    }
}
