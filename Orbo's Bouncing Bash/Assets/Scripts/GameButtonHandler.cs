using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtonHandler : MonoBehaviour
{
    private PauseManager pm;
    private LevelManager lm;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PauseManager>();
        lm = GetComponent<LevelManager>();
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
        // Reset Persistant Data
        PersistantData.instance.resetData();

        string curLevel = SceneManager.GetActiveScene().name;
        string nextLevel = lm.chooseLevelNoRepeat(curLevel);

        // Load next level
        SceneManager.LoadScene(nextLevel);

        Time.timeScale = 1;
    }

    public void nextLevel()
    {
        string curLevel = SceneManager.GetActiveScene().name;
        string nextLevel = lm.chooseLevelNoRepeat(curLevel);

        // Load next level
        SceneManager.LoadScene(nextLevel);
    }
}
