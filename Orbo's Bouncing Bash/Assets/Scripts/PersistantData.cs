using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantData : MonoBehaviour
{
    public static PersistantData instance { get; private set; }

    public int score { get; private set; } = 0;
    public int lives { get; private set; } = 3;

    private void Awake()
    {
        // Ensure only one instance of this class exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void addScore(int score)
    {
        this.score += score;
    }

    public void resetScore()
    {
        score = 0;
    }

    public void setLives(int lives)
    {
        this.lives = lives;
    }

    public void resetData()
    {
        score = 0;
        lives = 3;
    }
}
