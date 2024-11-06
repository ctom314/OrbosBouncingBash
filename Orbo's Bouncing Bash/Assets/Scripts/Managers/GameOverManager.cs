using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject darkScreenOverlay;
    public GameObject ball;
    public GameObject paddle;

    public TextMeshProUGUI scoreText;
    public bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void triggerGameOver()
    {
        isGameOver = true;

        // Unload ball
        ball.SetActive(false);

        // Disable paddle movement
        paddle.GetComponent<PaddleMovement>().canMove = false;

        // Show overlay
        darkScreenOverlay.SetActive(true);

        // Show game over screen
        gameOverScreen.SetActive(true);

        // Set score text
        scoreText.text = "Score: " + GetComponent<ScoreManager>().score.ToString("0000");
    }
}
