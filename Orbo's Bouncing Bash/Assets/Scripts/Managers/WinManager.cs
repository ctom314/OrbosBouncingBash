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

    // Start is called before the first frame update
    void Start()
    {
        isWin = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void triggerWin()
    {
        isWin = true;

        // Unload ball
        ball.SetActive(false);

        // Disable paddle movement
        paddle.GetComponent<PaddleMovement>().canMove = false;

        // Show overlay
        darkScreenOverlay.SetActive(true);

        // Show win screen
        winScreen.SetActive(true);

        // Set score text
        scoreText.text = "Current Score: " + GetComponent<ScoreManager>().score.ToString("0000");
    }
}
