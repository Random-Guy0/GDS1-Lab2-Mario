using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    float shellSpeed = 5;
    float RespawnTime = 20;
    bool moving = false;
    Collider2D[] damageBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Move()
    {
        moving = true;
    }
    void Stop()
    {
        moving = false;
    }
    void Change()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.attachedRigidbody.velocity.y < 0 && !moving)
            {
                Move();
            }
            else
            {
                Debug.Log(name + ": deal damage");
            }
            if (moving && (collision.attachedRigidbody.velocity.y < 0 || !collision.IsTouching(damageBox[0])))
            {
                Stop();
            }
            else
            {
                Debug.Log(name + ": deal damage");
            }
        }
        if (moving)
        {
            // change direction
        }
    }
}
