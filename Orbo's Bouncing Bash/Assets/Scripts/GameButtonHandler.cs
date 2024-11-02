using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtonHandler : MonoBehaviour
{
    private PauseManager pm;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PauseManager>();
    }

    public void resumeGame()
    {
        pm.resumeGame();
    }

    public void returnToMainMenu()
    {
        // Load main menu
        SceneManager.LoadScene("MainMenu");
    }

    public void playAgain()
    {
        // Load game
        SceneManager.LoadScene("Level01");
    }
}
