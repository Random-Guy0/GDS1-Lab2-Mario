using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private float moveDir = 0.0f;

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
        Vector3 newPos = transform.position;
        newPos.x += moveDir * moveSpeed * Time.deltaTime;
        transform.position = newPos;
    }
}
