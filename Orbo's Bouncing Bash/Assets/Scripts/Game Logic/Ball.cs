using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public float speed = 10.0f;

    private Rigidbody2D rb;
    private float screenHalfWidth;
    private float screenHalfHeight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Set initial velocity
        rb.velocity = new Vector2(2, 2).normalized * speed;

        // Get camera bounds
        Camera cam = Camera.main;
        screenHalfHeight = cam.orthographicSize;
        screenHalfWidth = cam.aspect * screenHalfHeight;
    }

    void Update()
    {
        checkBounds();

        // TEMPORARY
        resetLevel();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Maintain constant speed
        rb.velocity = rb.velocity.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Bounce off paddle upwards
        if (collision.gameObject.CompareTag("Paddle"))
        {
            Vector2 paddlePos = collision.transform.position;
            float hitPos = (transform.position.x - paddlePos.x) / (collision.collider.bounds.size.x / 2);

            // Calculate a new direction based on hit position, with an upward force
            Vector2 newDirection = new Vector2(hitPos, 1).normalized;

            // Set the ball's velocity to the new direction
            rb.velocity = newDirection * speed;
        }

        // If hitting the side of paddle, keep y direction
        else if (collision.gameObject.CompareTag("PaddleSide"))
        {
            // Reflect x direction, keep y direction
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        }
    }

    private void resetLevel()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Level01");
        }
    }

    private void checkBounds()
    {
        Vector2 pos = transform.position;

        // Check left and right bounds
        if (pos.x < -screenHalfWidth || pos.x > screenHalfWidth)
        {
            // Reverse x velocity
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
            pos.x = Mathf.Clamp(pos.x, -screenHalfWidth, screenHalfWidth);
        }

        // Check top bounds
        if (pos.y > screenHalfHeight)
        {
            // Reverse y velocity
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
            pos.y = screenHalfHeight;
        }

        transform.position = pos;
    }
}
