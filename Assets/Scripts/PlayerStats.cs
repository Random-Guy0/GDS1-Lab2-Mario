using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private BoxCollider2D playerCollider;

    private PlayerLives playerLives;

    private PowerupState powerupState;

    private void Start()
    {
        powerupState = PowerupState.Small;
        playerLives = FindObjectOfType<PlayerLives>();
    }

    public void OneUp()
    {
        playerLives.OneUp();
    }

    public int GetLives()
    {
        return playerLives.GetLives();
    }

    public void CollectPowerup()
    {
        switch (powerupState)
        {
            case PowerupState.Small:
                powerupState = PowerupState.Big;
                playerCollider.size = new Vector2(1, 2);
                playerCollider.offset = new Vector2(0, 0.5f);
                break;
            case PowerupState.Big:
                powerupState = PowerupState.Flower;
                break;
            case PowerupState.Flower:
                break;
        }
    }
    
    public void TakeDamage()
    {
        if (powerupState == PowerupState.Small)
        {
            playerLives.LoseLife();
            if (playerLives.GetLives() == 0)
            {
                Destroy(playerLives.gameObject);
                //Game over
            }
        }
        else
        {
            powerupState = PowerupState.Small;
            playerCollider.size = Vector2.one;
            playerCollider.offset = Vector2.zero;
        }
    }

    public PowerupState GetPowerupState()
    {
        return powerupState;
    }
}

public enum PowerupState
{
    Small,
    Big,
    Flower
}
