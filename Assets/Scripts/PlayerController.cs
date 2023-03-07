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

    [SerializeField] private float sprintMaxSpeed;
    [SerializeField] private float sprintSpeedIncreaseFactor;
    [SerializeField] private float sprintSpeedDecreaseFactor;

    [SerializeField] private float jumpVelocity;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float lowJumpMultiplier;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private BoxCollider2D playerCollider;

    private float maxSpeed;
    private float currentSpeed;
    private float currentSpeedIncreaseFactor;
    private float currentSpeedDecreaseFactor;
    
    private float moveDir = 0.0f;
    private float previousMoveDir = 0.0f;

    private bool canJump = true;
    private bool currentlyJumping = false;
    private bool jumpRequest = false;

    private bool crouching = false;

    private Camera mainCamera;

    private void Start()
    {
        maxSpeed = walkMaxSpeed;
        currentSpeedIncreaseFactor = walkSpeedIncreaseFactor;
        currentSpeedDecreaseFactor = walkSpeedDecreaseFactor;
        
        mainCamera = Camera.main;
    }

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
        if (crouching && canJump)
        {
            moveDir = 0;
        }
        
        if (moveDir != 0.0f)
        {
            previousMoveDir = moveDir;
            if (currentSpeed < maxSpeed)
            {
                currentSpeed += currentSpeedIncreaseFactor * Time.deltaTime * moveDir;
                if (Mathf.Abs(currentSpeed) > maxSpeed)
                {
                    currentSpeed = maxSpeed * moveDir;
                }
            }
            else if (currentSpeed > maxSpeed)
            {
                currentSpeed -= currentSpeedDecreaseFactor * Time.deltaTime * moveDir;
            }
        }
        else
        {
            if (currentSpeed != 0.0f)
            {
                currentSpeed -= currentSpeedDecreaseFactor * Time.deltaTime * previousMoveDir;
                if ((previousMoveDir > 0 && currentSpeed < 0) || (previousMoveDir < 0 && currentSpeed > 0))
                {
                    currentSpeed = 0.0f;
                }
            }
        }
        
        Vector3 newPos = transform.position;
        newPos.x += currentSpeed * Time.deltaTime;

        if (mainCamera.WorldToViewportPoint(Vector3.right * (newPos.x - 0.5f)).x >= 0.0)
        {
            transform.position = newPos;
        }
        else
        {
            currentSpeed = 0;
        }
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

    public void SprintAndAbility(InputAction.CallbackContext context)
    {
        //start sprinting/shoot fireball
        if (context.performed)
        {
            maxSpeed = sprintMaxSpeed;
            currentSpeedIncreaseFactor = sprintSpeedIncreaseFactor;
            currentSpeedDecreaseFactor = sprintSpeedDecreaseFactor;
        }
        //stop sprinting
        else if (context.canceled)
        {
            maxSpeed = walkMaxSpeed;
            currentSpeedIncreaseFactor = walkSpeedIncreaseFactor;
            currentSpeedDecreaseFactor = walkSpeedDecreaseFactor;
        }
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (playerStats.GetPowerupState() > 0)
            {
                crouching = true;
                playerCollider.size = Vector2.one;
                playerCollider.offset = Vector2.zero;
            }
        }
        else if (context.canceled)
        {
            if (crouching)
            {
                crouching = false;
                playerCollider.size = new Vector2(1, 2);
                playerCollider.offset = new Vector2(0, 0.5f);
            }
        }
    }

    public void BounceOnEnemy()
    {
        canJump = false;
        if (currentlyJumping)
        {
            jumpRequest = true;
        }
        else
        {
            rb.AddForce(Vector2.up * (jumpVelocity / 2.0f), ForceMode2D.Impulse);
        }
    }
}
