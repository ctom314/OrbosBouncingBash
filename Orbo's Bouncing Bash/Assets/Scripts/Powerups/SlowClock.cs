using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowClock : MonoBehaviour
{
    public float slowFactor = 0.5f;
    public int duration = 5;

    private PowerupManager pm;

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.Find("GameManager").GetComponent<PowerupManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Paddle"))
        {
            // Slow paddle
            pm.slowClock(slowFactor, duration);
            pm.powerupSpawned = false;

            // Setup powerup UI
            pm.powerupActive = true;
            pm.powerupType = 1;
            pm.duration = duration;

            // Destroy powerup
            Destroy(gameObject);
        }
    }
}
