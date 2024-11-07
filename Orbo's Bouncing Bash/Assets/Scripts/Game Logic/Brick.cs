using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int scoreBaseAmount = 10;
    public GameObject[] powerups;
    public GameManager gameManager;

    private ScoreManager sm;
    private PowerupManager pm;
    private LevelManager lm;

    private SpriteRenderer sr;
    private int brickType;
    private int hp = 1;
    private int maxHp;
    private int scoreAmount;

    // Spawning special bricks
    private bool spawnSpecial;
    private bool isMetal = false;
    private bool isSpecial = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get managers
        sm = gameManager.GetComponent<ScoreManager>();
        pm = gameManager.GetComponent<PowerupManager>();
        lm = gameManager.GetComponent<LevelManager>();

        sr = GetComponent<SpriteRenderer>();

        // Spawn special bricks by chance
        spawnSpecial = Random.Range(0.0f, 1.0f) <= lm.specialBrickChance;
        if (spawnSpecial)
        {
            spawnSpecialBrick();
        }

        // Randomize HP if not metal or special
        if (!isMetal && !isSpecial)
        {   
            maxHp = Random.Range(1, 4);
        }
        else if (isMetal && !isSpecial)
        {
            maxHp = 6;
        }
        else if (!isMetal && isSpecial)
        {
            maxHp = 3;
        }

        hp = maxHp;

        // Calculate score amount based on HP
        scoreAmount = scoreBaseAmount * maxHp;

        // Randomize brick type (Color)
        brickType = Random.Range(0, gameManager.bricksNormal.Count);

        // Set brick texture
        updateBrickTexture();
    }

    private void spawnSpecialBrick()
    {
        // Choose what type of special brick to spawn
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            isMetal = true;
        }
        else if (random == 1)
        {
            isSpecial = true;
        }
    }

    public void TakeDamage()
    {
        hp -= gameManager.ballDamage;
        if (hp <= 0)
        {
            // Remove brick from list
            gameManager.bricks.Remove(gameObject);

            // Try to spawn powerup if one is not already active or spawned
            if (!pm.powerupActive && !pm.powerupSpawned)
            {
                powerupSpawning();
            }

            // Destroy the brick
            Destroy(gameObject);

            // Award score
            sm.addScore(scoreAmount);
        }
    }

    private void powerupSpawning()
    {
        float random = Random.Range(0.0f, 1.0f);
        float threshold = 1 - pm.powerupChance;

        // Spawn random powerup if threshold is met OR if the brick is special
        if (random >= threshold || isSpecial)
        {
            int randomPowerup = Random.Range(0, powerups.Length);
            Instantiate(powerups[randomPowerup], transform.position, Quaternion.identity);
            pm.powerupSpawned = true;
        }
    }

    private void updateBrickTexture()
    {
        // Change color based on hp and max hp
        if (!isMetal && !isSpecial)
        {
            // MAX 3 HP
            if (maxHp == 3 && hp == 3)
            {
                sr.sprite = gameManager.getNormalBrick(brickType);
            }
            else if (maxHp == 3 && hp == 2)
            {
                sr.sprite = gameManager.getLightCrackBrick(brickType);
            }
            else if (maxHp == 3 && hp == 1)
            {
                sr.sprite = gameManager.getHeavyCrackBrick(brickType);
            }

            // MAX 2 HP
            if (maxHp == 2 && hp == 2)
            {
                sr.sprite = gameManager.getLightCrackBrick(brickType);
            }
            else if (maxHp == 2 && hp == 1)
            {
                sr.sprite = gameManager.getHeavyCrackBrick(brickType);
            }

            // MAX 1 HP
            if (maxHp == 1 && hp == 1)
            {
                sr.sprite = gameManager.getHeavyCrackBrick(brickType);
            }
        }
        else if (isMetal && !isSpecial)
        {
            // Special texturing for metal bricks
            // MAX 6 HP
            if (maxHp == 6 && hp == 6)
            {
                sr.sprite = gameManager.getMetalBrick(0);
            }
            else if (maxHp == 6 && hp == 5)
            {
                sr.sprite = gameManager.getMetalBrick(1);
            }
            else if (maxHp == 6 && hp == 4)
            {
                sr.sprite = gameManager.getMetalBrick(2);
            }
            else if (maxHp == 6 && hp == 3)
            {
                sr.sprite = gameManager.getMetalBrick(3);
            }
            else if (maxHp == 6 && hp == 2)
            {
                sr.sprite = gameManager.getMetalBrick(4);
            }
            else if (maxHp == 6 && hp == 1)
            {
                sr.sprite = gameManager.getMetalBrick(5);
            }
        }
        else if (!isMetal && isSpecial)
        {
            // Special texturing for special bricks
            // MAX 3 HP
            if (maxHp == 3 && hp == 3)
            {
                sr.sprite = gameManager.getSpecialBrick(0);
            }
            else if (maxHp == 3 && hp == 2)
            {
                sr.sprite = gameManager.getSpecialBrick(1);
            }
            else if (maxHp == 3 && hp == 1)
            {
                sr.sprite = gameManager.getSpecialBrick(2);
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            TakeDamage();
            updateBrickTexture();
        }
    }
}
