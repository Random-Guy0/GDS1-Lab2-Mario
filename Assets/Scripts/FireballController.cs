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

    private float direction = 0;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void SetDirection(float newDirection)
    {
        direction = newDirection;
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = moveSpeed * direction;
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
        Vector3 contactPoint = col.GetContact(0).point;
        Vector3 center = col.otherCollider.bounds.center;

        bool right = contactPoint.x > center.x;
        bool left = contactPoint.x < center.x;
        bool bottom = contactPoint.y < center.y;
        bool top = contactPoint.y > center.y;
        
        if (col.gameObject.CompareTag("Enemy"))
        {
            //kill enemy
            Destroy(gameObject);
        }
        else if(!col.gameObject.CompareTag("Player") && bottom)
        {
            canBounce = true;
        }
        
        if (!canBounce || ((right || left) && !bottom && !top))
        {
            Destroy(gameObject);
        }
    }
}
