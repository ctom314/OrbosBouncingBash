using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBounds : MonoBehaviour
{
    public GameObject ballObj;
    public GameObject paddle;

    // Managers
    private GameManager gm;
    private LivesManager lm;
    private PowerupManager pm;
    private GameOverManager gom;

    private Ball ballComp;

    // Start is called before the first frame update
    void Start()
    {
        // Get managers
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        lm = gm.GetComponent<LivesManager>();
        gom = gm.GetComponent<GameOverManager>();
        pm = gm.GetComponent<PowerupManager>();

        ballComp = ballObj.GetComponent<Ball>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Lose life
            lm.LoseLife();

            if (lm.lives > 0)
            {
                // Reset ball pos and launch
                StartCoroutine(resetLogic());
            }
            else
            {
                // Game over
                gom.triggerGameOver();
            }

        }
        else
        {
            // For any powerups that may fall out of bounds
            Destroy(collision.gameObject);
            pm.powerupSpawned = false;
        }
    }

    private IEnumerator resetLogic()
    {
        // Stop paddle movement
        paddle.GetComponent<PaddleMovement>().canMove = false;

        // Wait 1 second
        yield return new WaitForSeconds(1.0f);

        // Enable paddle movement
        paddle.GetComponent<PaddleMovement>().canMove = true;

        // Reset ball position and remove velocity
        ballObj.transform.position = new Vector2(ballComp.ballStartX, ballComp.ballStartY);
        ballComp.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        // Wait 1 second
        yield return new WaitForSeconds(1.0f);

        // Launch ball
        ballComp.setLaunchDirection();
    }
}
