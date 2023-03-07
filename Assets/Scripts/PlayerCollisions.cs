using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            if (transform.position.y <= col.transform.position.y)
            {
                playerStats.TakeDamage();
            }
            else
            {
                //bounce
            }
        }
        else if (col.gameObject.CompareTag("Powerup"))
        {
            playerStats.CollectPowerup();
        }
        else if (col.gameObject.CompareTag("Block") && transform.position.y < col.transform.position.y)
        {
            //hit block
        }
        else if (col.gameObject.CompareTag("Coin"))
        {
            //collect coin
        }
    }
}
