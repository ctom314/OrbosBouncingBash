using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBounds : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // TODO: Lose a life and reset the ball
            SceneManager.LoadScene("Level01");
        }
        else
        {
            // For any powerups that may fall out of bounds
            Destroy(collision.gameObject);
        }
    }
}
