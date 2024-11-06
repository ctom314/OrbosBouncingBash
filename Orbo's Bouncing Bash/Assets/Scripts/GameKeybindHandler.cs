using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameKeybindHandler : MonoBehaviour
{
    private GameButtonHandler gbh;
    private GameManager gm;
    private WinManager wm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<GameManager>();
        gbh = GetComponent<GameButtonHandler>();
        wm = GetComponent<WinManager>();
    }

    // Game Over Screen
    public void retry()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            gbh.playAgain();
        }
    }
    
    public void returnToMainMenu()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            gbh.returnToMainMenu();
        }
    }

    // Win Screen
    public void nextLevel()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gbh.nextLevel();
        }
    }

    public void winToMainMenu()
    {
        if (Input.GetKeyDown(KeyCode.Q) && wm.isWin)
        {
            gbh.returnToMainMenu();
        }
    }
}
