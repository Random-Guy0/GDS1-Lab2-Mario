using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Count : MonoBehaviour
{
    public GameObject manager;
    public bool firstTouch = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!firstTouch)
        {
            GetComponent<Collider>().enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (firstTouch)
        {
            int score = 100;
            int i = (int)collision.transform.position.y;
            if(i != -2) {
                if (i != -1)
                {
                    score += (i + 1) * 700;
                }
                manager.GetComponent<gamesystem>().Add(0, score);
                firstTouch = false;
            }
        }
    }
}
