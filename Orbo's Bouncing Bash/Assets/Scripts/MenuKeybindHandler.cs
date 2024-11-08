using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuKeybindHandler : MonoBehaviour
{
    // All keybindings for the main menu go here
    private MenuButtonHandler mbh;
    private MenuManager mm;

    // Timer vars for pressing Q to return to main menu, while preventing quitting at the same time
    private float menuChangeTime = 0f;
    private float quitDelay = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        mbh = GetComponent<MenuButtonHandler>();
        mm = GetComponent<MenuManager>();
    }

    public void startGame()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mm.loadGame();
        }
    }

    public void quitGame()
    {
        // Ensure quitting is not possible immediately after returning to main menu
        if (Input.GetKeyDown(KeyCode.Q) && (Time.unscaledTime - menuChangeTime) > quitDelay)
        {
            mbh.quitGame();
        }
    }

    public void backToMainMenu()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            mbh.returnToMainMenu();
            menuChangeTime = Time.unscaledTime;
        }
    }

    // Guide Menu
    public void guideMenuButton()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            mm.guideMenuActive = true;

            mbh.loadGuideMenu();
        }
    }

    public void toggleInfoPages()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (mm.bricksPage.activeSelf)
            {
                // Go to effects page
                mbh.showEffectsPage();
            }
            else if (mm.effectsPage.activeSelf)
            {
                // Go to bricks page
                mbh.showBricksPage();
            }
        }
    }
}
