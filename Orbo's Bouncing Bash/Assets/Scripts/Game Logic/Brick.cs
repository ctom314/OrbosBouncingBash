using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int scoreBaseAmount = 10;
    public float powerupChance = 0.25f;
    public GameObject[] powerups;
    public GameManager gameManager;

    private ScoreManager sm;
    private SpriteRenderer sr;
    private int brickType;
    private int hp = 1;
    private int maxHp;
    private int scoreAmount;

    // Start is called before the first frame update
    void Start()
    {
        sm = gameManager.GetComponent<ScoreManager>();
        sr = GetComponent<SpriteRenderer>();

        // Randomize HP
        maxHp = Random.Range(1, 4);
        hp = maxHp;

        // Calculate score amount based on HP
        scoreAmount = scoreBaseAmount * maxHp;

        // Randomize brick type (Color)
        brickType = Random.Range(0, gameManager.bricksNormal.Count);

        // Set brick texture
        updateBrickTexture();
    }

    public void TakeDamage()
    {
        hp--;
        if (hp <= 0)
        {
            // Remove brick from list
            gameManager.bricks.Remove(gameObject);

            // Destroy the brick
            Destroy(gameObject);

            // Award score
            sm.addScore(scoreAmount);
        }
    }

    private void updateBrickTexture()
    {
        // Change color based on hp and max hp
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            TakeDamage();
            updateBrickTexture();

            // TODO: Spawn powerup chance
        }
    }
}
