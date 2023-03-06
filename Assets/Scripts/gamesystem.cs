using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamesystem : MonoBehaviour
{
    public int coinCounter;
    public int SocreCounter;
    public float seconds;
    float Timer;

    // Start is called before the first frame update
    void Start()
    {
        coinCounter = 0;
        SocreCounter = 0;
        seconds = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        seconds = (int)(Timer % 60);
    }

    public void Add(int coin, int socre)
    {
        coinCounter += coin;
        SocreCounter += socre;
    }
}