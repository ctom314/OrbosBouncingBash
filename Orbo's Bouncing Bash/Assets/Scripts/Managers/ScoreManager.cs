using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        // Get score from persistant data
        score = PersistantData.instance.score;
    }

    // Update is called once per frame
    void Update()
    {
        updateScore();
    }

    private void updateScore()
    {
        // Cap score if over 5 digits
        if (score > 99999)
        {
            score = 99999;
        }

        scoreText.text = "Score: " + score.ToString("00000");
    }

    public void addScore(int amount)
    {
        score += amount;

        // Update persistant data
        PersistantData.instance.addScore(amount);
    }

    public void resetScore()
    {
        score = 0;
        PersistantData.instance.resetScore();
    }
}
