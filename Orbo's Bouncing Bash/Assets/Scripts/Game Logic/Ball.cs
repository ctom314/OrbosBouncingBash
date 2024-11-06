using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public float speed = 10.0f;
    public float ballStartX;
    public float ballStartY;

    private Rigidbody2D rb;
    private float screenHalfWidth;
    private float screenHalfHeight;

    private float minYVelocity = 0.5f;

    // Reflect timer
    private float reflectTime = 0.0f;
    private float reflectDuration = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Get ball start position
        ballStartX = transform.position.x;
        ballStartY = transform.position.y;

        // Wait then launch ball
        StartCoroutine(startLogic());

        // Get camera bounds
        Camera cam = Camera.main;
        screenHalfHeight = cam.orthographicSize;
        screenHalfWidth = cam.aspect * screenHalfHeight;
    }

    void Update()
    {
        // Set reflect timer
        reflectTime = Time.deltaTime;

        checkBounds();
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
        else if (collision.gameObject.CompareTag("PaddleSide") && reflectTime > reflectDuration)
        {
            // Reflect x direction, keep y direction
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);

            // Reset reflect timer
            reflectTime = 0.0f;
        }
    }

    public void setLaunchDirection()
    {
        // Set initial velocity. Pick random direction
        int choice = UnityEngine.Random.Range(0, 6);
        if (choice == 0)
        {
            // 45* Up right
            rb.velocity = new Vector2(2, 2).normalized * speed;
        }
        else if (choice == 1)
        {
            // 45* Up left
            rb.velocity = new Vector2(-2, 2).normalized * speed;
        }
        else if (choice == 2)
        {
            // 30* Up right
            rb.velocity = new Vector2(1, 2).normalized * speed;
        }
        else if (choice == 3)
        {
            // 30* Up left
            rb.velocity = new Vector2(-1, 2).normalized * speed;
        }
        else if (choice == 4)
        {
            // 60* Up right
            rb.velocity = new Vector2(3, 1).normalized * speed;
        }
        else
        {
            // 60* Up left
            rb.velocity = new Vector2(-3, 1).normalized * speed;
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

            // Ensure ball cannot bounce back and forth indefinitely
            if (Mathf.Abs(rb.velocity.y) < minYVelocity)
            {
                // Set velocity to minimum y velocity in same direction
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * minYVelocity);
            }
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

    private IEnumerator startLogic()
    {
        // Wait 1 second
        yield return new WaitForSeconds(1.0f);

        // Launch ball
        setLaunchDirection();
    }
}
