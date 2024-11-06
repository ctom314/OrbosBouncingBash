using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ball;
    public List<GameObject> bricks;

    // Brick sprites
    public List<Sprite> bricksNormal;
    public List<Sprite> bricksLightCrack;
    public List<Sprite> bricksHeavyCrack;

    private GameKeybindHandler gkh;
    private WinManager wm;
    private GameOverManager gom;

    // Start is called before the first frame update
    void Start()
    {
        // Final all bricks in the scene
        bricks = new List<GameObject>(GameObject.FindGameObjectsWithTag("Brick"));

        gkh = GetComponent<GameKeybindHandler>();
        wm = GetComponent<WinManager>();
        gom = GetComponent<GameOverManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if all bricks are destroyed
        if (allBricksDestroyed())
        {
            // Trigger win
            wm.triggerWin();

            gkh.nextLevel();
            gkh.winToMainMenu();
        }
        else
        {
            if (gom.isGameOver)
            {
                gkh.retry();
                gkh.returnToMainMenu();
            }
        }
    }

    public Sprite getNormalBrick(int id)
    {
        return bricksNormal[id];
    }

    public Sprite getLightCrackBrick(int id)
    {
        return bricksLightCrack[id];
    }

    public Sprite getHeavyCrackBrick(int id)
    {
        return bricksHeavyCrack[id];
    }

    public bool allBricksDestroyed()
    {
        return bricks.Count == 0;
    }

}
