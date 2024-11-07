using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    public int lives;

    // Lives UI
    public Image[] livesDisplay;
    public Sprite normalLife;
    public Sprite lostLife;

    // Start is called before the first frame update
    void Start()
    {
        // Get lives from persistant data and update display
        lives = PersistantData.instance.lives;
        updateLivesDisplay();
    }

    public void LoseLife()
    {
        // Get which life to disable
        Image life = livesDisplay[lives - 1];

        // Change Source image to empty Orbo
        life.sprite = lostLife;

        lives--;

        // Update persistent data
        PersistantData.instance.setLives(lives);
    }

    public bool isGameOver()
    {
        return lives <= 0;
    }

    public void updateLivesDisplay()
    {
        // Set lives images based on num lives
        for (int i = 0; i < livesDisplay.Length; i++)
        {
            if (i < lives)
            {
                livesDisplay[i].sprite = normalLife;
            }
            else
            {
                livesDisplay[i].sprite = lostLife;
            }
        }
    }
}
