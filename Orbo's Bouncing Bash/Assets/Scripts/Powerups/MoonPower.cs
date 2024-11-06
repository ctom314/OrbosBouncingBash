using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonPower : MonoBehaviour
{
    public int damageMult = 2;
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
            // Increase ball damage
            pm.moonPower(damageMult, duration);
            pm.powerupSpawned = false;

            // Setup powerup UI
            pm.powerupActive = true;
            pm.powerupType = 3;
            pm.duration = duration;

            // Destroy powerup
            Destroy(gameObject);
        }
    }
}
