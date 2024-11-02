using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Menu objs
    public GameObject mainMenu;

    // Info
    public GameObject guideMenu;

    // Menu Bools
    public bool mainMenuActive = true;
    public bool guideMenuActive = false;
    
    private MenuButtonHandler mbh;
    private MenuKeybindHandler mkbh;

    // Start is called before the first frame update
    void Start()
    {
        mbh = GetComponent<MenuButtonHandler>();
        mkbh = GetComponent<MenuKeybindHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mainMenuActive && !guideMenuActive)
        {
            mkbh.startGame();
            mkbh.quitGame();
            mkbh.guideMenuButton();
        }
        else if (!mainMenuActive && guideMenuActive)
        {
            mkbh.backToMainMenu();
        }
    }
}
