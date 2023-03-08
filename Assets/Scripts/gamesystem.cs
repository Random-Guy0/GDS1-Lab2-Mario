using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gamesystem : MonoBehaviour
{
    public int coinCounter;
    public int ScoreCounter;
    float seconds;
    public float Timer = 300;

    public static gamesystem instance;
    public TMP_Text marioScoreText;
    public TMP_Text coinScoreText;
    public TMP_Text timeScoreText;

    public AudioClip coinSound;
    // Start is called before the first frame update
    void Start()
    {
        coinCounter = 0;
        ScoreCounter = 0;
        seconds = 300;

        marioScoreText.text = "00" + ScoreCounter.ToString();
        coinScoreText.text = "X " + coinCounter.ToString();
        timeScoreText.text = "" + seconds.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        seconds = (int)(Timer);
        timeScoreText.text = "" + seconds.ToString();
    }

    public void Add(int coin, int score)
    {
        coinCounter += coin;
        ScoreCounter += score;
        marioScoreText.text = "00" + ScoreCounter.ToString();
        coinScoreText.text = "X " + coinCounter.ToString();
        AudioSource.PlayClipAtPoint(coinSound, transform.position);
    }
}