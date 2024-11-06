using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : MonoBehaviour
{
    public float speedMult = 2f;
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
            // Speed up ball and paddle
            pm.coffeeRush(speedMult, duration);
            pm.powerupSpawned = false;

            // Setup powerup UI
            pm.powerupActive = true;
            pm.powerupType = 2;
            pm.duration = duration;

            // Destroy powerup
            Destroy(gameObject);
        }
    }
}
