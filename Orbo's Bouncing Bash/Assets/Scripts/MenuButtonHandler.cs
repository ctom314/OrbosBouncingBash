using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonHandler : MonoBehaviour
{
    private MenuManager mm;

    // Start is called before the first frame update
    void Start()
    {
        mm = GetComponent<MenuManager>();
    }

    public void quitGame()
    {
        // Only works in a built game
        Application.Quit();
    }

    public void playGame()
    {
        SceneManager.LoadScene("Level01");
        Time.timeScale = 1;
    }

    // Guide Menu
    public void loadGuideMenu()
    {
        mm.mainMenuActive = false;
        mm.guideMenuActive = true;

        // Hide main menu
        mm.mainMenu.SetActive(false);

        // Show guide menu
        mm.guideMenu.SetActive(true);
    }

    public void returnToMainMenu()
    {
        mm.mainMenuActive = true;
        mm.guideMenuActive = false;

        // Hide guide menu
        mm.guideMenu.SetActive(false);

        // Show main menu
        mm.mainMenu.SetActive(true);
    }

}
