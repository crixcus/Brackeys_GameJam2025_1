using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEditor.SearchService;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 20f; // Speed of movement
    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        // Get input from WASD keys
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
    }

    void FixedUpdate()
    {
        rb.velocity = moveInput * moveSpeed;
    }
}

