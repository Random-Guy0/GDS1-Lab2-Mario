using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float bounceVelocity;

    private bool canBounce = false;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = moveSpeed;
        rb.velocity = velocity;

        if (canBounce)
        {
            canBounce = false;
            rb.AddForce(Vector2.up * bounceVelocity, ForceMode2D.Impulse);
        }
    }

    private void Update()
    {
        Vector2 fireballPos = mainCamera.WorldToViewportPoint(transform.position);
        if (fireballPos.x < 0 || fireballPos.x > 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            //kill enemy
            Destroy(gameObject);
        }
        else if(!col.gameObject.CompareTag("Player") && transform.position.y > col.transform.position.y)
        {
            canBounce = true;
        }
    }
}
