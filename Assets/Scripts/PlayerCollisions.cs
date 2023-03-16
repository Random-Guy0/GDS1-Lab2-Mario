using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerController playerController;
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Vector3 contactPoint = col.GetContact(0).point;
            Vector3 center = col.otherCollider.bounds.center;
            
            bool bottom = contactPoint.y < center.y;
            
            if (!bottom)
            {
                playerStats.TakeDamage();
            }
            else
            {
                playerController.BounceOnEnemy();
            }
        }
        else if (col.gameObject.CompareTag("RedMushroom"))
        {
            playerStats.CollectPowerup();
        }
        /*else if (col.gameObject.CompareTag("Block") && transform.position.y < col.transform.position.y)
        {
            //hit block
        }*/
        else if (col.gameObject.CompareTag("Coin"))
        {
            //collect coin
        }
        else if (col.gameObject.CompareTag("Death"))
        {
            playerStats.Die();
        }
        else if (col.gameObject.CompareTag("GreenMushroom"))
        {
            playerStats.OneUp();
        }
    }
}
