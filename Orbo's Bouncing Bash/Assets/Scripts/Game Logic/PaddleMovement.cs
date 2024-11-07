using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleMovement : MonoBehaviour
{
    public float speed = 10.0f;
    public bool canMove = true;

    private float paddleWidth;
    private float screenHalfWidth;

    // Controller input
    private Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();

        // Get paddle width
        paddleWidth = renderer.bounds.size.x;

        // Get screen half width
        screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize - (paddleWidth / 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            movePaddle();
        }
    }

    // Get Controller input
    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void movePaddle()
    {
        float hInput = Input.GetAxisRaw("Horizontal") + moveInput.x;

        // Move paddle
        Vector3 pos = transform.position + Vector3.right * hInput * speed * Time.deltaTime;

        // Clamp paddle position - Keep paddle within screen bounds
        pos.x = Mathf.Clamp(pos.x, -screenHalfWidth, screenHalfWidth);
        transform.position = pos;
    }
}
