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
        mm.loadGame();
    }

    public void returnToMainMenu()
    {
        mm.mainMenuActive = true;
        mm.guideMenuActive = false;

        // Reset guide menu
        mm.guideMenu.SetActive(false);
        mm.bricksPage.SetActive(true);
        mm.effectsPage.SetActive(false);

        // Show main menu
        mm.mainMenu.SetActive(true);
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

    public void showBricksPage()
    {
        mm.mainMenuActive = false;
        mm.guideMenuActive = true;

        // Hide effects page
        mm.effectsPage.SetActive(false);

        // Show bricks page
        mm.bricksPage.SetActive(true);
    }

    public void showEffectsPage()
    {
        mm.mainMenuActive = false;
        mm.guideMenuActive = true;

        // Hide bricks page
        mm.bricksPage.SetActive(false);

        // Show effects page
        mm.effectsPage.SetActive(true);
    }
}
