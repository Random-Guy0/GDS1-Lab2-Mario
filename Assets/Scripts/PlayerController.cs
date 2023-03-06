using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkMaxSpeed;
    [SerializeField] private float walkSpeedIncreaseFactor;
    [SerializeField] private float walkSpeedDecreaseFactor;

    [SerializeField] private float jumpVelocity;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float lowJumpMultiplier;
    [SerializeField] private Rigidbody2D rb;
    
    private float currentSpeed;
    
    private float moveDir = 0.0f;
    private float previousMoveDir = 0.0f;

    private bool canJump = true;
    private bool currentlyJumping = false;
    private bool jumpRequest = false;
    
    public void OnMove(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<float>();
    }
    
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (moveDir != 0.0f)
        {
            previousMoveDir = moveDir;
            if (currentSpeed != walkMaxSpeed)
            {
                currentSpeed += walkSpeedIncreaseFactor * Time.deltaTime * moveDir;
                if (Mathf.Abs(currentSpeed) > walkMaxSpeed)
                {
                    currentSpeed = walkMaxSpeed * moveDir;
                }
            }
        }
        else
        {
            if (currentSpeed != 0.0f)
            {
                currentSpeed -= walkSpeedDecreaseFactor * Time.deltaTime * previousMoveDir;
                if ((previousMoveDir > 0 && currentSpeed < 0) || (previousMoveDir < 0 && currentSpeed > 0))
                {
                    currentSpeed = 0.0f;
                }
            }
        }
        
        Vector3 newPos = transform.position;
        newPos.x += currentSpeed * Time.deltaTime;
        transform.position = newPos;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && canJump)
        {
            currentlyJumping = true;
            canJump = false;
            jumpRequest = true;
        }
        else if (context.canceled)
        {
            currentlyJumping = false;
        }
    }

    private void FixedUpdate()
    {
        if (jumpRequest)
        {
            rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            jumpRequest = false;
        }
        BetterJump();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        canJump = true;
    }

    //https://www.youtube.com/watch?v=7KiK0Aqtmzc
    //https://www.youtube.com/watch?v=acBCegN60kw
    private void BetterJump()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !currentlyJumping)
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }
}
