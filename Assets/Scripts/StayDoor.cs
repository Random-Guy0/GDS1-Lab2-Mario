using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayDoor : MonoBehaviour
{
    bool ready = false;
    public GameObject player;
    public GameObject camerain;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ready)
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                player.transform.position = new Vector2(-6.85f, -9.06f);
                camerain.transform.position = new Vector3(0f, -15.5f, -10.0f);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("ready to leave");
            ready = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("cant to leave");
            ready = false;
        }
    }
}
