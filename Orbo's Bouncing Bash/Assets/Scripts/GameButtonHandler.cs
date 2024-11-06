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

        // TODO: Switch chooseLevel to chooseLevelNoRepeat
        string curLevel = SceneManager.GetActiveScene().name;
        //string nextLevel = lm.chooseLevelNoRepeat(curLevel);
        string nextLevel = lm.chooseLevel();

        // Load next level
        SceneManager.LoadScene(nextLevel);
    }

    public void nextLevel()
    {
        // TODO: Switch chooseLevel to chooseLevelNoRepeat
        // TODO: Carry over score to new scenes

        string curLevel = SceneManager.GetActiveScene().name;
        //string nextLevel = lm.chooseLevelNoRepeat(curLevel);
        string nextLevel = lm.chooseLevel();

        // Load next level
        SceneManager.LoadScene(nextLevel);
    }
}
