using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject darkScreenOverlay;
    public GameObject ball;
    public GameObject paddle;

    public TextMeshProUGUI scoreText;
    public bool isWin;

    private PowerupManager pm;

    // Start is called before the first frame update
    void Start()
    {
        isWin = false;

        pm = GetComponent<PowerupManager>();
    }

    public void triggerWin()
    {
        isWin = true;

        // Cancel any active powerups
        pm.cancelPowerup();

        // Unload ball
        ball.SetActive(false);

        // Disable paddle movement
        paddle.GetComponent<PaddleMovement>().canMove = false;

        // Destroy any powerups
        GameObject[] powerups = GameObject.FindGameObjectsWithTag("Powerup");
        foreach (GameObject powerup in powerups)
        {
            Destroy(powerup);
        }

        // Show overlay
        darkScreenOverlay.SetActive(true);

        // Show win screen
        winScreen.SetActive(true);

        // Set score text
        scoreText.text = "Current Score: " + GetComponent<ScoreManager>().score.ToString("00000");
    }
}
