using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    private int lives;
    
    private void Start()
    {
        DontDestroyOnLoad(this);
        lives = 3;
    }

    public void OneUp()
    {
        lives++;
    }

    public int GetLives()
    {
        return lives;
    }

    public void LoseLife()
    {
        lives--;
    }
}
